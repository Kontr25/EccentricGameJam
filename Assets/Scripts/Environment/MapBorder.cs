using UnityEngine;

namespace Environment
{
    public class MapBorder : MonoBehaviour
    {
        [SerializeField] private Transform _character;
        private float _upperBound = 20f;
        private float _lowerBound = -20f;
        private float _rightBorder = 20f;
        private float _leftBorder = -20;

        public float UpperBound => _upperBound;

        public float LowerBound => _lowerBound;

        public float RightBorder => _rightBorder;

        public float LeftBorder => _leftBorder;

        public Vector3 RandomPosition()
        {
            Vector3 position = new Vector3();

            position = new Vector3(Random.Range(LeftBorder, RightBorder),0, Random.Range(LowerBound, UpperBound));
            
            while (Vector3.Distance(_character.position, position) < 10)
            {
                position = new Vector3(Random.Range(LeftBorder, RightBorder),0, Random.Range(LowerBound, UpperBound));
            }

            return position;
        }
    }
}