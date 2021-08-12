using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;


namespace RPG.Cinematics {
    public class CinematicsControlRemover : MonoBehaviour
    {

        private GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            CallEvents();
            player = GameObject.FindWithTag(Tags.TAG_PLAYER);
        }

        //calls the events of the Director component
        private void CallEvents()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        //enable control to the player
        private void EnableControl(PlayableDirector pd)
        {
            
            player.GetComponent<PlayerController>().enabled = true;
        }

        //disable control to the player
        private void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<CombatScheduler>().StopAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
    }
}
