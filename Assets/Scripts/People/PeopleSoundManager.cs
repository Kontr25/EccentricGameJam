using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class PeopleSoundManager : MonoBehaviour
    {
        public static PeopleSoundManager Instance;
        
        [SerializeField] private List<AudioClip> _manSounds;
        [SerializeField] private List<AudioClip> _womanSounds;

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
            int clipNumber = Random.Range(0, _manSounds.Count);
            AudioClip clip = _manSounds[clipNumber];
            _manSounds.Remove(_manSounds[clipNumber]);
            return clip;
        }
        
        public AudioClip RandomWomanSound()
        {
            int clipNumber = Random.Range(0, _womanSounds.Count);
            AudioClip clip = _womanSounds[clipNumber];
            _womanSounds.Remove(_womanSounds[clipNumber]);
            return clip;
        }
        
        public float SoundsTime(AudioSource sound)
        {
            return sound.time;
        }
    }
}