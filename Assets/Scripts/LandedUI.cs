using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TitleTextMesh;
    [SerializeField] TextMeshProUGUI StatsTextMesh;
    [SerializeField] TextMeshProUGUI LabelTextMesh;
    [SerializeField] TextMeshProUGUI NextButtonTextMesh;
    [SerializeField] private Button nextButton;

    private Action nextButtonClickAction;
    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            nextButtonClickAction();    
        });
    }

    private void Start()
    {
        Lander.Instance.onLanding += Lander_onLanding;
        Debug.Log("Hide");
        hide();
       
    }

    private void Lander_onLanding(object sender, Lander.onLandingEventArgs e)
    {
        bool newHighScore = false;
        bool newBestTime = false;

        if (e.landingtype == Lander.LandingType.Success)
        {
            TitleTextMesh.text = "SUCCESFULL LANDING";
            NextButtonTextMesh.text = "CONTINUE";
            nextButtonClickAction = GameManager.Instance.GoToNextLevel;

            newHighScore = (GameManager.Instance.GetScore()+e.score)> GameManager.Instance.GetHighScore();
            float bestTime = GameManager.Instance.GetBestTime();
            newBestTime = bestTime == 0 ||GameManager.Instance.GetTime() < bestTime;
        }
        else if(e.landingtype==Lander.LandingType.BulletHit)
        {
            TitleTextMesh.text = "<color=#ff0000>DIED</color>";
            NextButtonTextMesh.text = "RETRY";
            nextButtonClickAction = GameManager.Instance.RetryLevel;
        }
        else
        {
            TitleTextMesh.text = "<color=#ff0000>CRASH!</color>";
            NextButtonTextMesh.text = "RETRY";
            nextButtonClickAction=GameManager.Instance.RetryLevel;
        }
        if (newHighScore && newBestTime)
        {
            LabelTextMesh.text =
                "Landing Speed\n" +
                "Landing Angle\n" +
                "Multiplier\n" +
                "Score\n" +
                "<color=yellow>New High Score!</color>\n" +
                "<color=yellow>New Best Time!</color>";
        }
        else if (newHighScore)
        {
            LabelTextMesh.text =
                "Landing Speed\n" +
                "Landing Angle\n" +
                "Multiplier\n" +
                "Score\n" +
                "<color=yellow>New High Score!</color>\n" +
                "Best Time";
        }
        else if (newBestTime)
        {
            LabelTextMesh.text =
                "Landing Speed\n" +
                "Landing Angle\n" +
                "Multiplier\n" +
                "Score\n" +
                "High Score\n" +
                "<color=yellow>New Best Time!</color>";
        }
        else
        {
            LabelTextMesh.text =
                "Landing Speed\n" +
                "Landing Angle\n" +
                "Multiplier\n" +
                "Score\n" +
                "High Score\n" +
                "Best Time";
        }
        if (e.landingtype == Lander.LandingType.Success) {
            StatsTextMesh.text =
            Mathf.Round(e.landingSpeed * 2f) + "\n" +
            Mathf.Round(e.dotVector * 100f) + "\n" +
            "x" + e.scoreMultiplier + "\n" +
            (GameManager.Instance.GetScore() + e.score) + "\n" +
            Mathf.Max(GameManager.Instance.GetScore() + e.score, GameManager.Instance.GetHighScore()) + "\n" +
            Mathf.Min(GameManager.Instance.GetTime(), GameManager.Instance.GetBestTime()).ToString("F2");
        }
        else
        {
            StatsTextMesh.text =
            0 + "\n" +
            0+ "\n" +
            "x" + 0 + "\n" +
            0+ "\n" +
            GameManager.Instance.GetHighScore()+ "\n" +
            GameManager.Instance.GetBestTime().ToString("F2");
        }
            show();
    }
    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide()
    {
        gameObject.SetActive(false);
    }
}

