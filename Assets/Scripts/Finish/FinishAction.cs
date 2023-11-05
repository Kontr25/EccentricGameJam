using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Finish
{
    public class FinishAction : MonoBehaviour
    {
        public static FinishAction Instance;

        [SerializeField] private Transform _ragdollCamera;
        [SerializeField] private Rigidbody[] _smiles;
        [SerializeField] private Transform[] _finishText;
        [SerializeField] private Transform[] _endPointText;
        [SerializeField] private Transform _restartButton;
        [SerializeField] private float _moveDuradion;

        private Coroutine _moveTextRoutine;
        private WaitForSeconds _delay;

        private void Awake()
        {
            if (Instance == null)
            {
                transform.SetParent(null);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _delay = new WaitForSeconds(_moveDuradion);
        }

        public void Finish()
        {
            if (_moveTextRoutine != null)
            {
                StopCoroutine(_moveTextRoutine);
            }

            _moveTextRoutine = StartCoroutine(MoveTextRoutine());
        }

        private IEnumerator MoveTextRoutine()
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < _smiles.Length; i++)
            {
                Vector3 PositionNearCamera = new Vector3(_ragdollCamera.position.x + Random.Range(-2, +2),
                    _ragdollCamera.position.y + 2f,
                    _ragdollCamera.position.z + Random.Range(-2, +2));
                _smiles[i].transform.position = PositionNearCamera;
                _smiles[i].isKinematic = false;
            }
            for (int i = 0; i < _finishText.Length; i++)
            {
                _finishText[i].DOMove(_endPointText[i].position, _moveDuradion);
                yield return _delay;
            }

            yield return new WaitForSeconds(_moveDuradion * _finishText.Length);
            _restartButton.DOMove(_endPointText[_endPointText.Length - 1].position, _moveDuradion);
        }
    }
}