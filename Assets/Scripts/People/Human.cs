using System;
using UnityEngine;

namespace People
{
    public class Human : MonoBehaviour
    {
        [SerializeField] private HumanMover _humanMover;
        [SerializeField] private GameObject _firstMan;
        [SerializeField] private GameObject _secondMan;
        [SerializeField] private GameObject _firstWoman;
        [SerializeField] private GameObject _secondWoman;
        [SerializeField] private HumanType _humanType;
        [SerializeField] private HumanAgroZone _humanAgroZone;
        [SerializeField] private HumanAngryTalkingZone _humanAngryTalking;
        [SerializeField] private HumanShowPlaceTrigger _humanShowPlaceTrigger;
        
        private PeopleSpawner _peopleSpawner;

        public void SetHumanType(HumanType type)
        {
            _humanType = type;
            switch (type)
            {
                case HumanType.FirstManType:
                    _firstMan.SetActive(true);
                    break;
                case HumanType.SecondManType:
                    _secondMan.SetActive(true);
                    break;
                case HumanType.FirstWomanType:
                    _firstWoman.SetActive(true);
                    break;
                case HumanType.SecondWomanType:
                    _secondWoman.SetActive(true);
                    break;
            }
        }

        public PeopleSpawner Spawner
        {
            get => _peopleSpawner;
            set => _peopleSpawner = value;
        }

        public HumanMover Mover
        {
            get => _humanMover;
            set => _humanMover = value;
        }

        public HumanType HumanType
        {
            get => _humanType;
            set => _humanType = value;
        }

        public void GoOnShow(Transform showPlace)
        {
            _humanMover.Speed = 3.5f;
            _humanAgroZone.gameObject.SetActive(false);
            _humanAngryTalking.gameObject.SetActive(false);
            _humanMover.Patrol(false);
            _humanShowPlaceTrigger.SetTarget(showPlace);
            _humanMover.MoveTo(showPlace.position);
        }
    }
}