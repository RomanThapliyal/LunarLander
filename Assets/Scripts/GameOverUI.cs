using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI scoreTextMesh;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scene.MainMenueScene); });
    }
    private void Start()
    {
        scoreTextMesh.text="Final Score: "+GameManager.Instance.GetTotalScore().ToString();
    }

}
