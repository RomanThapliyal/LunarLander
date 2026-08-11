using UnityEngine;
using UnityEngine.UI;

public class MainMenue : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            GameManager.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene); });
        quitButton.onClick.AddListener(() => { Application.Quit(); });
    }
}
