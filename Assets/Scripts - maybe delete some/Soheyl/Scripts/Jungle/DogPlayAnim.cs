using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPlayAnim : MonoBehaviour
{

    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    JungleAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;
    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManagerRef = GetComponentInParent<JungleAnimManager>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }
    private void OnMouseDown()
    {
        if(AnimManagerRef.GetCurrentAnim() == EJungleAnimatorState.LionColoringPlayed && ColorManagerRef.GetCurrentColorName() == "Brown")
        {
            ParentAnimatorRef.SetTrigger("Dog_GTC");
            AnimManagerRef.SetCurrentAnim(EJungleAnimatorState.DogColoringPlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }
}
