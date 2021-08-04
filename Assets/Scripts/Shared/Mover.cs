using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        //variables declared at the start

        //cached references
        private Animator myAnim;
        private NavMeshAgent agent;

        // Awake is the first method called
        private void Awake()
        {
            Caches();
        }

        private void Caches()
        {
            agent = GetComponent<NavMeshAgent>();
            myAnim = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateAnimation();
        }

        //moves the player to a destination
        public void MovePlayer(Vector3 destination)
        {
            agent.destination = destination;
        }

        //updates the animator to see if the player is still, walking or running;
        private void UpdateAnimation()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float moveSpeed = localVelocity.z;
            myAnim.SetFloat(Tags.ANIM_FORWARDSPEED, moveSpeed);
        }
    }
}