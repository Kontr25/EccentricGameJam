using UnityEngine;

namespace Environment
{
    public class MapBorder : MonoBehaviour
    {
        [SerializeField] private Transform _character;
        [SerializeField] private Transform _stage;
        private float _upperBound = 22f;
        private float _lowerBound = -22f;
        private float _rightBorder = 22f;
        private float _leftBorder = -22;

        public float UpperBound => _upperBound;

        public float LowerBound => _lowerBound;

        public float RightBorder => _rightBorder;

        public float LeftBorder => _leftBorder;

        public Vector3 RandomPosition()
        {
            Vector3 position = new Vector3();

            position = new Vector3(Random.Range(LeftBorder, RightBorder),0, Random.Range(LowerBound, UpperBound));
            
            while (Vector3.Distance(_character.position, position) < 5 || Vector3.Distance(_stage.position, position) < 5)
            {
                position = new Vector3(Random.Range(LeftBorder, RightBorder),0, Random.Range(LowerBound, UpperBound));
            }

            return position;
        }
    }
}