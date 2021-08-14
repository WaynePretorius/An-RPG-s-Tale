using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PersistantObject : MonoBehaviour
    {
        //variables cached
        [Header("Pers Obj Prefabs")]
        [SerializeField] private GameObject persistanPrefab;

        //states
        private static bool hasSpawned = false;

        private void Awake()
        {
            CheckIfSpawned();
        }

        private void CheckIfSpawned()
        {
            if (hasSpawned) { return; }

            SpawnPersistantObj();

            hasSpawned = true;
        }

        private void SpawnPersistantObj()
        {
            GameObject persistantObj = Instantiate(persistanPrefab);
            DontDestroyOnLoad(persistantObj);
        }
    }
}
