using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2.0f;
        
        [SerializeField]
        private float groundSpeedMultiplier = 10.0f;
        
        [SerializeField]
        private float airSpeedMultiplier = 2.0f;

        [SerializeField]
        private float jumpForce = 10.0f;

        [SerializeField]
        private float groundDrag = 2.0f;
        
        [SerializeField]
        private float airDrag = 1.0f;

        private Transform myTransform;
        private Rigidbody myRigidbody;
        private InputReceiver inputReceiver;

        private bool isPaused;

        private Vector3 moveVector;
        private bool isGrounded;

        private float verticalAxis;
        private float horizontalAxis;
        private bool jumpAction;

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
            myRigidbody = GetComponent<Rigidbody>();
            inputReceiver = GetComponent<InputReceiver>();
        }

        private void OnEnable()
        {
            GameManager.OnPauseGameplayEvent += OnPauseGameplayHandler;
            GameManager.OnResumeGameplayEvent += OnResumeGameplayHandler;
        }

        private void OnDisable()
        {
            GameManager.OnPauseGameplayEvent -= OnPauseGameplayHandler;
            GameManager.OnResumeGameplayEvent -= OnResumeGameplayHandler;
        }

        private void OnPauseGameplayHandler()
        {
            isPaused = true;
        }

        private void OnResumeGameplayHandler()
        {
            isPaused = false;
        }

        private void Start()
        {
            myRigidbody.freezeRotation = true;
            isPaused = true;
        }

        void Update()
        {
            GetInput();
            CalculateIsGrounded();
            CalculateAndSetDrag();
            CalculateMoveVector();
            HandleJumping();
        }

        private void FixedUpdate()
        {
            float speedMultiplier = isGrounded ? groundSpeedMultiplier : airSpeedMultiplier;
            myRigidbody.AddForce(speed * speedMultiplier * moveVector, ForceMode.Acceleration);
        }

        private void GetInput()
        {
            if (inputReceiver == null || isPaused)
                return;

            verticalAxis = inputReceiver.VerticalAxisValue;
            horizontalAxis = inputReceiver.HorizontalAxisValue;
            jumpAction = inputReceiver.WasJumpKeyPressed;
        }

        private void CalculateIsGrounded()
        {
            isGrounded = Physics.Raycast(myTransform.position, Vector3.down, 1.1f);
        }

        private void CalculateAndSetDrag()
        {
            myRigidbody.drag = isGrounded ? groundDrag : airDrag;
        }

        private void CalculateMoveVector()
        {
            moveVector = verticalAxis * myTransform.forward + horizontalAxis * myTransform.right;
        }

        private void HandleJumping()
        {
            if (isGrounded && jumpAction)
            {
                myRigidbody.AddForce(jumpForce * myTransform.up, ForceMode.Impulse);
            }
        }
    }
}
