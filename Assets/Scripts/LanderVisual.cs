using UnityEngine;

public class LanderVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private ParticleSystem landingCelebrationParticleSystem;
    [SerializeField] private GameObject landerExplosinVfx;

    Lander lander;
    private void Awake()
    {
        landingCelebrationParticleSystem.Stop();
        lander = GetComponent<Lander>();
        lander.onUpforce += Lander_onUpforce;
        lander.onLeftforce += Lander_onLeftforce;
        lander.onRightforce += Lander_onRightforce;
        lander.onbeforeUpforce += Lander_beforeUpforce;

        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    }


    private void Start()
    {
        lander.onLanding += Lander_onLanding;
    }

    private void Lander_onLanding(object sender, Lander.onLandingEventArgs e)
    {
        switch (e.landingtype)
        {
            case Lander.LandingType.TooSteepAngle:
            case Lander.LandingType.TooFastLanding:
            case Lander.LandingType.WrongLandingArea:
            case Lander.LandingType.BulletHit:
                Instantiate(landerExplosinVfx,transform.position,Quaternion.identity);
                gameObject.SetActive(false);
                break;
            case Lander.LandingType.Success:
                landingCelebrationParticleSystem.Play();
                break;

        }
    }

    private void Lander_beforeUpforce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    }

    private void Lander_onRightforce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
    }

    private void Lander_onLeftforce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);
    }

    private void Lander_onUpforce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);
    }

    private void SetEnabledThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }

}
