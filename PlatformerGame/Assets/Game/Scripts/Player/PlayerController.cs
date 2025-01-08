using System;
using PG.Input;
using PG.Service;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PG.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private InputService IS => ServiceManager.Get<InputService>();
     
        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpPower = 10;
        [SerializeField] private float coyoteTime;
        [SerializeField] private float jumpBufferTime;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _movement;

        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;
        
        private bool IsGrounded => Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_jumpBufferCounter > 0)
            {
                _jumpBufferCounter -= Time.deltaTime;
            }
            
            if (_coyoteTimeCounter > 0 && _jumpBufferCounter > 0)
            {
                _rigidbody.linearVelocityY = jumpPower;
                _coyoteTimeCounter = 0;
                _jumpBufferCounter = 0;
            }
            
            transform.Translate(_movement * movementSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (IsGrounded)
            {
                _coyoteTimeCounter = coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.fixedDeltaTime;
            }
        }

        private void OnEnable()
        {
            IS.Player.Enable();

            IS.Player.Move.performed += OnMoveStart;
            IS.Player.Move.canceled += OnMoveEnd;

            IS.Player.Jump.performed += OnJump;
        }

        private void OnDisable()
        {
            IS.Player.Move.performed -= OnMoveStart;
            IS.Player.Move.canceled -= OnMoveEnd;

            IS.Player.Disable();
        }

        private void OnMoveStart(InputAction.CallbackContext obj)
        {
            _movement.x = obj.ReadValue<Vector2>().x;
        }

        private void OnMoveEnd(InputAction.CallbackContext obj)
        {
            _movement.x = 0;
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            _jumpBufferCounter = jumpBufferTime;
        }
    }
}
