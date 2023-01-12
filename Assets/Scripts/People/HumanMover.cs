using System.Collections;
using Character;
using Environment;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanMover : MonoBehaviour
    {
        [SerializeField] private CharacterMover _player;
        [SerializeField] private float _speed;
        [SerializeField] private float _patrolDelayValue;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private MapBorder _mapBorder;

        private Vector3 _patrolPoint;
        private bool _isCanMove = true;
        private bool _isPatrol = true;
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
            set
            {
                if (value != null)
                {
                    Patrol(false);
                    _moveTarget = value.transform;
                    StopMoveToCharacter();
                    _moveToCharacterRoutine = StartCoroutine(MoveToCharacter());
                }
                else
                {
                    StopMoveToCharacter();
                    Patrol(true);
                }
                _player = value;
            }
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

        public void MoveTo(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        private IEnumerator MoveToCharacter()
        {
            while (true)
            {
                MoveTo(_moveTarget.position);
                yield return _moveToCharacterDelay;
            }
        }

        private void StopMoveToCharacter()
        {
            if (_moveToCharacterRoutine != null)
            {
                StopCoroutine(_moveToCharacterRoutine);
                _moveToCharacterRoutine = null;
            }
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
                    if (_patrolRoutine != null)
                    {
                        StopCoroutine(_patrolRoutine);
                    }
                    _agent.ResetPath();
                    break;
            }
        }

        private IEnumerator Patrol()
        {
            while (true)
            {
                _patrolPoint = _mapBorder.RandomPosition();
                MoveTo(_patrolPoint);
                yield return _patrolDelay;
            }
        }

        public void StopMove()
        {
            _agent.speed = 0;
        }

        public void StartMove()
        {
            _agent.speed = _speed;
        }
    }
}
