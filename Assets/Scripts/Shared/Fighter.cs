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
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 2f;

        private float timeSinceLastAttack = Mathf.Infinity;

        //cached references
        private Transform currentTarget;
        private Mover mover;
        private Animator myAnim;
        private Health health;

        //called on the first frame the class comes into play
        private void Start()
        {
            Cached();
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
                mover.MovePlayer(currentTarget.position);
                float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
                if(distanceToTarget <= attackRange)
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
        }

        //Animation Event Caller
        public void Hit()
        {
            if(currentTarget == null) { return; }
            currentTarget.GetComponent<Health>().Damage(10);
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
    }
}
