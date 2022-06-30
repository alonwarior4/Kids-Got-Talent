using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionPlayAnimation : MonoBehaviour
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
        if(ColorManagerRef.GetCurrentColorName() == "Yellow")
        {
            ParentAnimatorRef.SetTrigger("Lion_GTC");
            AnimManagerRef.SetCurrentAnim(EJungleAnimatorState.LionColoringPlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }
}
