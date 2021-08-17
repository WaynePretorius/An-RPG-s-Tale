using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Control;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string uniqueID = "";
        public string GetUniqueIdentifier()
        {
            return "";
        }

        public object CaptureState()
        {
            return new Vector3Serializer(transform.position);
        }

        public void RestoreState(object state)
        {
            Vector3Serializer unitPos = (Vector3Serializer)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = unitPos.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<CombatScheduler>().StopAction();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) { return; }
            if (string.IsNullOrEmpty(gameObject.scene.path)) { return; }
            EditModeGenerateUniqueIDInScene();
        }

        private void EditModeGenerateUniqueIDInScene()
        {
            SerializedObject serialObject = new SerializedObject(this);
            SerializedProperty serialProp = serialObject.FindProperty(Tags.EDIT_ID);
            print("");

            if(string.IsNullOrEmpty(serialProp.stringValue))
            {
                serialProp.stringValue = System.Guid.NewGuid().ToString();
                serialObject.ApplyModifiedProperties();
            }
        }

#endif
    }
}
