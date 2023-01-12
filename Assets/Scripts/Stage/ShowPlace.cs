using UnityEngine;

namespace Stage
{
    public class ShowPlace : MonoBehaviour
    {
        [SerializeField] private Transform _stage;

        public Transform StandUpStage
        {
            get => _stage;
            set => _stage = value;
        }
    }
}