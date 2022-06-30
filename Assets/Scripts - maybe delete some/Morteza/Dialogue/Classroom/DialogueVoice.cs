using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueVoice : MonoBehaviour
{

    public void SetDialogueSound(AudioClip DialogueSound )
    {
        GetComponent<AudioSource>().PlayOneShot(DialogueSound);
    }
	
}
