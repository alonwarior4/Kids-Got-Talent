using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenSoundBehaviour : StateMachineBehaviour {

    [SerializeField] AudioClip[] PenSound;
    public float PenSoundVolume = 1f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        AudioSource.PlayClipAtPoint( PenSound[UnityEngine.Random.Range(0 , PenSound.Length)], Camera.main.transform.position, PenSoundVolume);
    }

}
