using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float waypointSize = .25f;
        //draws the gizmos
        private void OnDrawGizmos()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.magenta;
                int j = nextWaypoint(i);
                Transform waypoint = GetWaypoint(i);
                Gizmos.DrawSphere(waypoint.position, waypointSize);
                Gizmos.color = Color.black;
                Gizmos.DrawLine(GetWaypoint(i).position, GetWaypoint(j).position);
            }
        }

        public int nextWaypoint(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Transform GetWaypoint(int i)
        {
            return transform.GetChild(i);
        }
    }
}
