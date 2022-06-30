using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlayAnim : MonoBehaviour
{


    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    KharazmiAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;
    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManagerRef = GetComponentInParent<KharazmiAnimManager>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();

    }
    private void OnMouseDown() 
    {
        if (AnimManagerRef.GetCurrentState() == EKharazamiAnimatorState.TreeCompeleted && ColorManagerRef.GetCurrentColorName() == "Pink") 
        {
            ParentAnimatorRef.SetTrigger("Ball_GTC");
            AnimManagerRef.SetCurrentState(EKharazamiAnimatorState.BallPlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

}
