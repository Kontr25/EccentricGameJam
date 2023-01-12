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
            if (other.TryGetComponent(out Character.CharacterController character) && character.IsCanJoke)
            {
                if (_unloadRoutine != null)
                {
                    StopCoroutine(_unloadRoutine);
                }
                character.CharAnimator.IsWashing(true);
                _loadRoutine = StartCoroutine(FoodNecessityUI.Instance.smoothWash( 1f, 2f, character));
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
                _unloadRoutine = StartCoroutine(FoodNecessityUI.Instance.smoothWash( 0, 1f, character));
            }
        }
    }
}