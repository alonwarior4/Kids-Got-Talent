using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsSoundManager : MonoBehaviour
{
    public static BirdsSoundManager singletonInst;
    private AudioSource thisaudio;


    private void Awake()
    {
        if(!singletonInst)
        {
            singletonInst = this;
        }
    }

    private void Start()
    {
        thisaudio = GetComponent<AudioSource>();
    }
   
    public void playing(AudioClip c , float v)
    {
        thisaudio.PlayOneShot(c, v);
        //if (thisaudio.isPlaying)
        //{
        //    thisaudio.Play();
        //}
        //else
        //{
        //    thisaudio.PlayOneShot(c, v);
        //}
    }

    private void OnDestroy()
    {
        singletonInst = null;
    }
}
