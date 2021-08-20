using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Control;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, Iaction
    {
        //variables declared
        [Header("Fighter Settings")]
        [SerializeField] private float timeBetweenAttacks = 2f;

        private float timeSinceLastAttack = Mathf.Infinity;
        private float moveFraction = 1f;

        //cached references
        [Header("fighter caches")]
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapons defaultWeapon = null;

        private Transform currentTarget;
        private Mover mover;
        private Animator myAnim;
        private Health health;
        private Weapons currentWeapon = null;

        //called on the first frame the class comes into play
        private void Start()
        {
            Cached();
            EquiptWeapon(defaultWeapon);
        }

        //gets the referenced components
        private void Cached()
        {
            mover = GetComponent<Mover>();
            myAnim = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        //called every frame
        private void Update()
        {
            CheckIfDead();
        }

        //see if the object has health left
        private void CheckIfDead()
        {
            if (!health.IsDead)
            {
                UpdateTimer();
                MoveToTargetForAttack();
                StopAttackWhenDead();
            }
        }

        //check if we can attack
        public bool CanAttack(GameObject enemy)
        {
            if(enemy == null) { return false; }

            Health target = enemy.GetComponent<Health>();
            return target != null && !target.IsDead;
        }

        //updates the timer in realtime
        private void UpdateTimer()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        //moves in range to target to attack
        private void MoveToTargetForAttack()
        {
            if (currentTarget != null)
            {
                mover.MovePlayer(currentTarget.position, moveFraction);
                float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
                if(distanceToTarget <= currentWeapon.WeaponRange)
                {
                    mover.StopPlayer();
                    AttackSequence();
                }
            }
           
        }

        //everything that happens when the player attacks
        private void AttackSequence()
        {
            transform.LookAt(currentTarget);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                myAnim.ResetTrigger(Tags.ANIM_STOP_ATTACKING);
                myAnim.SetTrigger(Tags.ANIM_ATTACK);
                timeSinceLastAttack = 0f;
            }
        }

        //attacks the target
        public void Attack(GameObject target)
        {
            GetComponent<CombatScheduler>().StartAction(this);
            currentTarget = target.transform;
        }

        //Stop Attacking current Target
        public void StopAttack()
        {
            currentTarget = null;
            myAnim.ResetTrigger(Tags.ANIM_ATTACK);
            myAnim.SetTrigger(Tags.ANIM_STOP_ATTACKING);
        }

        //cancels the fighter class
        public void Cancel()
        {
            StopAttack();
            GetComponent<Mover>().Cancel();
        }

        //Animation Event Caller
        public void Hit()
        {
            if (currentTarget == null) { return; }
            Health theTarget = currentTarget.GetComponent<Health>();
            MeleeOrRanged(theTarget);
        }

        //see if the weapon is melee or ranged
        private void MeleeOrRanged(Health theTarget)
        {
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.InstantiateProjectile(rightHandTransform, leftHandTransform, theTarget);
            }
            else
            {
                theTarget.Damage(currentWeapon.WeaponDamage);
            }
        }

        //Ranged Animation Event Caller
        public void Shoot()
        {
            Hit();
        }

        //Stops the attack when the target is dead
        private void StopAttackWhenDead()
        {
            if (currentTarget == null) return;
            if (currentTarget.GetComponent<Health>().IsDead)
            {
                StopAttack();
            }

        }

        //calls the weapon to spawn
        public void EquiptWeapon(Weapons weapon)
        {
            if(weapon == null) { return; }
            currentWeapon = weapon;
            weapon.SpawnWeapon(rightHandTransform, leftHandTransform, myAnim);
        }
    }
}
