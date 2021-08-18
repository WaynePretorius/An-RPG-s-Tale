using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    public class Pickups : MonoBehaviour
    {
        [SerializeField] private Weapons weapon;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == Tags.TAG_PLAYER)
            {
                other.gameObject.GetComponent<Fighter>().EquiptWeapon(weapon);
                Destroy(gameObject, 0.5f);
            }
        }

    }
}