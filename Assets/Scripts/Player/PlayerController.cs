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
            LookForEnemy();
            LookForMoveMouseClick();
        }

        //see if the enemy was clicked on
        private void LookForEnemy()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

                foreach (RaycastHit hit in hits)
                {
                    EnemyTarget target = hit.collider.gameObject.GetComponent<EnemyTarget>();

                    if (target == null) { continue; }

                        fighter.Attack(target);
                    
                }
            }
        }

        //moves the player to a target location
        private void LookForMoveMouseClick()
        {
            if (Input.GetMouseButton(0))
            {
                DetectCursorInputandMovePlayer();
            }
        }

        //detects where the mouse clicked and set that as the players destination
        private void DetectCursorInputandMovePlayer()
        {
            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);
            if (hasHit)
            {
                mover.MovePlayer(hitInfo.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}