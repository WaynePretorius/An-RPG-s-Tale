using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapons : ScriptableObject
    {
        [Header("Weapon Stats")]
        [SerializeField] private float weaponDamage = 1f;
        [SerializeField] private float weaponRange = 1f;

        public float WeaponDamage { get { return weaponDamage; } }
        public float WeaponRange { get { return weaponRange; } }

        [Header("Weapon Caches")]
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animOverride;

        public void SpawnWeapon(Transform hand, Animator anim)
        {
            if (weaponPrefab != null)
            {
                Instantiate(weaponPrefab, hand);
            }

            if (animOverride != null)
            {
                anim.runtimeAnimatorController = animOverride;
            }
        }
    }
}