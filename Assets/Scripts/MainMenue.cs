using UnityEngine;
using UnityEngine.UI;

public class MainMenue : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button guideButton;
    [SerializeField] Button playButton2;
    [SerializeField] Button quitButton;
    [SerializeField] Button backButton;

    [SerializeField] GameObject guideScreen;
    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            GameManager.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene); });
        playButton2.onClick.AddListener(() => {
            GameManager.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });
        backButton.onClick.AddListener(() => { guideScreen.SetActive(false); });
        guideButton.onClick.AddListener(() => { guideScreen.SetActive(true); });
        quitButton.onClick.AddListener(() => { Application.Quit(); });
    }
}
