using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanAnimator : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _enemyAnimator;
    
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Angry1 = Animator.StringToHash("Angry1");
        private static readonly int Angry2 = Animator.StringToHash("Angry2");
        private static readonly int StopTalking = Animator.StringToHash("StopTalking");

        private void FixedUpdate()
        {
            _enemyAnimator.SetFloat(Speed, _agent.velocity.magnitude);
        }

        public void AngryTalking()
        {
            Debug.Log("ANGRY");
            int randomAnimation = Random.Range(0, 2);
            switch (randomAnimation)
            {
                case 0:
                    _enemyAnimator.SetTrigger(Angry1);
                    break;
                case 1:
                    _enemyAnimator.SetTrigger(Angry2);
                    break;
            }
        }

        public void StopAngryTalking()
        {
            _enemyAnimator.SetTrigger(StopTalking);
        }
    }
}