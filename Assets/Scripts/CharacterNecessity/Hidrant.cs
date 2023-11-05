using UnityEngine;

namespace CharacterNecessity
{
    public class Hidrant : MonoBehaviour
    {
        [SerializeField] private float _distributionDelay;
        
        private Coroutine _loadRoutine;
        private Coroutine _unloadRoutine;
        private WaitForSeconds _delay;

        private void Start()
        {
            _delay = new WaitForSeconds(_distributionDelay);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character.CharacterController character))
            {
                if (_unloadRoutine != null)
                {
                    StopCoroutine(_unloadRoutine);
                }
                character.CharAnimator.IsWashing(true);
                _loadRoutine = StartCoroutine(CharacterNecessityUI.Instance.smoothWash( 1f, 4f, character));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character.CharacterController character))
            {
                if (_loadRoutine != null)
                {
                    StopCoroutine(_loadRoutine);
                }
                character.CharAnimator.IsWashing(false);
                if (CharacterNecessityUI.Instance.WashBar.fillAmount < 0.99f)
                {
                    _unloadRoutine = StartCoroutine(CharacterNecessityUI.Instance.smoothWash( 0, 1f, character));
                }
            }
        }
    }
}