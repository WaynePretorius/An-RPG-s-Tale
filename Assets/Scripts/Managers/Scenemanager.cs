using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Scenemanager : MonoBehaviour
    {
        //variables decalred
        [SerializeField] private int sceneToLoad = -1;

        //caches Referenced
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PortalNumbers portalType;

        //when the collider has been triggered
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.TAG_PLAYER)
            {
                StartCoroutine(TransitionBetweenScenes());
            }
        }

        //keep the portal until after the next scene has loaded
        private IEnumerator TransitionBetweenScenes()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("No scene to load");
                yield break;
            }

            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Scenemanager otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            Destroy(gameObject);
        }

        //get the portal
        private Scenemanager GetOtherPortal()
        {
            foreach(Scenemanager portal in FindObjectsOfType<Scenemanager>())
            {
                if (portal == this) { continue; }
                if (portal.portalType != portalType) { continue; }

                return portal;
            }

            return null;
        }

        //move the player to the spawnPoint of the portal
        private void UpdatePlayer(Scenemanager otherPortal)
        {
            GameObject player = GameObject.FindWithTag(Tags.TAG_PLAYER);
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}

public enum PortalNumbers
{
    A, B, C, D, E
}