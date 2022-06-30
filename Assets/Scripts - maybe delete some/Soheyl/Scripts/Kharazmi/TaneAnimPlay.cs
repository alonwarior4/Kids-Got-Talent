using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaneAnimPlay : MonoBehaviour
{


    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    SpriteRenderer SpriteRef;
    KharazmiAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;


    [SerializeField] SpriteRenderer FullTreeRef;
    [SerializeField] SpriteRenderer BargRef;

    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManagerRef = GetComponentInParent<KharazmiAnimManager>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        SpriteRef = GetComponent<SpriteRenderer>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }
    private void OnMouseDown()
    {
        PlayAnim();
    }

    private void PlayAnim()
    {
        if(ColorManagerRef.GetCurrentColorName() == "Brown")
        {
            Visibilty();
            ParentAnimatorRef.SetTrigger("Tane_GTC");
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

    public void SetTreeAnimatorState()
    {
        if (AnimManagerRef.GetCurrentState() == EKharazamiAnimatorState.BargPlayed)
        {
            AnimManagerRef.SetCurrentState(EKharazamiAnimatorState.TreeCompeleted);
            ParentAnimatorRef.SetBool("Ball_GTI", true);
            GetComponentInParent<ShowText>().ShowOliverBullshits(1);
        }
        else
        {
            AnimManagerRef.SetCurrentState(EKharazamiAnimatorState.TanePlayed);
            ParentAnimatorRef.SetBool("Tree_StopMoving", true);
        }
    }

    private void Visibilty()
    {
        FullTreeRef.enabled = false;
        SpriteRef.enabled = true;
        BargRef.enabled = true;
    }


}
