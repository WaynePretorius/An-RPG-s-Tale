using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class Cinematics : MonoBehaviour
    {
        //states
        private bool isDonePlaying = false;

        //when the player enters the trigger
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == Tags.TAG_PLAYER && !isDonePlaying)
            {
                GetComponent<PlayableDirector>().Play();
                isDonePlaying = true;
            }
        }
    }
}