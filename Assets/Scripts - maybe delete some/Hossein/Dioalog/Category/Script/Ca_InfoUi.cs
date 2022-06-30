using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ca_InfoUi : MonoBehaviour
{

	public string StateOption;
	public Ca_TypesObjectsEnum Type;
    public AudioClip thisObjSound;
    public bool isQuestion;

    public void EnableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnMouseDown()
    {
        if (isQuestion)
        {
            StartCoroutine(PlayQuestionSound());
        }
        else
        {
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(thisObjSound, Camera.main.transform.position, 2f);
    }

    IEnumerator PlayQuestionSound()
    {
        GetComponent<BoxCollider2D>().enabled = false;        
        AudioSource.PlayClipAtPoint(thisObjSound, Camera.main.transform.position, 2f);
        yield return new WaitForSeconds(1.25f);
        GetComponent<BoxCollider2D>().enabled = true;
    }


}
