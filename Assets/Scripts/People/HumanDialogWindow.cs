using System.Collections;
using DG.Tweening;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace People
{
    public class HumanDialogWindow : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _bodyTarget;
        [SerializeField] private GameObject _destroyed;
        
        private Transform _mainCamera;
        private HumanAngryTalkingZone _humanAngryTalkingZone;
        private Coroutine _loadRoutine;
        private Coroutine _unloadRoutine;
        private WaitForSeconds _delay;
        private Character.CharacterController _character;
        private Transform _moveTargetPoint;

        public Transform MainCamera
        {
            get => _mainCamera;
            set => _mainCamera = value;
        }

        public HumanAngryTalkingZone AngryTalkingZone
        {
            get => _humanAngryTalkingZone;
            set => _humanAngryTalkingZone = value;
        }

        public CharacterController TargetCharacter
        {
            get => _character;
            set => _character = value;
        }

        public Transform MoveTargetPoint
        {
            get => _moveTargetPoint;
            set => _moveTargetPoint = value;
        }
        
        private void Start()
        {
            _delay = new WaitForSeconds(1f);
        }
        private void Update()
        {
            if (_mainCamera != null)
            {
                Vector3 _direction = _mainCamera.transform.position - transform.position;
                
                if (transform.position.x - _character.transform.position.x < 0)
                {
                    _direction = -_direction;
                }
                
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(_direction.normalized),
                    Time.deltaTime * _rotationSpeed);
            }

            if (_moveTargetPoint != null)
            {
                transform.position = Vector3.Lerp (transform.position, _moveTargetPoint.position, _speed * Time.deltaTime);
            }
        }

        public void LoadWindow(float loadtime)
        {
            if (_unloadRoutine != null)
            {
                StopCoroutine(_unloadRoutine);
            }
                
            _loadRoutine = StartCoroutine(smoothVal(transform.localScale.x, 1f, loadtime, _character));
        }
        
        public void UnLoadWindow()
        {
            if (_loadRoutine != null)
            {
                StopCoroutine(_loadRoutine);
            }

            _unloadRoutine = StartCoroutine(smoothVal(transform.localScale.x, 0.05f, 1, _character));
        }

        private IEnumerator smoothVal (float from, float to, float timer, Character.CharacterController character)
        {
            float t = 0.0f;
                 
            transform.localScale = new Vector3(from,from,from);

            while (t < 1.0f) {
                t += Time.deltaTime * (1.0f / timer);
     
                float scale =  Mathf.Lerp (from, to, t);
                transform.localScale = new Vector3(scale, scale, scale);

                yield return 0;
            }

            if (transform.localScale.x > 0.1f)
            {
                Shot();
            }
        }

        private void Shot()
        {
            _humanAngryTalkingZone.CanTalk = false;
            _character.characterNecessityUI.JokeIsListened();
            _body.DOLocalMove(_bodyTarget.localPosition, .3f);
            _humanAngryTalkingZone.CurrentWindow = null;
            _mainCamera = null;
            _moveTargetPoint = null;
            transform.SetParent(_character.HeadPoint);
            transform.DOLocalJump(Vector3.zero, .5f, 1, .5f).onComplete = () =>
            {
                _character.CanListen = true;
                _character.Hit();
                transform.SetParent(null);
                _body.gameObject.SetActive(false);
                _destroyed.SetActive(true);
            };
        }
    }
}