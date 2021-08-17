using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private SavingSystem save;

        private void Start()
        {
            save = GetComponent<SavingSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            SaveToFile(defaultSaveFile);
            LoadFromFile(defaultSaveFile);
        }

        //saves to file
        private void SaveToFile(string saveFile)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                save.Save(saveFile);
            }
        }

        private void LoadFromFile(string saveFile)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                save.Load(saveFile);
            }
        }
    }
}