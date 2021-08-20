using RPG.Control;
using System;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapons : ScriptableObject
    {
        //varables declared
        [Header("Weapon Stats")]
        [SerializeField] private float weaponDamage = 1f;
        [SerializeField] private float weaponRange = 1f;

        //variable functions
        public float WeaponDamage { get { return weaponDamage; } }
        public float WeaponRange { get { return weaponRange; } }

        //cached references
        [Header("Weapon Caches")]
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animOverride;
        [SerializeField] Projectile projectile = null;

        //states
        [Header("Weapon States")]
        [SerializeField] private bool isRightHanded = true;

        //instantiates the weapon
        public void SpawnWeapon(Transform rightHand, Transform leftHand , Animator anim)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (weaponPrefab != null)
            { 
                Transform hand = LeftOrRightHand(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefab, hand);
                weapon.name = Tags.OBJ_WEAPON;
            }

            if (animOverride != null)
            {
                anim.runtimeAnimatorController = animOverride;
            }
        }

        //destroys the old weapon to make space for the new one
        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(Tags.OBJ_WEAPON);
            
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(Tags.OBJ_WEAPON);
            }

            if(oldWeapon == null) { return; }

            oldWeapon.name = Tags.OBJ_DESTROY;
            Destroy(oldWeapon.gameObject);
            
        }

        //instantiates the projectile and set the target 
        public void InstantiateProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Transform hand = LeftOrRightHand(rightHand, leftHand);
            Projectile projectileInstance = Instantiate(projectile, hand.position, Quaternion.identity);
            projectileInstance.SetTarget(target, WeaponDamage);
        }

        //does the weapon have a projectile
        public bool HasProjectile()
        {
            return projectile != null;
        }

        //Which hand will the weapon spawn
        private Transform LeftOrRightHand(Transform rightHand, Transform leftHand)
        {
            Transform hand;
            if (isRightHanded)
            {
                hand = rightHand;
            }
            else
            {
                hand = leftHand;
            }

            return hand;
        }

    }
}