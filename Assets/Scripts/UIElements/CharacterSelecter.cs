using System;
using Character;
using Cinemachine;
using TMPro;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace UIElements
{
    public class CharacterSelecter : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _characterAnimator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CharacterStartCamera _characterStartCamera;
        [SerializeField] private CharacterMover _characterMover;
        [SerializeField] private GameObject[] _objectForDisable;
        [SerializeField] private SettingsMenu _settingsMenu;
        [SerializeField] private CinemachineVirtualCamera _characterCamera;
        [SerializeField] private TMP_Text _nameNext;
        [SerializeField] private String[] _names;

        private int _currentSkinNumber = 0;

        private void Start()
        {
            ChangeCharacter();
        }

        public void NextSkin()
        {
            if (_currentSkinNumber < 5)
            {
                _currentSkinNumber++;
            }
            else
            {
                _currentSkinNumber = 0;
            }
            
            ChangeCharacter();
        }
        
        public void PrewiousSkin()
        {
            if (_currentSkinNumber > 0)
            {
                _currentSkinNumber--;
            }
            else
            {
                _currentSkinNumber = 5;
            }
            
            ChangeCharacter();
        }

        private void ChangeCharacter()
        {
            switch (_currentSkinNumber)
            {
                case 0:
                    SetCharacter(CharacterName.Eldar);
                    break;
                case 1:
                    SetCharacter(CharacterName.Maloy);
                    break;
                case 2:
                    SetCharacter(CharacterName.Leha);
                    break;
                case 3:
                    SetCharacter(CharacterName.Kolya);
                    break;
                case 4:
                    SetCharacter(CharacterName.Kirill);
                    break;
                case 5:
                    SetCharacter(CharacterName.Idrak);
                    break;
            }
        }

        private void SetCharacter(CharacterName name)
        {
            _characterAnimator.SetPose(name);
            _characterController.SetCharacterSkin(name);
            _characterStartCamera.SetPoseCamera(name);
            SetName(name);
        }

        private void SetName(CharacterName name)
        {
            switch (name)
            {
                case CharacterName.Eldar:
                    _nameNext.text = _names[0];
                    break;
                case CharacterName.Maloy:
                    _nameNext.text = _names[1];
                    break;
                case CharacterName.Leha:
                    _nameNext.text = _names[2];
                    break;
                case CharacterName.Kolya:
                    _nameNext.text = _names[3];
                    break;
                case CharacterName.Kirill:
                    _nameNext.text = _names[4];
                    break;
                case CharacterName.Idrak:
                    _nameNext.text = _names[5];
                    break;
            }
        }

        public void Play()
        {
            _characterCamera.Priority = 10;
            _characterStartCamera.DisableAllPoseCameras();
            _characterAnimator.StartPlay();
            for (int i = 0; i < _objectForDisable.Length; i++)
            {
                _objectForDisable[i].SetActive(false);
            }
            _characterMover.enabled = true;
            _settingsMenu.SwitchSettingsMenu();
        }
    }
}