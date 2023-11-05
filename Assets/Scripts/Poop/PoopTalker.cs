using System.Collections;
using CameraChanger;
using DG.Tweening;
using People;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Poop
{
    public class PoopTalker : MonoBehaviour
    {
        [SerializeField] private GameObject _openEyes;
        [SerializeField] private GameObject _closeEyes;
        [SerializeField] private GameObject[] _mouth;
        [SerializeField] private GameObject _laughMouth;
        [SerializeField] private float _mouthChangeDelayValue;
        [SerializeField] private float _laughMouthMoveDelayValue;
        [SerializeField] private float _jokeDurationValue;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _jokeSound;
        [SerializeField] private PeopleSpawner _peopleSpawner;

        private int _prewiousMouthNumber;
        private int _currentMouthNumber;
        private Coroutine _jokeRoutine;
        private Coroutine _laughRoutine;
        private WaitForSeconds _jokeDuration;
        private WaitForSeconds _mouthChangeDelay;
        private WaitForSeconds _laughMouthMoveDelay;
        private Vector3 _laughMothDefaultLocalPosition;

        public void jokeSound(AudioClip clip)
        {
            _jokeDuration = new WaitForSeconds(clip.length);
            _jokeSound.clip = clip;
        }

        private void Start()
        {
            _laughMothDefaultLocalPosition = _laughMouth.transform.localPosition;
            _mouthChangeDelay = new WaitForSeconds(_mouthChangeDelayValue);
            _laughMouthMoveDelay = new WaitForSeconds(_laughMouthMoveDelayValue + .1f);
        }

        public void StartJoke()
        {
            _jokeSound.Play();
            StartCoroutine(TalkSomething());
        }

        private IEnumerator TalkSomething()
        {
            StopJokeRoutine();
            _jokeRoutine = StartCoroutine(Joke());
            yield return _jokeDuration;
            StopJokeRoutine();
            _laughRoutine = StartCoroutine(Laugh());
        }

        private void StopJokeRoutine()
        {
            if (_jokeRoutine != null)
            {
                StopCoroutine(_jokeRoutine);
            }
        }

        private IEnumerator Joke()
        {
            while (true)
            {
                while (_prewiousMouthNumber == _currentMouthNumber)
                {
                    _currentMouthNumber = Random.Range(0, _mouth.Length);
                }

                DisableAllMouth();
                
                _mouth[_currentMouthNumber].SetActive(true);
                _prewiousMouthNumber = _currentMouthNumber;
                yield return _mouthChangeDelay;
            }
        }
        
        private IEnumerator Laugh()
        {
            
            DisableAllMouth();
            CameraChanger.CameraChanger.Instance.ActivateCamera(NameCamera.PeopleCamera);
            _audioSource.Play();
            _openEyes.SetActive(false);
            _closeEyes.SetActive(true);
            _laughMouth.SetActive(true);
            for (int i = 0; i < _peopleSpawner.Humans.Count; i++)
            {
                _peopleSpawner.Humans[i].humanAnimator.Clamp();
            }
            Invoke(nameof(RagdoolCamera), 4f);
            while (true)
            {
                _laughMouth.transform.DOLocalMoveY(_laughMouth.transform.localPosition.y + 0.1f, _laughMouthMoveDelayValue/2).onComplete = () =>
                {
                    _laughMouth.transform.DOLocalMoveY(_laughMouth.transform.localPosition.y - 0.1f, _laughMouthMoveDelayValue/2).onComplete = () =>
                    {
                        _laughMouth.transform.localPosition = _laughMothDefaultLocalPosition;
                    };
                };
                yield return _laughMouthMoveDelay;
            }
        }

        private void RagdoolCamera()
        {
            CameraChanger.CameraChanger.Instance.ActivateCamera(NameCamera.RagdollCamera);
        }

        private void DisableAllMouth()
        {
            for (int i = 0; i < _mouth.Length; i++)
            {
                _mouth[i].SetActive(false);
            }
        }
    }
}