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

        //Cached Reference
        private Mover mover;
        private Fighter fighter;

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
        }

        // Update is called once per frame
        void Update()
        {
            if (LookForEnemy()) return;
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

                if (target == null) { continue; }
                if (!fighter.CanAttack(target)) { continue; }

                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target);
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
                    mover.StartMovingPlayer(hitInfo.point);
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