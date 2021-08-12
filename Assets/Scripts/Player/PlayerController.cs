using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        //Variables Declared
        private float playerMoveFraction = 1f;

        //Cached Reference
        private Mover mover;
        private Fighter fighter;
        private Health health;

        //states of the class

        private void Start()
        {
            Caches();
        }

        //get the cached references at the start
        private void Caches()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            Checks();
        }

        //all checks that needs to be made
        private void Checks()
        {
            if (LookForEnemy() && !health.IsDead) return;
            if (DetectCursorInputandMovePlayer()) return;
            print("No hits from raycast");
        }

        //see if the enemy was clicked on
        private bool LookForEnemy()
        {
           
           RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                EnemyTarget target = hit.collider.gameObject.GetComponent<EnemyTarget>();

                if(target == null) { continue; }

                GameObject attackTarget = target.gameObject;

                if (target == null) { continue; }

                if (!fighter.CanAttack(attackTarget)) { continue; }

                if (Input.GetMouseButton(0))
                {
                    fighter.Attack(attackTarget);
                }
                return true;
            }
            return false;
        }

        //detects where the mouse clicked and set that as the players destination
        private bool DetectCursorInputandMovePlayer()
        {
            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMovingPlayer(hitInfo.point, playerMoveFraction);
                }
                return true;
            }
            return false;
        }

        //get the raycast for the mouse
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}