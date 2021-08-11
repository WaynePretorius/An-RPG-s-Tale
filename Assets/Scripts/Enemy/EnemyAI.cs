using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class EnemyAI : MonoBehaviour
    {
        //variables declared
        [Header("AI Settings")]
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float returnToPostTime = 3f;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float dwellTime = 2f;

        private float distanceToTarget = Mathf.Infinity;
        private float timeLastSeenPlayer = Mathf.Infinity;
        private float dwellTimer = Mathf.Infinity;
        private int currentIndex = 0;

        //cached references
        [Header("Ai Cache Settings")]
        [SerializeField] private PatrolPath path;
        private GameObject player;
        private Fighter fighter;
        private Health health;
        private CombatScheduler scheduler;
        private Mover mover;
        private Vector3 guadLocation;

        //game states
        private bool isProvoked = false;
        private bool isDwelling = false;

        //first method called as soon as the class comes into play
        private void Awake()
        {
            GetFindComponents();
        }

        //called in the first frame the object is spawned
        private void Start()
        {
            guadLocation = transform.position;
        }

        //give value to cahced references
        private void GetFindComponents()
        {
            player = GameObject.FindWithTag(Tags.TAG_PLAYER);
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            scheduler = GetComponent<CombatScheduler>();
        }

        //updates every frame
        private void Update()
        {
            if (!health.IsDead)
            {
                CheckIfEnemyInRange();
            }
        }

        //checks if the enemy is in range
        private void CheckIfEnemyInRange()
        {
            if (player == null) { return; }

            distanceToTarget = Vector3.Distance(player.transform.position, transform.position);

            AiStateOfChase();
            Timers();
        }

        //timers for the AI
        private void Timers()
        {
            timeLastSeenPlayer += Time.deltaTime;
            dwellTimer += Time.deltaTime;
        }

        //what the ai should do 
        private void AiStateOfChase()
        {
            if (distanceToTarget <= chaseDistance)
            {
                isProvoked = true;
                timeLastSeenPlayer = 0f;
                ChasePlayer();
            }
            else if (timeLastSeenPlayer < returnToPostTime)
            {
                scheduler.StopAction();
                isProvoked = false;
            }
            else
            {
                PatrolBehaviou();
            }
        }

        //draws the radius so that developer can see where the range is for the ai script
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        //moves to the player
        private void ChasePlayer()
        {
            if (isProvoked)
            {
                fighter.Attack(player);
            }
        }

        //stand still or move back to guard route
        private void PatrolBehaviou()
        {
            Vector3 nextPos = guadLocation;
            if (path != null)
            {
                if (AtWaypoint())
                {
                    Dwelling();
                    CycleWaypoint();
                }

                nextPos = GetCurrentWaypoint();
            }
            if (dwellTimer > dwellTime)
            {
                mover.StartMovingPlayer(nextPos);
            }
        }

        //set the dwelltimer so the ai can pause for a time
        private void Dwelling()
        {
            isDwelling = true;
            if (isDwelling)
            {
                dwellTimer = 0;
                isDwelling = false;
                scheduler.StopAction();
            }
        }

        //get the next waypoint index and set it
        private void CycleWaypoint()
        {
            int nextWaypointIndex = path.nextWaypoint(currentIndex);
            currentIndex = nextWaypointIndex;
        }

        //see if the ai is at it's current waypoint
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        //get the position of the current waypoint
        private Vector3 GetCurrentWaypoint()
        {
            Vector3 currentWaypoint = path.GetWaypoint(currentIndex).transform.position;
            return currentWaypoint;
        }
    }
}
