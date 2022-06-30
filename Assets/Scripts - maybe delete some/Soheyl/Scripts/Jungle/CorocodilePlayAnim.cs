using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorocodilePlayAnim : MonoBehaviour
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
        if(AnimManagerRef.GetCurrentAnim() == EJungleAnimatorState.RabbitColoringPlayed && ColorManagerRef.GetCurrentColorName() == "Green")
        {
            ParentAnimatorRef.SetTrigger("Corocodile_GTC");
            AnimManagerRef.SetCurrentAnim(EJungleAnimatorState.CorocodileColoringPlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }
}
