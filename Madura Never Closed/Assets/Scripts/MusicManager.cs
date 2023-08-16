using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;

    private float currentVolume = 1f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        currentVolume = PlayerPrefs.GetFloat(PLAYER_MUSIC_VOLUME, 1f);
        audioSource.volume = currentVolume;
    }

    public void SetVolume(float volumeNumber)
    {
        currentVolume = volumeNumber;
        audioSource.volume = currentVolume;

        PlayerPrefs.SetFloat(PLAYER_MUSIC_VOLUME, currentVolume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return currentVolume;
    }
}
