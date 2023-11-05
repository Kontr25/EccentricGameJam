using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace UIElements
{
    public class DamageWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _damageWindow;
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private float _emotionalDamageSound;
        [SerializeField] private float _moveDuration;
        [SerializeField] private Animator _animator;
        
        private Coroutine _checkWindowRoutine;
        private WaitForSeconds _duration;
        private WaitForSeconds _moveDelay;
        private Vector3 _defaultPosition;
        private static readonly int StartShaking = Animator.StringToHash("StartShaking");
        private static readonly int StopShaking = Animator.StringToHash("StopShaking");

        private void Start()
        {
            _defaultPosition = transform.position;
            _duration = new WaitForSeconds(_emotionalDamageSound);
            _moveDelay = new WaitForSeconds(_moveDuration);
        }

        public void CheckDamageWindow()
        {
            if (_checkWindowRoutine != null)
            {
               StopCoroutine(_checkWindowRoutine);
            }

            _checkWindowRoutine = StartCoroutine(CheckWindowRoutine());
        }

        private IEnumerator CheckWindowRoutine()
        {
            _damageWindow.SetActive(true);
            transform.DOMove(_targetPoint.position, _moveDuration);
            yield return _moveDelay;
            _animator.SetTrigger(StartShaking);
            yield return _duration;
            _animator.SetTrigger(StopShaking);
            transform.DOMove(_defaultPosition, _moveDuration).onComplete = () =>
            {
                _damageWindow.SetActive(false);
            };
        }
    }
}