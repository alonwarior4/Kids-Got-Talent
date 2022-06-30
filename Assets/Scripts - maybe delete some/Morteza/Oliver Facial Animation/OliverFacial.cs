using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliverFacial : MonoBehaviour
{
    Animator oliverAnimator;
    [SerializeField] UIManagement uiManagement;

    private void Awake()
    {
        oliverAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// state = 1 for false , 2 for nothing after change , 3 for true
    /// </summary>
    /// <param name="state"></param>
    public void SetOliverAnimatorState(int state)
    {
        if(uiManagement.isInUI == false)
        {
            oliverAnimator.SetInteger("Answer", state);
        }       
    }
	
}
