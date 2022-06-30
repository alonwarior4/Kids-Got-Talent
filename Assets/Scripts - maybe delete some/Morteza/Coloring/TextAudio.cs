using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAudio : MonoBehaviour
{
    public static TextAudio instance;
    AudioSource audioSource;



    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetCurrentAudio(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    public void ReadText()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
        else
        {
            StartCoroutine(ReadTextCoroutine());
        }
    }

    public void ReadTextAgain()
    {
        if (audioSource.isPlaying) { return; }
        else
        {
            audioSource.Play();
        }
    }


    IEnumerator ReadTextCoroutine()
    {
        yield return new WaitUntil(() => audioSource.clip.length - audioSource.time < Mathf.Epsilon);
        audioSource.Play();
    }

}
