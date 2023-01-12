using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Character.CharacterController;

namespace Stage
{
    public class StandUpPlace : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
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
            if (other.TryGetComponent(out CharacterController character) && character.IsCanJoke)
            {
                if (_unloadRoutine != null)
                {
                    StopCoroutine(_unloadRoutine);
                }
                
                _loadRoutine = StartCoroutine(smoothVal(_progressBar.fillAmount, 1f, 2f, character));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterController character) && _progressBar.fillAmount > 0)
            {
                if (_loadRoutine != null)
                {
                    StopCoroutine(_loadRoutine);
                }

                _unloadRoutine = StartCoroutine(smoothVal(_progressBar.fillAmount, 0, 1f, character));
            }
        }

        private IEnumerator smoothVal (float from, float to, float timer, CharacterController character)
        {
            float t = 0.0f;
                 
            _progressBar.fillAmount = from;

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                _progressBar.fillAmount =  Mathf.Lerp (from, to, t);  

                yield return 0;
            }

            if (_progressBar.fillAmount > 0)
            {
                character.StartStandUp(transform.position);
            }
        }
    }
}