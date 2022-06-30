using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliverMainMenu : MonoBehaviour
{
    Animator oliverMainMenuAnimator;
    [Tooltip("Must Be 1.5x")][SerializeField] float animationDelay;
    [Tooltip("first delay for first animation and must be 1.5x")] [SerializeField] float startAnimationDelay;
    string[] animatorTriggers = {"HandShake" , "Pointing" , "KharKhar" };

    private void Awake()
    {
        oliverMainMenuAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(OliverWellcome());
    }

    IEnumerator OliverWellcome()
    {
        yield return new WaitForSeconds(startAnimationDelay);
        while (true)
        {
            int triggerIndex = UnityEngine.Random.Range(0, animatorTriggers.Length);
            oliverMainMenuAnimator.SetTrigger(animatorTriggers[triggerIndex]);
            yield return new WaitForSeconds(animationDelay);
        }
    }
}
