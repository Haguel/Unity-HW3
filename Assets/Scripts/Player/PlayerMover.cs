using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _sprintSpeed;
        
        [Header("Jump Settings")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _maxGroundAngle;
        
        [Header("Rotation Settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private float _mouseSensitivity;
        [SerializeField] private float _yRotationRange;
        
        private float _verticalRotation;
        private bool _isGrounded;
        
        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            Rotate();
            Jump();
        }

        private void OnCollisionStay(Collision other)
        {
            var minYNormal = _maxGroundAngle * Mathf.Deg2Rad;

            foreach (var contact in other.contacts)
            {
                if (contact.normal.y <= minYNormal)
                {
                    _isGrounded = false;
                    return;
                }   
            }
            
            _isGrounded = true;
        }

        private void OnCollisionExit(Collision other)
        {
            _isGrounded = false;
        }

        private void Jump()
        {
            if (_playerInput.JumpInput && _isGrounded)
            {
                _rigidbody.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
            }
        }

        private void Rotate()
        {
            var horizontalRotation = _playerInput.MouseX * _mouseSensitivity;
            transform.Rotate(0f, horizontalRotation, 0f);
            
            _verticalRotation -= _playerInput.MouseY * _mouseSensitivity;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -_yRotationRange, _yRotationRange);
            _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private void Move()
        {
            var speed = _playerInput.SprintInput ? _sprintSpeed : _walkSpeed;

            var movementVector = new Vector3(_playerInput.HorizontalInput, 0, _playerInput.VerticalInput) 
                                 * (speed * Time.fixedDeltaTime);

            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(movementVector));
        }
    }
}
