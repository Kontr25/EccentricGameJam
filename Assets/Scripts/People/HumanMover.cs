using System.Collections;
using Character;
using Environment;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody _humanRigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private CharacterMover _player;
        [SerializeField] private float _speed;
        [SerializeField] private float _patrolDelayValue;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private MapBorder _mapBorder;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private HumanAnimator _humanAnimator;

        private bool _runFromCharacter = false;
        private Vector3 _patrolPoint;
        private bool _isCanMove = true;
        [SerializeField] private bool _isPatrol;
        private Transform _moveTarget;
        private WaitForSeconds _patrolDelay;
        private WaitForSeconds _moveToCharacterDelay = new WaitForSeconds(.5f);
        private Coroutine _patrolRoutine;
        private Coroutine _moveToCharacterRoutine;


        private void Start()
        {
            _agent.speed = _speed;
            _patrolDelay = new WaitForSeconds(_patrolDelayValue);
            Patrol(true);
        }

        public CharacterMover Player
        {
            get => _player;
            set => _player = value;
        }

        public MapBorder BorderMap
        {
            get => _mapBorder;
            set => _mapBorder = value;
        }

        public float Speed
        {
            get => _speed;
            set
            {
                _agent.speed = value;
                _speed = value;
            }
        }

        public bool RunFromCharacter
        {
            get => _runFromCharacter;
            set
            {
                if (value)
                {
                    _humanRigidbody.isKinematic = false;
                    _agent.enabled = false;
                    _collider.enabled = value;
                    Patrol(false);
                }
                else
                {
                    _humanRigidbody.isKinematic = true;
                    _collider.enabled = value;
                    _agent.enabled = true;
                    Patrol(true);
                }
                
                _runFromCharacter = value;
            }
        }

        private void FixedUpdate()
        {
            if (_runFromCharacter && _player != null)
            {
                Vector3 direction = _player.transform.position - transform.position;
                _humanRigidbody.velocity = -direction.normalized * _speed;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(-direction),
                    Time.deltaTime * _rotationSpeed);
                _humanAnimator.Move(_humanRigidbody.velocity.magnitude);
            }
            else
            {
                _humanAnimator.Move(_agent.velocity.magnitude);
            }
        }

        public void MoveTo(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public void Patrol(bool value)
        {
            switch (value)
            {
                case true:
                    _isPatrol = true;
                    _patrolRoutine = StartCoroutine(Patrol());
                    break;
                case false:
                    _isPatrol = false;
                    if (_agent.enabled)
                    {
                        _agent.ResetPath();
                    }
                    if (_patrolRoutine != null)
                    {
                        StopCoroutine(_patrolRoutine);
                    }
                    break;
            }
        }

        private IEnumerator Patrol()
        {
            while (_isPatrol)
            {
                _patrolPoint = _mapBorder.RandomPosition();
                MoveTo(_patrolPoint);
                yield return _patrolDelay;
            }
        }
    }
}
