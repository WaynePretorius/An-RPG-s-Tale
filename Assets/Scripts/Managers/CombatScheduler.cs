using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class CombatScheduler : MonoBehaviour
    {
        private Iaction currentAction;

        public void StartAction(Iaction action)
        {
            if(currentAction == action) { return; }
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        public void StopAction()
        {
            StartAction(null);
        }
    }
}