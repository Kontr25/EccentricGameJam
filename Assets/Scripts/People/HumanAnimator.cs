using UnityEngine;

namespace People
{
    public class HumanAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _humanAnimator;
    
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int StopTalking = Animator.StringToHash("StopTalking");
        private static readonly int Happy1 = Animator.StringToHash("Happy1");
        private static readonly int Happy2 = Animator.StringToHash("Happy2");
        private static readonly int Happy3 = Animator.StringToHash("Happy3");
        private static readonly int Happy4 = Animator.StringToHash("Happy4");

        public void Move(float speed)
        {
            _humanAnimator.SetFloat(Speed, speed);
        }

        public void AngryTalking(bool value)
        {
            if (value)
            {
                _humanAnimator.SetLayerWeight(0,0);
                _humanAnimator.SetLayerWeight(1,1);
            }
            else
            {
                _humanAnimator.SetLayerWeight(0,1);
                _humanAnimator.SetLayerWeight(1,0);
            }
        }

        public void Clamp()
        {
            int happyNumber = Random.Range(1, 5);

            switch (happyNumber)
            {
                case 1:
                    _humanAnimator.SetTrigger(Happy1);
                    break;
                case 2:
                    _humanAnimator.SetTrigger(Happy2);
                    break;
                case 3:
                    _humanAnimator.SetTrigger(Happy3);
                    break;
                case 4:
                    _humanAnimator.SetTrigger(Happy4);
                    break;
            }
        }
    }
}