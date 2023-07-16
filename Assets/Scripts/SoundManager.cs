using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private Sound GunSound;
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(string str)
    {
        switch (str)
        {
            case "GunSound":
                PlayDirect(GunSound);
                break;
            default:
                break;
        }
    }

    private void PlayDirect(Sound audio)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.clip = audio.audioClip[Random.Range(0, audio.audioClip.Length)];
        audioSource.dopplerLevel = 0;
        audioSource.reverbZoneMix = 0;
        audioSource.volume = Random.Range(audio.minVolume, audio.maxVolume);
        audioSource.pitch = Random.Range(audio.minPitch, audio.maxPitch);
        audioSource.Play();
        Destroy(audioSource, 1f);
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip[] audioClip;
    [Range(0, 2)]
    public float minVolume = 1;
    [Range(0, 2)]
    public float maxVolume = 1;
    [Range(0, 2)]
    public float minPitch = 1;
    [Range(0, 2)]
    public float maxPitch = 1;
}
