using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button soundVolumeButton;
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private Button guideButton;
    [SerializeField] private Button backButton;

    [SerializeField] private TextMeshProUGUI soundVolumeTextMesh;
    [SerializeField] private TextMeshProUGUI musicVolumeTextMesh;

    [SerializeField] private GameObject guideScreen;

    private void Awake()
    {
        Time.timeScale = 1f;

        soundVolumeButton.onClick.AddListener(() => { 
            SoundManager.Instance.ChangeSoundVolume();
            soundVolumeTextMesh.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        });
        musicVolumeButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeMusicVolume();
            musicVolumeTextMesh.text="MUSIC "+MusicManager.Instance.GetMusicVolume();
        });
        resumeButton.onClick.AddListener(() => { GameManager.Instance.UnPauseGame(); });
        menuButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scene.MainMenueScene); });
        restartButton.onClick.AddListener(() => { GameManager.Instance.RetryLevel(); });

        backButton.onClick.AddListener(() => { guideScreen.SetActive(false); });
        guideButton.onClick.AddListener(() => { guideScreen.SetActive(true); });
    }
    private void Start()
    {
        Time.timeScale = 1f;
        Hide();
        GameManager.Instance.onGamePaused += GameManager_onGamePaused;
        GameManager.Instance.onGameUnPaused += GameManager_onGameUnPaused;

        soundVolumeTextMesh.text = "SOUND "+SoundManager.Instance.GetSoundVolume();
        musicVolumeTextMesh.text = "MUSIC " + MusicManager.Instance.GetMusicVolume();
  

    }

    private void GameManager_onGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_onGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
