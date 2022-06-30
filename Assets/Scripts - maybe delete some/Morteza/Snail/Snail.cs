using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    float moveSpeed;
    [SerializeField] AudioClip snailSound;
    [Tooltip("cooldown between two headturn animation when clicked")] [SerializeField] float Cooldown;

    //cash ref
    Animator snailAnimatior;

    // conditions to trigger animation
    bool isAnimationFinished = false;    // if movement animation reach the end
    bool isRunningCoroutine = false;    // if transition animation coroutine is running (cooldown for headturn animation)
    bool isClicked = false;  // when clicked it has permission to change the state of animation




    private void Awake()
    {
        snailAnimatior = GetComponent<Animator>();
    }
 
  

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(snailSound, Camera.main.transform.position);
    }
  
    private void OnMouseDown()
    {
        isClicked = true;
        if (!isRunningCoroutine)
        {
            StartCoroutine(CheckTheEnd());
        }             
    }

    IEnumerator CheckTheEnd()
    {
        isRunningCoroutine = true;
        if (snailAnimatior.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            snailAnimatior.speed = 2f;
        }
        while (!isAnimationFinished)
        {
            yield return null;
        }
        snailAnimatior.speed = 1f;
        snailAnimatior.SetTrigger("HeadTurn");
        isAnimationFinished = false;
        yield return new WaitForSeconds(Cooldown);
        isRunningCoroutine = false;
        isClicked = false;
    }

    public void EndOfAnimation()
    {
        if (isClicked)
        {
            isAnimationFinished = true;
        }
    }


    

   
}
