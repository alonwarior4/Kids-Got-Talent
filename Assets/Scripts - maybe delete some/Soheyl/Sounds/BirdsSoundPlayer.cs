using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] randomWingSounds;
    public float fade_v;

    private void Start()
    {
        fade_v = 1;
    }

    public void PlaySoundsRandomly()
    {
        if (randomWingSounds.Length == 0) { return; }
        int randomIndex = UnityEngine.Random.Range(0, randomWingSounds.Length);
      
        BirdsSoundManager.singletonInst.playing(randomWingSounds[randomIndex] , fade_v);           
    }
}
