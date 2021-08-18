using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Control;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, Iaction, ISavable
    {
        //variables declared at the start
        [Header("Movement Settings")]
        [SerializeField] private float maxSpeed = 5.66f;

        //cached references
        private Animator myAnim;
        private NavMeshAgent agent;
        private Health health;

        //states

        // Awake is the first method called
        private void Awake()
        {
            Caches();
        }

        //calls all the cached references
        private void Caches()
        {
            agent = GetComponent<NavMeshAgent>();
            myAnim = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateAnimation();
        }

        //moves the player and cancels attacks
        public void StartMovingPlayer(Vector3 destination, float moveFraction)
        {
            if (!health.IsDead)
            {
                GetComponent<CombatScheduler>().StartAction(this);
                MovePlayer(destination, moveFraction);
            }
            else
            {
                return;
            }
        }

        //moves the player to a destination
        public void MovePlayer(Vector3 destination, float moveFraction)
        {
            agent.destination = destination;
            agent.speed = maxSpeed * Mathf.Clamp01(moveFraction);
            agent.isStopped = false;
        }

        //stops the player from moving
        public void StopPlayer()
        {
            agent.isStopped = true;
        }

        //updates the animator to see if the player is still, walking or running;
        private void UpdateAnimation()
        { 
            myAnim.SetFloat(Tags.ANIM_FORWARDSPEED, MoveSpeed());
        }

        //calculate the movespeed for the player
        private float MoveSpeed()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float moveSpeed = localVelocity.z;
            return moveSpeed;
        }

        //cancels the mover class
        public void Cancel()
        {
            StopPlayer();
        }

        //Capture the current state
        public object CaptureState()
        {
            return new Vector3Serializer(transform.position);
        }

        //restore the captured state
        public void RestoreState(object state)
        {
            Vector3Serializer unitPos = (Vector3Serializer)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = unitPos.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<CombatScheduler>().StopAction();
        }
    }
}