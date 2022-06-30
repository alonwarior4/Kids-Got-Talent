using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitPlayAnim : MonoBehaviour
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
        if(AnimManagerRef.GetCurrentAnim() == EJungleAnimatorState.CatColoringPlayed && ColorManagerRef.GetCurrentColorName() == "Pink")
        {
            ParentAnimatorRef.SetTrigger("Rabbit_GTC");
            AnimManagerRef.SetCurrentAnim(EJungleAnimatorState.RabbitColoringPlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

}
