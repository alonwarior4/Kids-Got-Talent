using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HajiFiroozSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClipsForRandom;
    [SerializeField] float soundVolume = 1;

    //Cache
    private void Awake()
    {
    }
    public void PlaySoundsRandomly()
    {
        //if(changeColorRef.isEraser) { return; }
        if (audioClipsForRandom.Length == 0) { return; }
        int randomIndex = UnityEngine.Random.Range(0, audioClipsForRandom.Length);
        AudioSource.PlayClipAtPoint(audioClipsForRandom[randomIndex], Camera.main.transform.position,soundVolume);
    }
}
