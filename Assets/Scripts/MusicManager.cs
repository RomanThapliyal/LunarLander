using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const int MUSIC_VOLUME_MAX = 10;

    public static MusicManager Instance { get; private set; }

    public event EventHandler onMusicVolumeChanged;

    private static int musicVolume = 4;

    private AudioSource musicAudioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = GetMusicVolumeNormalized();
    }
    private void Start()
    {
        musicAudioSource.volume = GetMusicVolumeNormalized();
    }
    public void ChangeMusicVolume()
    {
        musicVolume = (musicVolume + 1) % (MUSIC_VOLUME_MAX + 1);

        musicAudioSource.volume = GetMusicVolumeNormalized();

        onMusicVolumeChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetMusicVolumeNormalized()
    {
        return (float)musicVolume / MUSIC_VOLUME_MAX;
    }
}