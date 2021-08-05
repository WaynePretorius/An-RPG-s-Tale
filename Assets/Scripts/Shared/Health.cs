using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        //variables declared
        [SerializeField] private float currentHealth = 100f;

        //functions for private variables and states
        public float CurrentHealth {get { return currentHealth; } set { currentHealth = value; } }
        public bool IsDead { get { return isDead; } }

        //gameStates
        bool isDead = false;

        //deducts damage from health
        public void Damage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth < 1)
            {
                Die();
            }
        }

        //play the death Animation
        private void Die()
        { 
            if (isDead) return;
            if (!isDead)
            {
                GetComponent<Animator>().SetTrigger(Tags.ANIM_DIE);
                isDead = true;
                GetComponent<Collider>().enabled = false;
            }
        }
            
    }
}