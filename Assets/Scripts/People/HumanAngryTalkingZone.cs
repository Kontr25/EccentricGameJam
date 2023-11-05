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
        [SerializeField] private Outline[] _humanOutLine;
        [SerializeField] private HumanAgroZone _humanAgroZone;

        private bool _canTalk = true;
        private CharacterController _currentCharacter;
        private HumanDialogWindow _currentWindow;
        [SerializeField] private AudioSource mem;

        public HumanDialogWindow CurrentWindow
        {
            get => _currentWindow;
            set => _currentWindow = value;
        }

        public bool CanTalk
        {
            get => _canTalk;
            set
            {
                if (!value)
                {
                    for (int i = 0; i < _humanOutLine.Length; i++)
                    {
                        _humanOutLine[i].enabled = value;
                    }
                    _humanAgroZone.gameObject.SetActive(false);
                    _currentCharacter = null;
                    humanAnimator.AngryTalking(false);
                    humanMover.RunFromCharacter = false;
                    humanMover.Speed = 1f;
                    humanMover.Player = null;
                    gameObject.SetActive(false);
                }
                
                _canTalk = value;
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
            if (other.TryGetComponent(out CharacterController character) && _canTalk && character.CanListen)
            {
                character.CanListen = false;
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
                if(mem.clip == null)
                {
                    if (_human.HumanType == HumanType.FirstManType || _human.HumanType == HumanType.SecondManType)
                    {
                        mem.clip = PeopleSoundManager.Instance.RandomManSound();
                    }
                    else if (_human.HumanType == HumanType.FirstWomanType || _human.HumanType == HumanType.SecondWomanType)
                    {
                        mem.clip = PeopleSoundManager.Instance.RandomWomanSound();
                    }
                    
                }
                mem.Play();
                _currentWindow.LoadWindow(mem.clip.length);
                humanAnimator.AngryTalking(true);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterController character) && _canTalk)
            {
                mem.Stop();
                if (_currentCharacter != null)
                {
                    _currentCharacter.CanListen = true;
                }
                _currentCharacter = null;
                CheckingForNull();
                if (_currentWindow.TargetCharacter == null)
                {
                    _currentWindow.TargetCharacter = character;
                }
                _currentWindow.UnLoadWindow();
                humanAnimator.AngryTalking(false);
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