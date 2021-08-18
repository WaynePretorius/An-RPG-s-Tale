using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Control
{
    public class Health : MonoBehaviour, ISavable
    {
        //variables declared
        [SerializeField] private float currentHealth = 100f;

        //functions for private variables and states
        public float CurrentHealth {get { return currentHealth; } set { currentHealth = value; } }
        public bool IsDead { get { return isDead; } }

        //gameStates
        [SerializeField] bool isDead = false;

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
                StopandCancelComponents();
            }
        }

        //stop and cancel all components on death
        private void StopandCancelComponents()
        {
            GetComponent<Animator>().SetTrigger(Tags.ANIM_DIE);
            isDead = true;
            GetComponent<Collider>().enabled = false;
            GetComponent<CombatScheduler>().StopAction();
            GetComponent<NavMeshAgent>().enabled = false;
        }

        public object CaptureState()
        {
            return currentHealth;
        }

        //restore the captured state
        public void RestoreState(object state)
        {
            float unitHealth = (float)state;
            currentHealth = unitHealth;
            if(currentHealth <= 0)
            {
                Die();
            }
        }
    }
}