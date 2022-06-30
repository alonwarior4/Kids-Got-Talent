using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagParent : MonoBehaviour
{


    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    KharazmiAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;
    ShowText ShowTextRef;
    [SerializeField] SpriteRenderer FullFlag;
    [SerializeField] SpriteRenderer ChoobFlag;
    [SerializeField] SpriteRenderer BotFlag;
    [SerializeField] SpriteRenderer TopFlag;
    [SerializeField] SpriteRenderer SefidFlag;

    public bool TopColored = false;
    public bool BotColored = false;
    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManagerRef = GetComponentInParent<KharazmiAnimManager>();
        ShowTextRef = GetComponentInParent<ShowText>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();

    }
    /// <summary>
    /// TopOrBot => True for Top and False for Bot
    /// </summary> 
    /// <param name="ConditionName"></param>
    /// <param name="TopOrBot"></param>
    public void PlayAnim(string ConditionName, bool TopOrBot)
    {
        if (AnimManagerRef.GetCurrentState() == EKharazamiAnimatorState.KyteCompeleted && ((ColorManagerRef.GetCurrentColorName() == "Green" && TopOrBot) || (ColorManagerRef.GetCurrentColorName() == "Red" && !TopOrBot)))
        {
            Visibilty();
            ParentAnimatorRef.SetBool(ConditionName,true);
            CheckAndSetState(TopOrBot);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }

    }

    public void Visibilty()
    {
        FullFlag.enabled = false;
        ChoobFlag.enabled = true;
        BotFlag.enabled = true;
        TopFlag.enabled = true;
        SefidFlag.enabled = true;
    }

    private void CheckAndSetState(bool TopOrBot)
    {
        if(TopOrBot)
        {
            TopColored = true;
        }
        else
        {
            BotColored = true;            
        }

        if(TopColored && BotColored)
        {
            AnimManagerRef.SetCurrentState(EKharazamiAnimatorState.FlagCompeleted);
            Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();
            for(int i =0; i<childColliders.Length; i++)
            {
                childColliders[i].enabled = false;
            }
            Invoke("ShowAirPlaneText", 3f);
            ParentAnimatorRef.SetTrigger("RedLight");
        }
    }
    private void ShowAirPlaneText()
    {
        ShowTextRef.ShowOliverBullshits(5);
    }
}
