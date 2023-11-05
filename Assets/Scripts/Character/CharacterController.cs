using System.Collections;
using CharacterNecessity;
using DG.Tweening;
using Poop;
using UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _characterAnimator;
        [SerializeField] private CharacterRagdoll _characterRagdoll;
        [SerializeField] private CharacterMover _characterMover;
        [SerializeField] private PoopController _poopController;
        [SerializeField] private Transform _headPoint;
        [SerializeField] private AudioSource _hit;
        [SerializeField] private AudioSource _emotionalDamage;
        [SerializeField] private float _soundDelayValue;
        [SerializeField] private DamageWindow[] _damageWindows;
        [SerializeField] private GameObject[] _skins;
        [SerializeField] private GameObject[] _ragdollSkins;
        [SerializeField] private AudioClip[] _joke;
        [SerializeField] private CharacterNecessityUI _characterNecessityUI;
        [SerializeField] private Collider _collider;
        [SerializeField] private AudioSource _doPoopSound;
        [SerializeField] private AudioSource _poopSound;

        private bool _canListen = true;
        private Coroutine _hitRoutine;
        private WaitForSeconds _soundDelay;
        private bool _isCanJoke = false;
        private Coroutine _doPoopRoutine;
        private WaitForSeconds _poopDelay;
        private bool _isClear = false;

        public bool IsCanJoke
        {
            get => _isCanJoke;
            set => _isCanJoke = value;
        }

        public Transform HeadPoint => _headPoint;

        public bool IsClear
        {
            get => _isClear;
            set => _isClear = value;
        }

        public CharacterAnimator CharAnimator
        {
            get => _characterAnimator;
            set => _characterAnimator = value;
        }

        public CharacterNecessityUI characterNecessityUI
        {
            get => _characterNecessityUI;
            set => _characterNecessityUI = value;
        }

        public bool CanListen
        {
            get => _canListen;
            set
            {
                if (value)
                {
                    _collider.enabled = false;
                    _collider.enabled = true;
                }
                _canListen = value;
            }
        }

        private void Start()
        {
            _soundDelay = new WaitForSeconds(_soundDelayValue);
            _poopDelay = new WaitForSeconds(_doPoopSound.clip.length);
        }

        public void SetCharacterSkin(CharacterName name)
        {
            DisableAllSkins();
            
            switch (name)
            {
                case CharacterName.Eldar:
                    _skins[0].SetActive(true);
                    _ragdollSkins[0].SetActive(true);
                    _poopController.poopTalker.jokeSound(_joke[0]);
                    break;
                case CharacterName.Maloy:
                    _skins[1].SetActive(true);
                    _ragdollSkins[1].SetActive(true);
                    _poopController.poopTalker.jokeSound(_joke[1]);
                    break;
                case CharacterName.Leha:
                    _skins[2].SetActive(true);
                    _ragdollSkins[2].SetActive(true);
                    _poopController.poopTalker.jokeSound(_joke[2]);
                    break;
                case CharacterName.Kolya:
                    _skins[3].SetActive(true);
                    _ragdollSkins[3].SetActive(true);
                    _poopController.poopTalker.jokeSound(_joke[3]);
                    break;
                case CharacterName.Kirill:
                    _skins[4].SetActive(true);
                    _ragdollSkins[4].SetActive(true);
                    _poopController.poopTalker.jokeSound(_joke[4]);
                    break;
                case CharacterName.Idrak:
                    _skins[5].SetActive(true);
                    _ragdollSkins[5].SetActive(true);
                    _poopController.poopTalker.jokeSound(_joke[5]);
                    break;
            }
        }

        private void DisableAllSkins()
        {
            for (int i = 0; i < _skins.Length; i++)
            {
                _skins[i].SetActive(false);
                _ragdollSkins[i].SetActive(false);
            }
        }

        public void StartStandUp(Vector3 standUpPlace)
        {
            _characterMover.enabled = false;
            transform.DOLookAt(new Vector3(0f, transform.position.y, 0f), .3f);
            transform.DOMove(standUpPlace, .3f);
            _doPoopRoutine = StartCoroutine(DoPoop());
        }

        public void Hit()
        {
            if (_hitRoutine != null)
            {
                StopCoroutine(_hitRoutine);
            }
            _characterAnimator.Hit();
            _hitRoutine = StartCoroutine(HitSoundRoutine());
        }

        private IEnumerator HitSoundRoutine()
        {
            _hit.Play();
            yield return _soundDelay;
            _damageWindows[Random.Range(0, _damageWindows.Length)].CheckDamageWindow();
            _emotionalDamage.Play();
        }

        private IEnumerator DoPoop()
        {
            _characterAnimator.DoPoop();
            _doPoopSound.Play();
            yield return _poopDelay;
            _poopSound.Play();
            _poopController.gameObject.SetActive(true);
            _poopController.StandUp();
            _characterAnimator.gameObject.SetActive(false);
            _characterRagdoll.gameObject.SetActive(true);
        }
    }

    public enum CharacterName
    {
        None,
        Eldar,
        Maloy,
        Leha,
        Kolya,
        Kirill,
        Idrak
    }
}