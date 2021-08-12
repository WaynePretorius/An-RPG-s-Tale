using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class FollowCamera : MonoBehaviour
    {
        //variables declared
        [Header("Camera Settings")]
        [SerializeField] private float rotateSpeed = 5f;

        // Update is called once per frame after any other update methods
        void LateUpdate()
        {
            RotatateCamera();
        }

        private void RotatateCamera()
        {
            float axesRotate = Input.GetAxis(Tags.AXIS_HORIZONTAL) * rotateSpeed * Time.deltaTime;
            Vector3 rotation = new Vector3(0, axesRotate, 0);
            transform.Rotate(rotation);
        }

    }
}