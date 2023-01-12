using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class CharacterRagdoll : MonoBehaviour
    {
        [SerializeField] private Rigidbody _assRigidbody;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private Transform _explosionPoint;

        private Vector3 _explosionPosition;
        private void Start()
        {
            transform.SetParent(null);
            Explosion();
        }

        private void Explosion()
        {
            int deviation = 0;
            switch (Random.Range(0,2))
            {
                case 0:
                    deviation = -4;
                    break;
                case 1:
                    deviation = 4;
                    break;
            }
            _explosionPosition = new Vector3(_explosionPoint.transform.position.x + deviation,
                _explosionPoint.transform.position.y - 2f, _explosionPoint.transform.position.z);
            _assRigidbody.AddExplosionForce(_explosionForce, _explosionPosition, _explosionRadius);
        }
    }
}