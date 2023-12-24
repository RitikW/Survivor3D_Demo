using CameraScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public Animator anim;
        public float moveSpeed = 6.0F;

        private PlayerInput playerInput;
        private CharacterController characterController;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            Camera m_MainCamera = Camera.main;
            CameraFollow cameraFollow = m_MainCamera.GetComponent<CameraFollow>();
            cameraFollow.target = gameObject;
        }

        void FixedUpdate()
        {
            // for JoyStick inputs
            Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x * moveSpeed, 0, input.y * moveSpeed);
            if(anim!= null)
            {
                anim.SetFloat("Forward", input.y);
                anim.SetFloat("Turn", input.x);
            }

            characterController.Move(move * Time.deltaTime);
        }
    }
}

