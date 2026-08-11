using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const int SOUND_VOLUME_MAX = 10;
    private static int soundVolume=6;
    [SerializeField] private AudioClip fuelPickUpAudioClip;
    [SerializeField] private AudioClip coinPickUpAudioClip;
    [SerializeField] private AudioClip crashAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;
    [SerializeField] private AudioClip keyPickUpAudioClip;
    [SerializeField] private AudioClip gateOpenAudioClip;
    [SerializeField] private AudioClip CannonFireAudioClip;

    public static SoundManager Instance { get; private set; }
    public event EventHandler onSoundVolumeChanged;

    private void Awake()
    {
        Instance = this;    
    }
    private void Start()
    {
        Lander.Instance.onCoinPickUp += Lander_onCoinPickUp;
        Lander.Instance.onFuelPickUp += Lander_onFuelPickUp;
        Lander.Instance.onLanding += Lander_onLanding;
 
    }



    private void Lander_onLanding(object sender, Lander.onLandingEventArgs e)
    {
        if (e.landingtype == Lander.LandingType.Success)
        {
            AudioSource.PlayClipAtPoint(landingSuccessAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
        }
        else
        {
            AudioSource.PlayClipAtPoint(crashAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
        }
    }

    private void Lander_onFuelPickUp(object sender, System.EventArgs e)
    {
        Debug.Log("Cannon sound event fired");
        AudioSource.PlayClipAtPoint(fuelPickUpAudioClip,Camera.main.transform.position,GetSoundVolumeNormalized());
    }

    private void Lander_onCoinPickUp(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickUpAudioClip,Camera.main.transform.position,GetSoundVolumeNormalized());
    }

    public void PlayKeyPickUpSound()
    {
        AudioSource.PlayClipAtPoint(keyPickUpAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    public void PlayGateOpenSound()
    {
        AudioSource.PlayClipAtPoint(gateOpenAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    public void PlayCannonFireSound()
    {
        AudioSource.PlayClipAtPoint(CannonFireAudioClip,Camera.main.transform.position,GetSoundVolumeNormalized());
    }
    public void ChangeSoundVolume()
    {
        soundVolume = (soundVolume + 1) % (SOUND_VOLUME_MAX+1);
        onSoundVolumeChanged?.Invoke(this,EventArgs.Empty);
    }

    public int GetSoundVolume()
    {
        return soundVolume;
    }

    public float GetSoundVolumeNormalized()
    {
        return ((float)soundVolume) / (SOUND_VOLUME_MAX);
    }
}
