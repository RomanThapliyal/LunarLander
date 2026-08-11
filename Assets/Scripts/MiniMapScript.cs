using UnityEngine;

public class MinimapContainer : MonoBehaviour
{
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject brokenGlass;

    private bool hasMoved=false;    

    private void Start()
    {
        minimap.SetActive(false);
        brokenGlass.SetActive(false);
        Lander.Instance.onLanding += Lander_onLanding;
    }

    private void Lander_onLanding(object sender, Lander.onLandingEventArgs e)
    {
        if (e.landingtype != Lander.LandingType.Success)
        {
            brokenGlass.SetActive(true);
        }
    }

    private void Update()
    {
        if(!hasMoved &&
             (GameInput.instance.isUpLanderPressed() ||
              GameInput.instance.isLeftLanderPressed() ||
              GameInput.instance.isRightLanderPressed()))
        {
            hasMoved = true;
            minimap.SetActive(true);
        }
        if (GameInput.instance.isMiniMapPressed())
        {
            minimap.SetActive(!minimap.activeSelf);
        }
    }
}