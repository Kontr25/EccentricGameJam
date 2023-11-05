using UnityEngine;

namespace CharacterNecessity
{
    public class Garbage : MonoBehaviour
    {
        [SerializeField] private Outline[] _outlines;
        [SerializeField] private Animator _animator;
        
        private Coroutine _loadRoutine;
        private Coroutine _unloadRoutine;
        private WaitForSeconds _delay;
        private Food _currentFood;
        private static readonly int Open = Animator.StringToHash("Open");


        public Food CurrentFood
        {
            get => _currentFood;
            set
            {
                if (value == null)
                {
                    OutlineEnabled(false);

                }
                else
                {
                    OutlineEnabled(true);
                }
                _currentFood = value;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character.CharacterController character) && _currentFood != null)
            {
                _currentFood.Eat(character.HeadPoint);
                _animator.SetTrigger(Open);
                CurrentFood = null;
            }
        }

        private void OutlineEnabled(bool value)
        {
            for (int i = 0; i < _outlines.Length; i++)
            {
                _outlines[i].enabled = value;
            }
        }
    }
}