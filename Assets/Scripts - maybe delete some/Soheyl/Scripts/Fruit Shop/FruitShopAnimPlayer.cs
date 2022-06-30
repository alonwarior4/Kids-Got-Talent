using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitShopAnimPlayer : MonoBehaviour
{

    OliverFacial oliverFacialAnimation;
    Animator AnimatorRef;
    FruitShopAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;

    private void Awake()
    {
        AnimatorRef = GetComponent<Animator>();
        AnimManagerRef = GetComponent<FruitShopAnimManager>();
        ColorManagerRef = GetComponent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }

    public void CheckAnimState(EFruitShopState StateToPlay,string TriggerName)
    {
        switch(StateToPlay)
        {
            case EFruitShopState.PlayMango:
                if(ColorManagerRef.GetCurrentColorName() == "Yellow")
                {
                    AnimatorRef.SetTrigger(TriggerName);
                    AnimManagerRef.SetCurrentState(StateToPlay);
                    oliverFacialAnimation.SetOliverAnimatorState(3);
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EFruitShopState.PlayCarrot:
                if (ColorManagerRef.GetCurrentColorName() == "Orange")
                    PlayAnimation(StateToPlay,EFruitShopState.PlayMango,TriggerName);
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EFruitShopState.PlayPear:
                if (ColorManagerRef.GetCurrentColorName() == "Yellow")
                    PlayAnimation(StateToPlay,EFruitShopState.PlayCarrot,TriggerName);
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EFruitShopState.PlayWatermelon:
                if (ColorManagerRef.GetCurrentColorName() == "Green")
                    PlayAnimation(StateToPlay, EFruitShopState.PlayPear, TriggerName);
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EFruitShopState.PlayBanana:
                if (ColorManagerRef.GetCurrentColorName() == "Yellow")
                    PlayAnimation(StateToPlay,EFruitShopState.PlayWatermelon,TriggerName);
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EFruitShopState.PlayApple:
                if (ColorManagerRef.GetCurrentColorName() == "Red")
                    PlayAnimation(StateToPlay, EFruitShopState.PlayBanana, TriggerName);
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EFruitShopState.PlayOrange:
                if (ColorManagerRef.GetCurrentColorName() == "Orange")
                    PlayAnimation(StateToPlay,EFruitShopState.PlayApple,TriggerName);
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EFruitShopState.PlayCherry:
                if (ColorManagerRef.GetCurrentColorName() == "DarkRed")
                    PlayAnimation(StateToPlay, EFruitShopState.PlayOrange, TriggerName);
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

            case EFruitShopState.PlayGrapes:
                if (ColorManagerRef.GetCurrentColorName() == "Purple")
                {
                    PlayAnimation(StateToPlay, EFruitShopState.PlayCherry, TriggerName);
                    GetComponentInChildren<GrapesPlayAnim>().gameObject.GetComponent<Collider2D>().enabled = false;
                    UIManagement uiManager = FindObjectOfType<UIManagement>();
                    uiManager.TellFinalStory();
                    uiManager.isEndOfColoring = true;
                }
                else
                {
                    oliverFacialAnimation.SetOliverAnimatorState(1);
                }
                break;

        }
    }

    private void PlayAnimation(EFruitShopState StateToSet,EFruitShopState StateToCheck,string TriggerName)
    {
        if(AnimManagerRef.GetCurrentState() == StateToCheck)
        {
            AnimatorRef.SetTrigger(TriggerName);
            AnimManagerRef.SetCurrentState(StateToSet);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

    public void BeeFruitsSelecter(string animatorTrigger)
    {
        BeeContoller.instance.gameObject.GetComponent<Animator>().SetTrigger(animatorTrigger);
    }


}
