using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BargPlayAnim : MonoBehaviour
{

    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    SpriteRenderer SpriteRef;
    KharazmiAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;

    [SerializeField] SpriteRenderer FullTreeRef;
    [SerializeField] SpriteRenderer TaneRef;

    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        SpriteRef = GetComponent<SpriteRenderer>();
        AnimManagerRef = GetComponentInParent<KharazmiAnimManager>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }

    private void OnMouseDown()
    {
        PlayAnim();
    }

    private void PlayAnim()
    {
        if (ColorManagerRef.GetCurrentColorName() == "Green")
        {
            Visibilty();
            ParentAnimatorRef.SetTrigger("Barg_GTC");
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

    public void SetTreeAnimatorState()
    {
        if (AnimManagerRef.GetCurrentState() == EKharazamiAnimatorState.TanePlayed)
        {
            AnimManagerRef.SetCurrentState(EKharazamiAnimatorState.TreeCompeleted);
            ParentAnimatorRef.SetBool("Ball_GTI",true);
            GetComponentInParent<ShowText>().ShowOliverBullshits(1);
        }
        else
        {
            AnimManagerRef.SetCurrentState(EKharazamiAnimatorState.BargPlayed);
            ParentAnimatorRef.SetBool("Tree_StopMoving",true);
        }
    }

    private void Visibilty()
    {
        FullTreeRef.enabled = false;
        SpriteRef.enabled = true;
        TaneRef.enabled = true;
    }

}
