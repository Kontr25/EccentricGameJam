using Character;
using UnityEngine;

namespace People
{
    public class HumanAgroZone : MonoBehaviour
    {
        [SerializeField] private HumanMover humanMover;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterMover character))
            {
                humanMover.Player = character;
                humanMover.Speed = 2f;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterMover character))
            {
                humanMover.Speed = 1f;
                humanMover.Player = null;
            }
        }
    }
}