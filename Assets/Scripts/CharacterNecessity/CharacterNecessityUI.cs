using System.Collections;
using People;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Character.CharacterController;

namespace CharacterNecessity
{
    public class CharacterNecessityUI : MonoBehaviour
    {
        public static CharacterNecessityUI Instance;

        [SerializeField] private Image _foodBar;
        [SerializeField] private Image _washBar;
        [SerializeField] private Image _brainBar;
        [SerializeField] private ParticleSystem _stink;
        [SerializeField] private CharacterController _characterController;

        private float _currentFillamountBrain = 0;
        private float _maxCountJokes;
        private float _currenntCountJokes = 0;
        private float _maxCountFood;
        private float _currenntCountFood = 0;

        public float MaxCountFood
        {
            get => _maxCountFood;
            set => _maxCountFood = value;
        }

        public float maxCountJokes
        {
            get => _maxCountJokes;
            set => _maxCountJokes = value;
        }

        public Image WashBar => _washBar;

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
            _maxCountFood = FoodManager.Instance.Foods.Count;
        }

        public void Eat()
        {
            float part = 1 / _maxCountFood;
            StartCoroutine(smoothFood(_currenntCountFood, (_currenntCountFood + part), 0.5f));
        }
        
        public void JokeIsListened()
        {
            float part = 1 / _maxCountJokes;
            _currentFillamountBrain += part;
            StartCoroutine(smoothBrain(_currenntCountJokes, _currentFillamountBrain, 0.5f));
        }
        

        private IEnumerator smoothFood (float from, float to, float timer)
        {
            float t = 0.0f;
                 
            _foodBar.fillAmount = from;

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                _foodBar.fillAmount =  Mathf.Lerp (from, to, t);  

                yield return 0;
            }

            _currenntCountFood = to;
            
            if (_foodBar.fillAmount >= 0.99f)
            {
                CheckReadiness();
            }
        }
        
        private IEnumerator smoothBrain (float from, float to, float timer)
        {
            float t = 0.0f;
                 
            _brainBar.fillAmount = from;

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                _brainBar.fillAmount =  Mathf.Lerp (from, to, t);  

                yield return 0;
            }

            _currenntCountJokes = to;
            
            if (_brainBar.fillAmount >= 0.99f)
            {
                CheckReadiness();
            }
        }
        public IEnumerator smoothWash (float to, float timer, CharacterController character)
        {
            float t = 0.0f;
            float from = _washBar.fillAmount;

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                _washBar.fillAmount =  Mathf.Lerp (from, to, t);  

                yield return 0;
            }

            if (_washBar.fillAmount > 0)
            {
                _stink.Stop();
                CheckReadiness();
            }
        }

        private void CheckReadiness()
        {
            if (_washBar.fillAmount > 0.99f && _foodBar.fillAmount > 0.99f &&
                _brainBar.fillAmount > 0.99f)
            {
                _characterController.IsCanJoke = true;
                PeopleSpawner.Instance.GoOnShow();
            }
        }
    }
}