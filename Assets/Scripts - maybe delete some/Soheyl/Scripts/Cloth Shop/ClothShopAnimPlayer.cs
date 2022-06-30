using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothShopAnimPlayer : MonoBehaviour
{
    OliverFacial oliverFacialAnimation;
    Animator AnimatorRef;
    ClothShopAnimatorManager AnimManagerRef;
    ColorManager ColorManagerRef;
    private void Awake()
    {
        AnimatorRef = GetComponent<Animator>();
        AnimManagerRef = GetComponent<ClothShopAnimatorManager>();
        ColorManagerRef = GetComponent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }

    public void CheckAnimation(EClothShopState StateWantToPlay , string TriggerName)
    {
        //TODO Refactor if's , if you wish !
        switch(StateWantToPlay)
        {
            case EClothShopState.PlayBag:
               if(ColorManagerRef.GetCurrentColorName() == "Brown")
                {
                    AnimatorRef.SetTrigger(TriggerName);
                    AnimManagerRef.SetCurrentState(EClothShopState.PlayBag);
                    oliverFacialAnimation.SetOliverAnimatorState(3);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EClothShopState.PlayDress:
                if (ColorManagerRef.GetCurrentColorName() == "Pink")
                {
                    PlayAnimation(TriggerName, StateWantToPlay, EClothShopState.PlayBag);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EClothShopState.PlayShirt:
                if (ColorManagerRef.GetCurrentColorName() == "Green")
                {
                    PlayAnimation(TriggerName, StateWantToPlay, EClothShopState.PlayDress);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EClothShopState.PlayGlass:
                if (ColorManagerRef.GetCurrentColorName() == "Black")
                {
                    PlayAnimation(TriggerName, StateWantToPlay, EClothShopState.PlayShirt);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EClothShopState.PlayHat:
                if (ColorManagerRef.GetCurrentColorName() == "Purple")
                {
                    PlayAnimation(TriggerName, StateWantToPlay, EClothShopState.PlayCap);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EClothShopState.PlayCap:
                if (ColorManagerRef.GetCurrentColorName() == "LightBlue")
                {
                    PlayAnimation(TriggerName, StateWantToPlay, EClothShopState.PlayGlass);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EClothShopState.PlayCoat:
                if (ColorManagerRef.GetCurrentColorName() == "DarkBlue")
                {
                    PlayAnimation(TriggerName, StateWantToPlay, EClothShopState.PlayHat);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EClothShopState.PlayPants:
                if (ColorManagerRef.GetCurrentColorName() == "Black")
                {
                    PlayAnimation(TriggerName, StateWantToPlay, EClothShopState.PlayCoat);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EClothShopState.PlayShoe:
                if (ColorManagerRef.GetCurrentColorName() == "Yellow")
                {
                    PlayAnimation(TriggerName, StateWantToPlay, EClothShopState.PlayPants);
                    UIManagement uiManager = FindObjectOfType<UIManagement>();
                    uiManager.TellFinalStory();
                    uiManager.isEndOfColoring = true;
                    GetComponentInChildren<ShoePlayAnim>().gameObject.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

        }

    }
    private void PlayAnimation(string TriggerName,EClothShopState StateToPlay , EClothShopState StateToCheck)
    {
        if (AnimManagerRef.GetCurrentState() == StateToCheck)
        {           
            AnimatorRef.SetTrigger(TriggerName);
            AnimManagerRef.SetCurrentState(StateToPlay);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

}
