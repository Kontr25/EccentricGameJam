using Cinemachine;
using UnityEngine;

namespace Character
{
    public class CharacterStartCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera[] _poseCamaras;
        
        public void SetPoseCamera(CharacterName name)
        {
            DisableAllPoseCameras();
            
            switch (name)
            {
                case CharacterName.Eldar:
                    _poseCamaras[0].Priority = 100;
                    break;
                case CharacterName.Maloy:
                    _poseCamaras[1].Priority = 100;
                    break;
                case CharacterName.Leha:
                    _poseCamaras[2].Priority = 100;
                    break;
                case CharacterName.Kolya:
                    _poseCamaras[3].Priority = 100;
                    break;
                case CharacterName.Kirill:
                    _poseCamaras[4].Priority = 100;
                    break;
                case CharacterName.Idrak:
                    _poseCamaras[5].Priority = 100;
                    break;
            }
        }

        public void DisableAllPoseCameras()
        {
            for (int i = 0; i < _poseCamaras.Length; i++)
            {
                _poseCamaras[i].Priority = 0;
            }
        }
    }
}