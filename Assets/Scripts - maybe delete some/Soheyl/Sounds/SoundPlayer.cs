using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SoundPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClipsForRandom;
    [SerializeField] AudioClip[] audioClipsForRandom_1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolumeScale(float scaleToSet)
    {
        audioSource.volume = scaleToSet;
    }

    public void PlayAudioClip(AudioClip audioToPlay)
    {
        audioSource.PlayOneShot(audioToPlay);
    }

    public void StopAudioSource()
    {
        audioSource.Stop();
    }

    public void PlaySoundsRandomly()
    {
        if(audioClipsForRandom.Length == 0) { return; }
        int randomIndex = UnityEngine.Random.Range(0, audioClipsForRandom.Length);
        audioSource.PlayOneShot(audioClipsForRandom[randomIndex]);
    }
    public void PlaySoundsRandomly_1()
    {
        if (audioClipsForRandom_1.Length == 0) { return; }
        int randomIndex = UnityEngine.Random.Range(0, audioClipsForRandom_1.Length);
        audioSource.PlayOneShot(audioClipsForRandom_1[randomIndex]);
    }
}
