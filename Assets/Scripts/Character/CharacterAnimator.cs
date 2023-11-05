using UnityEngine;

namespace Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _characterRigidbody;
        private int _hitNumber = 0;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int RunJump = Animator.StringToHash("RunJump");
        private static readonly int StayJump = Animator.StringToHash("StayJump");
        private static readonly int DoPoopTrigger = Animator.StringToHash("DoPoop");
        private static readonly int IsWash = Animator.StringToHash("IsWash");
        private static readonly int Wash = Animator.StringToHash("Wash");
        private static readonly int Hit1 = Animator.StringToHash("Hit1");
        private static readonly int Hit2 = Animator.StringToHash("Hit2");
        private static readonly int EldarStartPose = Animator.StringToHash("EldarStartPose");
        private static readonly int MaloyStartPose = Animator.StringToHash("MaloyStartPose");
        private static readonly int LehaStartPose = Animator.StringToHash("LehaStartPose");
        private static readonly int KolyaStartPose = Animator.StringToHash("KolyaStartPose");
        private static readonly int KirillStartPose = Animator.StringToHash("KirillStartPose");
        private static readonly int IdrakStartPose = Animator.StringToHash("IdrakStartPose");
        private static readonly int Play = Animator.StringToHash("StartPlay");

        private void Start()
        {
            _animator.SetTrigger(EldarStartPose);
        }

        private void FixedUpdate()
        {
            _animator.SetFloat(Speed, _characterRigidbody.velocity.magnitude);
        }

        public void Jump()
        {
            if (_characterRigidbody.velocity.magnitude > 0)
            {
                _animator.SetTrigger(RunJump);
            }
            else
            {
                _animator.SetTrigger(StayJump);
            }
        }

        public void DoPoop()
        {
            _animator.SetTrigger(DoPoopTrigger);
        }

        public void IsWashing(bool value)
        {
            if (!value)
            {
                _animator.SetBool(IsWash, value);
            }
            else
            {
                _animator.SetBool(IsWash, value);
                _animator.SetTrigger(Wash);
            }
        }

        public void Hit()
        {
            if (_hitNumber == 0)
            {
                _hitNumber = 1;
                _animator.SetTrigger(Hit1);
            }
            else
            {
                _animator.SetTrigger(Hit2);
                _hitNumber = 0;
            }
            
        }

        public void SetPose(CharacterName name)
        {
            switch (name)
            {
                case CharacterName.Eldar:
                    _animator.SetTrigger(EldarStartPose);
                    break;
                case CharacterName.Maloy:
                    _animator.SetTrigger(MaloyStartPose);
                    break;
                case CharacterName.Leha:
                    _animator.SetTrigger(LehaStartPose);
                    break;
                case CharacterName.Kolya:
                    _animator.SetTrigger(KolyaStartPose);
                    break;
                case CharacterName.Kirill:
                    _animator.SetTrigger(KirillStartPose);
                    break;
                case CharacterName.Idrak:
                    _animator.SetTrigger(IdrakStartPose);
                    break;
            }
        }

        public void StartPlay()
        {
            _animator.SetTrigger(Play);
        }
    }
}