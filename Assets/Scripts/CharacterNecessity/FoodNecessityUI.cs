using System.Collections;
using People;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Character.CharacterController;

namespace CharacterNecessity
{
    public class FoodNecessityUI : MonoBehaviour
    {
        public static FoodNecessityUI Instance;

        [SerializeField] private Image _foodIcon;
        [SerializeField] private Image _washIcon;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _character;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _speed;
        [SerializeField] private ParticleSystem _stink;

        private float _maxCountFood;
        private float _currenntCountFood = 0;

        public float MaxCountFood
        {
            get => _maxCountFood;
            set => _maxCountFood = value;
        }

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

        private void Update()
        {
            if (_mainCamera != null)
            {
                Vector3 targetPosition = new Vector3(_character.position.x -1f, _character.position.y + 2f, _character.position.z);
                transform.position = Vector3.Lerp (transform.position, targetPosition, _speed * Time.deltaTime);
                
                Vector3 _direction = _mainCamera.transform.position - transform.position;
            
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(_direction.normalized),
                    Time.deltaTime * _rotationSpeed);
            }
        }

        public void Eat()
        {
            float part = 1 / _maxCountFood;
            StartCoroutine(smoothFood(_currenntCountFood, (_currenntCountFood + part), 0.5f));
        }
        

        private IEnumerator smoothFood (float from, float to, float timer)
        {
            float t = 0.0f;
                 
            _foodIcon.fillAmount = from;

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                _foodIcon.fillAmount =  Mathf.Lerp (from, to, t);  

                yield return 0;
            }

            _currenntCountFood = to;
            
            if (_foodIcon.fillAmount >= 0.99f)
            {
                _foodIcon.transform.parent.gameObject.SetActive(false);
                CheckReadiness();
            }
        }

        public IEnumerator smoothWash (float to, float timer, CharacterController character)
        {
            float t = 0.0f;
            float from = _washIcon.fillAmount;

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                _washIcon.fillAmount =  Mathf.Lerp (from, to, t);  

                yield return 0;
            }

            if (_washIcon.fillAmount > 0)
            {
                _washIcon.transform.parent.gameObject.SetActive(false);
                _stink.Stop();
                CheckReadiness();
            }
        }

        private void CheckReadiness()
        {
            if (!_washIcon.transform.parent.gameObject.activeInHierarchy &&
                !_foodIcon.transform.parent.gameObject.activeInHierarchy)
            {
                PeopleSpawner.Instance.GoOnShow();
            }
        }
    }
}