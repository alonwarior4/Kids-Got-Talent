using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator[] friendsAnimator ;
    string[] triggers = { "HeadLeft", "HeadRight" };

    private void Start()
    {
        StartCoroutine(SelectFirendHead());
    }

    IEnumerator SelectFirendHead()
    {
        int friendsNumbers;
        int friendIndex;
        List<Animator> chosenAnimators = new List<Animator>();
        yield return new WaitForSeconds(2);
        while (true)
        {
            friendsNumbers = UnityEngine.Random.Range(1, friendsAnimator.Length - 3);
            for (int Number = 0; Number <= friendsNumbers ; Number++)
            {
                friendIndex = UnityEngine.Random.Range(0, 4);
                while (chosenAnimators.Contains(friendsAnimator[friendIndex]))
                {
                    friendIndex = UnityEngine.Random.Range(0, 4);
                }
                friendsAnimator[friendIndex].SetTrigger(triggers[UnityEngine.Random.Range(0 , triggers.Length)]);
                chosenAnimators.Add(friendsAnimator[friendIndex]);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f , 1.5f));
            }
            chosenAnimators.Clear();
            yield return new WaitForSeconds(UnityEngine.Random.Range(7.5f, 10));
        }
    }
}
