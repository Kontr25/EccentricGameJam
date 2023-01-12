using System;
using System.Collections.Generic;
using Environment;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace People
{
    public class PeopleSpawner : MonoBehaviour
    {
        public static PeopleSpawner Instance;
        [SerializeField] private int _peopleCount;
        [SerializeField] private MapBorder _mapBorder;
        [SerializeField] private Human _human;
        [SerializeField] private List<Human> _humans;
        [SerializeField] private Transform _showPlacesParent;
        [SerializeField] private List<Transform> _showPlaces = new List<Transform>();

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
            SpawnPeople();
            foreach (Transform place in _showPlacesParent)
            {
                _showPlaces.Add(place);
            }
        }

        private void SpawnPeople()
        {
            for (int i = 0; i < _peopleCount; i++)
            {
                Human human = Instantiate(_human, _mapBorder.RandomPosition(), quaternion.identity);
                human.Mover.BorderMap = _mapBorder;
                _humans.Add(human);
                int randomHumanType = Random.Range(1, 5);
                switch (randomHumanType)
                {
                    case 1:
                        human.SetHumanType(HumanType.FirstManType);
                        break;
                    case 2:
                        human.SetHumanType(HumanType.SecondManType);
                        break;
                    case 3:
                        human.SetHumanType(HumanType.FirstWomanType);
                        break;
                    case 4:
                        human.SetHumanType(HumanType.SecondWomanType);
                        break;
                }
            }
        }

        public void GoOnShow()
        {
            for (int i = 0; i < _humans.Count; i++)
            {
                int randomShowPlace = Random.Range(0, _showPlaces.Count);
                _humans[i].GoOnShow(_showPlaces[randomShowPlace]);
                _showPlaces.Remove(_showPlaces[randomShowPlace]);
            }
        }
    }

    public enum HumanType
    {
        FirstManType,
        SecondManType,
        FirstWomanType,
        SecondWomanType
    }
}