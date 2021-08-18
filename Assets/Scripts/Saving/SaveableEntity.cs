using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Control;
using System;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [Header("Entity Settings")]
        [SerializeField] private string uniqueID = "";

        private static Dictionary<string, SaveableEntity> globalLookUp = new Dictionary<string, SaveableEntity>();

        //get the unique identifyer that is generated
        public string GetUniqueIdentifier()
        {
            return uniqueID;
        }

        //capture the state of the entity
        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();

            foreach(ISavable savable in GetComponents<ISavable>())
            {
                state[savable.GetType().ToString()] = savable.CaptureState();
            }

            return state;
            
        }

        //restore the state of the entity
        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;

            foreach(ISavable saveAble in GetComponents<ISavable>())
            {
                string typeString = saveAble.GetType().ToString();
                if (stateDict.ContainsKey(typeString))
                {
                    saveAble.RestoreState(stateDict[typeString]);
                }
            }

        }

        //only in edit mode
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) { return; }
            if (string.IsNullOrEmpty(gameObject.scene.path)) { return; }
            EditModeGenerateUniqueIDInScene();
        }

        //generate the unique identifyer and save it to the entity
        private void EditModeGenerateUniqueIDInScene()
        {
            SerializedObject serialObject = new SerializedObject(this);
            SerializedProperty serialProp = serialObject.FindProperty(Tags.EDIT_ID);
            print("");

            if(string.IsNullOrEmpty(serialProp.stringValue) || !IsUnique(serialProp.stringValue))
            {
                serialProp.stringValue = System.Guid.NewGuid().ToString();
                serialObject.ApplyModifiedProperties();
            }

            globalLookUp[serialProp.stringValue] = this;
        }

        //see if the unique id exits, and if so, if it is this ibjects id
        private bool IsUnique(string compare)
        {
            if (!globalLookUp.ContainsKey(compare)) { return true; }

            if (globalLookUp[compare] == this) { return true; }

            if (globalLookUp[compare] == null)
            {
                globalLookUp.Remove(compare);
                return true;
            }

            if(globalLookUp[compare].GetUniqueIdentifier() != compare)
            {
                globalLookUp.Remove(compare);
                return true;
            }

            return false;
        }

#endif
    }
}
