using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseSoundBehaviour : StateMachineBehaviour
{
    [SerializeField] AudioClip eraserSound;
    public float eraserSoundVolume = 1f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        AudioSource.PlayClipAtPoint(eraserSound, Camera.main.transform.position, eraserSoundVolume);
    }
} 
