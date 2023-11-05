using UnityEngine;

namespace Character
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody _characterRigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private CharacterAnimator _characterAnimator;
        
        private Ray _ray;
        private RaycastHit _hit;
        private bool _isCanJump;
        private bool _isCanMove = false;

        public bool IsCanMove
        {
            get => _isCanMove;
            set
            {
                if (!value)
                {
                    _characterRigidbody.velocity = Vector3.zero;
                }
                _isCanMove = value;
            }
        }

        protected bool CanJump()
        {
            Vector3 rayPosirion = new Vector3(_characterTransform.position.x, _characterTransform.position.y + .5f,
                _characterTransform.position.z);
            _ray = new Ray(rayPosirion, Vector3.down);
            return Physics.Raycast(_ray, out _hit, 1f, _targetLayer);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && CanJump() && _isCanMove)
            {
                Jump();
            }
        }

        public void FixedUpdate()
        {
            if (_isCanMove)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                Vector3 velocity = new Vector3(h, 0, v) * _speed;
                velocity.y = _characterRigidbody.velocity.y;
                _characterRigidbody.velocity = velocity;
            
                Vector3 _direction = new Vector3(h, 0f, v);
                _direction = _direction.normalized;
                _direction = new Vector3(_direction.x, 0, _direction.z);
                if (_direction != Vector3.zero)
                {
                    _characterTransform.rotation = Quaternion.Slerp(_characterTransform.rotation,
                        Quaternion.LookRotation(_direction),
                        Time.deltaTime * _rotationSpeed);
                }
            }
        }

        public void Jump()
        {
            _characterAnimator.Jump();
            _characterRigidbody.velocity += Vector3.up * _jumpSpeed;
        }
    }
}