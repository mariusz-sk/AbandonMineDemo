using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraTransform;

        [SerializeField]
        private float xSensitivity = 5.0f;

        [SerializeField]
        private float ySensitivity = 5.0f;

        private Transform myTransform;
        private InputReceiver inputReceiver;

        private float mouseX;
        private float mouseY;
        private float cameraPitch = 0.0f;

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
            inputReceiver = GetComponent<InputReceiver>();

            if (cameraTransform == null)
            {
                Camera camera = GetComponentInChildren<Camera>();
                if (camera != null)
                {
                    cameraTransform = camera.GetComponent<Transform>();
                }
            }
        }

        void Update()
        {
            GetInput();

            RotatePlayer();
        }

        private void GetInput()
        {
            if (inputReceiver == null)
                return;

            mouseX = inputReceiver.MouseXAxisValue;
            mouseY = inputReceiver.MouseYAxisValue;
        }

        private void RotatePlayer()
        {
            myTransform.Rotate(Vector3.up, mouseX * xSensitivity);

            if (cameraTransform != null)
            {
                cameraPitch += mouseY * ySensitivity;
                cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

                Quaternion xRotation = Quaternion.Euler(-cameraPitch, 0.0f, 0.0f);
                cameraTransform.localRotation = xRotation;
            }
        }
    }
}