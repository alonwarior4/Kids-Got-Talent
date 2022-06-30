using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlayAnim : MonoBehaviour
{
    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    JungleAnimManager AnimManageRef;
    ColorManager ColorManagerRef;

    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManageRef = GetComponentInParent<JungleAnimManager>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }
    private void OnMouseDown()
    {
        if(AnimManageRef.GetCurrentAnim() == EJungleAnimatorState.SheepColoringPlayed && ColorManagerRef.GetCurrentColorName() == "Purple")
        {
            ParentAnimatorRef.SetTrigger("Cat_GTC");
            AnimManageRef.SetCurrentAnim(EJungleAnimatorState.CatColoringPlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }
}
