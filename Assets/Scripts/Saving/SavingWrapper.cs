using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        //variables declared
        const string defaultSaveFile = "quicksave";
        private string fileName = "autoSave";
        private float fadeInTime = 2f;

        private SavingSystem save;

        //first fram of the object
        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.ImmediateWhite();
            save = GetComponent<SavingSystem>();
            yield return save.LoadLastScene(fileName);
            yield return fader.FadeIn(fadeInTime);
        }

        // Update is called once per frame
        void Update()
        {
            SaveToFile(defaultSaveFile);
            LoadFromFile(defaultSaveFile);
        }

        //Check keybindings for saving
        private void SaveToFile(string saveFile)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save(saveFile);
            }
        }

        //saves the file under the given filename
        public void Save(string saveFile)
        {
            save.Save(saveFile);
        }

        //Check keybindings for loading
        private void LoadFromFile(string saveFile)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load(saveFile);
            }
        }

        //Loads the file under correct filename
        public void Load(string saveFile)
        {
            save.Load(saveFile);
        }
    }
}