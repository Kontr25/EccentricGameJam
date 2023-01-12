using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Transform _settingsWindow;
    [SerializeField] private Transform _hintsWindow;
    [SerializeField] private GameObject _closePanel;
    [SerializeField] private AudioSource _bgMusic;
    [SerializeField] private float _openTime = 1f;
    [SerializeField] private Button _BGMusicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Sprite _enabledSoundSprite;
    [SerializeField] private Sprite _disabledSoundSprite;
    [SerializeField] private Sprite _disabledBGMusicSprite;
    [SerializeField] private Sprite _enabledBGMusicSprite;

    private bool _isOpen = true;
    private bool _volumeEnable = true;
    private bool _bgMusicEnable = true;
    private Vector3 _settingsWindowDefaultPosition;
    private Vector3 _hintsWindowDefaultPosition;

    private void Start()
    {
        _settingsWindowDefaultPosition = _settingsWindow.position;
        _hintsWindowDefaultPosition = _hintsWindow.position;
    }

    public void SwitchSettingsMenu()
    {
        if (_isOpen == false)
        {
            _settingsWindow.DOMoveX(_settingsWindowDefaultPosition.x, _openTime);
            _hintsWindow.DOMoveX(_hintsWindowDefaultPosition.x, _openTime);
            _closePanel.SetActive(true);
        }
        else
        {
            _settingsWindow.DOMoveX(_settingsWindowDefaultPosition.x + 1000, _openTime);
            _hintsWindow.DOMoveX(_hintsWindowDefaultPosition.x - 1000, _openTime);
            _closePanel.SetActive(false);
        }

        _isOpen = !_isOpen;
    }


    public void SwitchAllSound()
    {
        _volumeEnable = !_volumeEnable;

        if (_volumeEnable == false)
        {
            AudioListener.volume = 0f;
            _soundButton.image.sprite = _disabledSoundSprite;
        }
        else
        {
            AudioListener.volume = 1f;
            _soundButton.image.sprite = _enabledSoundSprite;
        }
    }
    
    public void SwitchBGMusic()
    {
        _bgMusicEnable = !_bgMusicEnable;

        if (_bgMusicEnable == false)
        {
            _bgMusic.Stop();
            _BGMusicButton.image.sprite = _disabledBGMusicSprite;
        }
        else
        {
            _bgMusic.Play();
            _BGMusicButton.image.sprite = _enabledBGMusicSprite;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
