using System;
using Character;
using DefaultNamespace;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace People
{
    public class HumanAngryTalkingZone : MonoBehaviour
    {
        [SerializeField] private Human _human;
        [SerializeField] private HumanAnimator humanAnimator;
        [SerializeField] private HumanMover humanMover;
        [SerializeField] private HumanDialogWindow _dialogWindowPrefab;
        [SerializeField] private Transform _dialogWindowPoint;

        private CharacterController _currentCharacter;
        private HumanDialogWindow _currentWindow;
        [SerializeField] private AudioSource mem;

        public HumanDialogWindow CurrentWindow
        {
            get => _currentWindow;
            set => _currentWindow = value;
        }

        private void Update()
        {
            if (_currentCharacter != null)
            {
                Vector3 direction = _currentCharacter.transform.position - humanMover.transform.position;
                
                humanMover.transform.rotation = Quaternion.Slerp(humanMover.transform.rotation,
                Quaternion.LookRotation(direction.normalized),
                Time.deltaTime * 10);
            }
        }

        private void SpawnDialogWindow()
        {
            _currentWindow = Instantiate(_dialogWindowPrefab, _dialogWindowPoint.position, Quaternion.identity);
            _currentWindow.MainCamera = CameraChanger.CameraChanger.Instance.AllCameras[0].transform;
            _currentWindow.AngryTalkingZone = this;
            _currentWindow.MoveTargetPoint = _dialogWindowPoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                _currentCharacter = character;
                CheckingForNull();
                if (mem.isPlaying)
                {
                    mem.Stop();
                }
                if (_currentWindow.TargetCharacter == null)
                {
                    _currentWindow.TargetCharacter = character;
                }
                if (_human.HumanType == HumanType.FirstManType || _human.HumanType == HumanType.SecondManType)
                {
                    mem.clip = PeopleSoundManager.Instance.RandomManSound();
                }
                else if (_human.HumanType == HumanType.FirstWomanType || _human.HumanType == HumanType.SecondWomanType)
                {
                    mem.clip = PeopleSoundManager.Instance.RandomWomanSound();
                }
                mem.Play();
                _currentWindow.LoadWindow(mem.clip.length);
                humanAnimator.AngryTalking();
                humanMover.StopMove();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                _currentCharacter = null;
                CheckingForNull();
                if (_currentWindow.TargetCharacter == null)
                {
                    _currentWindow.TargetCharacter = character;
                }
                _currentWindow.UnLoadWindow();
                humanAnimator.StopAngryTalking();
                humanMover.StartMove();
            }
        }

        private void CheckingForNull()
        {
            if (_currentWindow == null)
            {
                SpawnDialogWindow();
            }
        }
    }
}