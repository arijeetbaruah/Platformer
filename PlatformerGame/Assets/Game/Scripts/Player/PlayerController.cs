using System;
using FMOD.Studio;
using FMODUnity;
using PG.Audio;
using PG.Input;
using PG.Service;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using STOP_MODE = FMOD.Studio.STOP_MODE;

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
        
        [Title("Audio")]
        [SerializeField] private EventReference walkingEvent;
        [SerializeField] private EventReference jumpStartEvent;
        [SerializeField] private EventReference jumpLandEvent;
        
        [SerializeField] private PlayerAnimationController playerAnimationController;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _movement;

        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;
        private bool _isGrounded = true;
        private bool _isWalking = false;
        private bool _isAttacking = false;
        private EventInstance _walkingInstance;

        public bool IsGrounded => _isGrounded;
        public bool IsAttacking => _isAttacking;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            TryGetComponent(out playerAnimationController);
            
            _walkingInstance = ServiceManager.Get<AudioService>().CreateInstance(walkingEvent);
            
            ServiceManager.Get<AudioService>().StopAndReleaseMusic();
        }

        private void OnDestroy()
        {
            _walkingInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _walkingInstance.release();
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
            
            _rigidbody.linearVelocityX = _movement.x * movementSpeed * Time.deltaTime;
            if (_movement.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (_movement.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private void FixedUpdate()
        {
            var oldState = _isGrounded;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
            _isGrounded = hit.collider != null;
            if (oldState != _isGrounded)
            {
                if (IsGrounded)
                {
                    ServiceManager.Get<AudioService>().PlayOnce(jumpLandEvent);
                }
                else
                {
                    ServiceManager.Get<AudioService>().PlayOnce(jumpLandEvent);
                }
            }
            
            if (IsGrounded)
            {
                var platformType = hit.transform.GetComponent<PG.Framework.Platform>().platformType;
                _coyoteTimeCounter = coyoteTime;
                _walkingInstance.setParameterByName("PlatformType", (float)platformType);
            }
            else
            {
                _coyoteTimeCounter -= Time.fixedDeltaTime;
            }
        }

        private void LateUpdate()
        {
            bool oldState = _isWalking;
            _isWalking = _rigidbody.linearVelocity.x != 0 && _rigidbody.linearVelocity.y == 0;

            if (oldState != _isWalking)
            {
                if ( _isWalking && IsGrounded)
                {
                    _walkingInstance.start();
                }
                else
                {
                    _walkingInstance.stop(STOP_MODE.IMMEDIATE);
                }
            }
        }

        private void OnEnable()
        {
            IS.Player.Enable();

            IS.Player.Move.performed += OnMoveStart;
            IS.Player.Move.canceled += OnMoveEnd;

            IS.Player.Attack.performed += OnAttack;

            IS.Player.Jump.performed += OnJump;
        }

        private void OnDisable()
        {
            if (ServiceManager.Instance != null)
            {
                IS.Player.Move.performed -= OnMoveStart;
                IS.Player.Move.canceled -= OnMoveEnd;

                IS.Player.Attack.performed -= OnAttack;

                IS.Player.Jump.performed -= OnJump;

                IS.Player.Disable();
            }
        }

        private void OnAttack(InputAction.CallbackContext obj)
        {
            if (!IsGrounded || _isAttacking) return;

            _rigidbody.linearVelocityX = 0;
            _isAttacking = true;
            playerAnimationController.Attack(() =>
            {
                _isAttacking = false;
            });
        }

        private void OnMoveStart(InputAction.CallbackContext obj)
        {
            if (_isAttacking) return;

            _movement.x = obj.ReadValue<Vector2>().x;
        }

        private void OnMoveEnd(InputAction.CallbackContext obj)
        {
            _movement.x = 0;
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            if (_isAttacking) return;

            _jumpBufferCounter = jumpBufferTime;
        }
    }
}
