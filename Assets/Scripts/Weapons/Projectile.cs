using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        //variables declared
        [Header("Projectile Settings")]
        [SerializeField] private float projectileSpeed = 4f;
        [SerializeField] private float effectDamage = 0;

        private float projectileDamage = 0;

        //functions for variables
        public float ProjectileDamage { get { return projectileDamage; } set { projectileDamage = value; } }

        //cached variables
        private Health target = null;

        //states
        [SerializeField] private bool hasEffect = false;
        [SerializeField] private bool isHoming = false;

        //sets the target
        public void SetTarget(Health appointedTarget, float damage)
        {
            target = appointedTarget;
            projectileDamage = damage;
        }

        private void Start()
        {
            PointProjectile();
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) { return; }
            if (isHoming && !target.IsDead)
            {
                PointProjectile();
            }
            MoveProjectile();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead) { return; }
            DamageAndDestroy();
        }

        //damage target and destroy it
        private void DamageAndDestroy()
        {
            target.Damage(SetDamage());
            Destroy(gameObject);
        }

        //increase the damage if there is effects on the projectile
        private float SetDamage()
        {
            if (hasEffect)
            {
                return projectileDamage += effectDamage;
            }
            else
            {
               return projectileDamage;
            }
        }

        //points the projectile at the target
        private void PointProjectile()
        {
            transform.LookAt(AimPosition());
        }

        //moves the projectile to the target
        private void MoveProjectile()
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }

        //aims for the middle of the target
        private Vector3 AimPosition()
        {
            CapsuleCollider collider = target.GetComponent<CapsuleCollider>();
            if (collider == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * collider.height / 2; ;
        }
    }
}