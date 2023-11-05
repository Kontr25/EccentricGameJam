using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CharacterNecessity
{
    public class PositionSetter : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;
        [SerializeField] private List<Transform> _transforms;

        private void Awake()
        {
            SetPosition();
        }

        private void SetPosition()
        {
            for (int i = 0; i < _transforms.Count; i++)
            {
                int pointNumber = Random.Range(0, _points.Count);
                Transform point = _points[pointNumber];
                _transforms[i].position = point.position;
                _transforms[i].rotation = point.rotation;
                _points.Remove(point);
            }
        }
    }
}