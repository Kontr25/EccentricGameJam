using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class PeopleSoundManager : MonoBehaviour
    {
        public static PeopleSoundManager Instance;
        
        [SerializeField] private AudioClip[] _manSounds;
        [SerializeField] private AudioClip[] _womanSounds;

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

        public AudioClip RandomManSound()
        {
            return _manSounds[Random.Range(0, _manSounds.Length)];
        }
        
        public AudioClip RandomWomanSound()
        {
            return _womanSounds[Random.Range(0, _womanSounds.Length)];
        }
        
        public float SoundsTime(AudioSource sound)
        {
            return sound.time;
        }
    }
}