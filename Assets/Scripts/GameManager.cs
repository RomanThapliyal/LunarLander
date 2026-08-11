using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int levelNumber = 1;
    public static int totalScore=0;


    public static void ResetStaticData()
    {
        levelNumber = 1;
        totalScore = 0;
    }

    public event EventHandler onGamePaused;
    public event EventHandler onGameUnPaused;

    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private int score;
    private float time;
    private bool isTimerActive;
     
    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        Lander.Instance.onCoinPickUp += Lander_onCoinPickUp;
        Lander.Instance.onLanding += Lander_onLanding;
        Lander.Instance.onStateChanged += Lander_onStateChanged;
        GameInput.instance.onPauseButtonPressed += GameInput_onPauseButtonPressed;
        LoadCurrentLevel();
    }

    private void GameInput_onPauseButtonPressed(object sender, System.EventArgs e)
    {
        PauseUnpauseGame();
    }

    private void Lander_onStateChanged(object sender, Lander.onStateChangedEventArgs e)
    {
        isTimerActive = e.state == Lander.State.normal;
        if (e.state == Lander.State.normal)
        {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CineMachineZoom2D.Instance.SetNormalOrthoGraphicSize();
        }
    }

    private void Update()
    {
        if (isTimerActive)
        {
            time += Time.deltaTime;
        }
    }

    private void LoadCurrentLevel()
    {
        GameLevel gamelevel=GetGameLevel();
        GameLevel spawnedGameLevel = Instantiate(gamelevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = spawnedGameLevel.GetLandingStartPosition();
        cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.GetCameraStartTargetTransform();
        CineMachineZoom2D.Instance.SetTargetOrthoGraphicSize(spawnedGameLevel.GetZoomedOutOrthographicSize());
    }
        
     private GameLevel GetGameLevel()
    {
        foreach (GameLevel gamelevel in gameLevelList)
        {
            if (gamelevel.GetLevelNumber() == levelNumber)
            {
                return gamelevel;
            }
        }
        return null;
      }

    private void Lander_onLanding(object sender, Lander.onLandingEventArgs e)
    {
        AddScore(e.score);
        if (e.landingtype == Lander.LandingType.Success)
        {
            SaveLevelRecords();
        }
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
    public void GoToNextLevel()
    {
        levelNumber++;
        totalScore += score;
        if (GetGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        }
    }

    public void RetryLevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }
    private void Lander_onCoinPickUp(object sender, System.EventArgs e)
    {
        AddScore(100);
    }

    public void AddScore(int addScoreAmount)
    {
        score += addScoreAmount;
    }
    public int GetScore()
    {
        return score;
    }
    public float GetTime()
    {
        return time;
    }

    public int GetLevel()
    {
        return levelNumber;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        onGamePaused?.Invoke(this,EventArgs.Empty);
    }

    public void PauseUnpauseGame()
    {
        if (Time.timeScale != 0) { PauseGame(); }
        else { UnPauseGame(); }
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        onGameUnPaused?.Invoke(this,EventArgs.Empty);
    }
    public void SaveLevelRecords()
    {
        string scoreKey = "Level" + levelNumber + "_HighScore";
        int previousHighScore = PlayerPrefs.GetInt(scoreKey, 0);

        if (score > previousHighScore)
        {
            PlayerPrefs.SetInt(scoreKey, score);
        }

        string timeKey = "Level" + levelNumber + "_BestTime";
        float previousBestTime = PlayerPrefs.GetFloat(timeKey, float.MaxValue);

        if (time < previousBestTime)
        {
            PlayerPrefs.SetFloat(timeKey, time);
        }

        PlayerPrefs.Save();
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("Level" + levelNumber + "_HighScore",0);
    }

    public float GetBestTime()
    {
        return PlayerPrefs.GetFloat("Level" + levelNumber + "_BestTime",100000);
    }
}

