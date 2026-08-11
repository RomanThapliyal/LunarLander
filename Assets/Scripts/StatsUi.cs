using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statsTextMesh;
    [SerializeField] TextMeshProUGUI RecordTextMesh;
    [SerializeField] GameObject SpeedRightArrowObj;
    [SerializeField] GameObject SpeedLeftArrowObj;
    [SerializeField] GameObject SpeedUpArrowObj;
    [SerializeField] GameObject SpeedDownArrowObj;
    [SerializeField] Image FuelBar;

    [SerializeField] GameObject container;

    [SerializeField] Animator animator;
    private void Awake()
    {
        hide();
    }
    private void Update()
    {
        if((GameInput.instance.isUpLanderPressed() || GameInput.instance.isLeftLanderPressed() || GameInput.instance.isRightLanderPressed()))
            {
            show();
        }
        UpdateStatsTextMesh();
    }

    private void UpdateStatsTextMesh()
    {
        SpeedUpArrowObj.SetActive(Lander.Instance.GetSpeedY()>0);
        SpeedDownArrowObj.SetActive(Lander.Instance.GetSpeedY() < 0);
        SpeedRightArrowObj.SetActive(Lander.Instance.GetSpeedX() > 0);
        SpeedLeftArrowObj.SetActive(Lander.Instance.GetSpeedX() < 0);

        FuelBar.fillAmount = Lander.Instance.GetFuelAmountNormalized();
        UpdateFuelBarColor();

        statsTextMesh.text = GameManager.Instance.GetLevel()+"\n"+
            GameManager.Instance.GetScore() + "\n" +
            Mathf.Round(GameManager.Instance.GetTime()) + "\n" +
            Mathf.Abs(Mathf.Round((Lander.Instance.GetSpeedX()*10f))) + "\n" +
            Mathf.Abs(Mathf.Round((Lander.Instance.GetSpeedY()*10f)));
            ;
    }

    private void UpdateRecordTextMesh()
    {
        if(GameManager.Instance.GetBestTime() != 100000) {
            RecordTextMesh.text = GameManager.Instance.GetHighScore() + "\n" +
            GameManager.Instance.GetBestTime().ToString("F2"); 
        }
        else
        {
            RecordTextMesh.text = "NULL" + "\n" + "NULL";
        }
    }
    private void hide()
    {
        if (container.activeSelf)
            container.SetActive(false);
    }

    private void show()
    {
        if (!container.activeSelf)
        {
            container.SetActive(true);
            UpdateRecordTextMesh();
            PlayStatsUIAnimation();
        }
    }

    public void PlayStatsUIAnimation()
    {
        animator.SetTrigger("Show");
        Debug.Log("WOOO");
    }

    private void UpdateFuelBarColor()
    {
        float fuel = Lander.Instance.GetFuelAmountNormalized();

        Color red = Color.red;
        Color orange = new Color(1f, 0.5f, 0f);
        Color yellow = Color.yellow;
        Color green = Color.green;

        if (fuel < 0.33f)
        {
            FuelBar.color = Color.Lerp(red, orange, fuel / 0.33f);
        }
        else if (fuel < 0.66f)
        {
            FuelBar.color = Color.Lerp(orange, yellow, (fuel - 0.33f) / 0.33f);
        }
        else
        {
            FuelBar.color = Color.Lerp(yellow, green, (fuel - 0.66f) / 0.34f);
        }
    }

}
