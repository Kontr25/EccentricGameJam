using System;
using Cinemachine;
using UnityEngine;

namespace CameraChanger
{
    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera[] _allCameras;
        // CharacterCamera = 0, PoopCamera = 1, PeopleCamera = 2, RagdollCamera = 3
        
        public static CameraChanger Instance;

        public CinemachineVirtualCamera[] AllCameras
        {
            get => _allCameras;
            set => _allCameras = value;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                transform.parent = null;
                Instance = this;
                
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _allCameras[0].Priority = 50;
        }

        public void ActivateCamera(NameCamera nameCamera)
        {
            DisableAllCameras();
            switch (nameCamera)
            {
                case NameCamera.CharacterCamera:
                    _allCameras[0].Priority = 10;
                    break;
                case NameCamera.PoopCamera:
                    _allCameras[1].Priority = 10;
                    break;
                case NameCamera.PeopleCamera:
                    _allCameras[2].Priority = 10;
                    break;
                case NameCamera.RagdollCamera:
                    _allCameras[3].Priority = 10;
                    break;
            }
        }

        private void DisableAllCameras()
        {
            for (int i = 0; i < _allCameras.Length; i++)
            {
                _allCameras[i].Priority = 0;
            }
        }
    }

    public enum NameCamera
    {
        CharacterCamera,
        PoopCamera,
        PeopleCamera,
        RagdollCamera
    }
}