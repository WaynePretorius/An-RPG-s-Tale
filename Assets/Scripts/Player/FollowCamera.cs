using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        // Update is called once per frame
        void LateUpdate()
        {
            FollowPlayer();
        }

        //follows the player
        private void FollowPlayer()
        {
            transform.position = target.position;
        }

    }
}