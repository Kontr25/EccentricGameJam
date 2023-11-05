using DG.Tweening;
using Stage;
using UnityEngine;

namespace People
{
    public class HumanShowPlaceTrigger : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        private Transform _target;

        public void SetTarget(Transform showPlace)
        {
            _target = showPlace;
            _collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ShowPlace showPlace) && showPlace.transform == _target)
            {
                Vector3 rotationTargetPosition = new Vector3(showPlace.StandUpStage.position.x,
                    transform.parent.position.y,
                    showPlace.StandUpStage.position.z);
                transform.parent.DOLookAt(rotationTargetPosition, .5f);
            }
        }
    }
}