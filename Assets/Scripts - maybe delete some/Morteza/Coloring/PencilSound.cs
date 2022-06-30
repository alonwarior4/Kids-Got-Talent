using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Color Sounds")]
public class PencilSound : ScriptableObject
{
    AudioClip penSound;
    public AudioClip[] pencilSounds;
    
    public AudioClip Sound(string Name)
    {
        for(int i=0; i<pencilSounds.Length; i++)
        {
            if(Name == pencilSounds[i].name)
            {
                penSound = pencilSounds[i];
                break;
            }
            else
            {
                continue;
            }
        }
        return penSound;       
    }
}


