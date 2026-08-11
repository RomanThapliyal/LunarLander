using UnityEngine;

public class LanderAudio : MonoBehaviour
{
    [SerializeField] private AudioSource thrusterAudioClip;

    private Lander lander;
    private void Awake()
    {
        lander = GetComponent<Lander>();    
    }
    private void Start()
    {
        thrusterAudioClip.Pause();
        SoundManager.Instance.onSoundVolumeChanged += SoundManager_onSoundVolumeChanged;

    }

    private void SoundManager_onSoundVolumeChanged(object sender, System.EventArgs e)
    {
        thrusterAudioClip.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }

    private void Update()
    {
        if (lander.state == Lander.State.normal)
        {
            if (GameInput.instance.isUpLanderPressed() || GameInput.instance.isLeftLanderPressed() || GameInput.instance.isRightLanderPressed())
            {
                if (!thrusterAudioClip.isPlaying)
                    thrusterAudioClip.Play();
            }
            else
            {
                thrusterAudioClip.Pause();
            }
        }
        else
        {
            thrusterAudioClip.Pause();
        }
    }
}
