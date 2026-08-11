using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerVisual : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource crashCinemachineImpulseSource;
    [SerializeField] private CinemachineImpulseSource PickUpCinemachineImpulseSource;
    [SerializeField] private ParticleSystem pickUpParticleSystem;
    [SerializeField] private ScorePopUp scorePopupVFX;

    private void Start()
    {
        Lander.Instance.onLanding += Lander_onLanding;
        Lander.Instance.onCoinPickUp += Lander_onCoinPickUp;
        Lander.Instance.onFuelPickUp += Lander_onFuelPickUp;
        CannonScript.Instance.onShoot += Cannon_onShoot;
    }

    private void Cannon_onShoot(object sender, System.EventArgs e)
    {
        PickUpCinemachineImpulseSource.GenerateImpulse(1f);
    }

    private void Lander_onFuelPickUp(object sender, System.EventArgs e)
    {
        PickUpCinemachineImpulseSource.GenerateImpulse(0.1f);
        Instantiate(pickUpParticleSystem, Lander.Instance.lastPickupPosition, Quaternion.identity);
        Instantiate(scorePopupVFX, Lander.Instance.lastPickupPosition, Quaternion.identity).SetTextTo("+Fuel ");
    }

    private void Lander_onCoinPickUp(object sender, System.EventArgs e)
    {
        PickUpCinemachineImpulseSource.GenerateImpulse(0.1f);
        Instantiate(pickUpParticleSystem, Lander.Instance.lastPickupPosition, Quaternion.identity);
        Instantiate(scorePopupVFX, Lander.Instance.lastPickupPosition, Quaternion.identity).SetTextTo("+100");
    }

    private void Lander_onLanding(object sender, Lander.onLandingEventArgs e)
    {
        switch (e.landingtype)
        {
            case Lander.LandingType.TooSteepAngle:
            case Lander.LandingType.TooFastLanding:
            case Lander.LandingType.WrongLandingArea:
            case Lander.LandingType.BulletHit:
                crashCinemachineImpulseSource.GenerateImpulse(2.3f);
                break;
        }
    }
}
