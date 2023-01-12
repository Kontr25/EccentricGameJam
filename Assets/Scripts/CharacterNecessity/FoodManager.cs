using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CharacterNecessity
{
    public class FoodManager : MonoBehaviour
    {
        public static FoodManager Instance;

        [SerializeField] private List<Food> _foods;
        [SerializeField] private List<Garbage> _garbages;

        public List<Food> Foods
        {
            get => _foods;
            set => _foods = value;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                transform.SetParent(null);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            for (int i = 0; i < _foods.Count; i++)
            {
                int garbageNumber = Random.Range(0, _garbages.Count);
                _foods[i].transform.position = new Vector3(_garbages[garbageNumber].transform.position.x,
                        1f, _garbages[garbageNumber].transform.position.z);
                _garbages[garbageNumber].CurrentFood = _foods[i];
                _garbages.Remove(_garbages[garbageNumber]);
            }
        }
    }
}