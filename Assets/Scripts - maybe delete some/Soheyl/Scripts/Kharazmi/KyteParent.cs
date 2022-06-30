using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyteParent : MonoBehaviour
{


    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    KharazmiAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;
    ShowText ShowTextRef;
    [SerializeField] Color32 YellowPenColor;
    [SerializeField] Color32 RedPenColor;
    [SerializeField] Color32 BluePenColor;
    [SerializeField] Color32 GreenPenColor;
    [SerializeField] Color32 NullBlack;

    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManagerRef = GetComponentInParent<KharazmiAnimManager>();
        ShowTextRef = GetComponentInParent<ShowText>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }

    public void PlayAnim(SpriteRenderer ZirRef,string TriggerName)
    {
        string CurrentColorName = ColorManagerRef.GetCurrentColorName();
        bool isItTurn = AnimManagerRef.GetCurrentState() == EKharazamiAnimatorState.TaxiPlayed;
        bool isItColored = ZirRef.color == new Color32(255, 255, 255, 255); //White
        bool isCorrectColorSelected = (CurrentColorName == "Yellow" || CurrentColorName == "Green" || CurrentColorName == "Red" || CurrentColorName == "Blue");
        if (isItTurn && isItColored && isCorrectColorSelected ) 
        {
            Color32 SelectedColor = CheckAndSetKiteColors(ColorManagerRef.GetCurrentColorName());
            if(!SelectedColor.Equals(NullBlack))
            {
                ZirRef.color = SelectedColor;
                ParentAnimatorRef.SetTrigger(TriggerName);
            }
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

    #region KiteColoring
    private bool isYellowColored = false;
    private bool isGreenColored = false;
    private bool isRedColored = false;
    private bool isBlueColored = false;

    public Color32 CheckAndSetKiteColors(string PickedColorName)
    {
        switch (PickedColorName)
        {
            case "Yellow":
                if (!isYellowColored)
                {
                    isYellowColored = true;
                    return YellowPenColor;
                }
                return NullBlack;

            case "Green":
                if (!isGreenColored)
                {
                    isGreenColored = true;
                    return GreenPenColor;
                }
                return NullBlack;

            case "Red":
                if (!isRedColored)
                {
                    isRedColored = true;
                    return RedPenColor;
                }
                return NullBlack;

            case "Blue":
                if (!isBlueColored)
                {
                    isBlueColored = true;                   
                    return BluePenColor;
                }
                return NullBlack;

            default:
                return new Color32(255, 255, 255, 255);
        }
    }
    #endregion


    #region KyteState

    public void CheckState()
    {
        if (isBlueColored && isGreenColored && isRedColored && isYellowColored)
        {
            AnimManagerRef.SetCurrentState(EKharazamiAnimatorState.KyteCompeleted);
            Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();
            for(int i=0; i<childColliders.Length; i++)
            {
                childColliders[i].enabled = false;
            }
            ShowTextRef.ShowOliverBullshits(4);
            Invoke("PlayFlagIdle", 3f);
        }
    }
    #endregion

    private void PlayFlagIdle()
    {
        ParentAnimatorRef.SetTrigger("Flag_GTI");
    }
}
