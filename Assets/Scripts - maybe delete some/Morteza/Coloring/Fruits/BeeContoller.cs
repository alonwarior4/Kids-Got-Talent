using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeContoller : MonoBehaviour
{
     EFruitShopState shopState = EFruitShopState.NO_Anim;
    public static BeeContoller instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void Checkstate()
    {
        Animator beeAnimator = GetComponent<Animator>();
        switch (shopState)
        {
            case EFruitShopState.NO_Anim:
                break;
            case EFruitShopState.PlayMango:
                beeAnimator.SetTrigger("mango_bee");
                break;
            case EFruitShopState.PlayCarrot:
                beeAnimator.SetTrigger("carrot_bee");
                break;
            case EFruitShopState.PlayPear:
                beeAnimator.SetTrigger("pear_bee");
                break;
            case EFruitShopState.PlayWatermelon:
                beeAnimator.SetTrigger("watermelon_bee");
                break;
            case EFruitShopState.PlayBanana:
                beeAnimator.SetTrigger("banana_bee");
                break;
            case EFruitShopState.PlayApple:
                beeAnimator.SetTrigger("apple_bee");
                break;
            case EFruitShopState.PlayOrange:
                beeAnimator.SetTrigger("orange_bee");
                break;
            case EFruitShopState.PlayCherry:
                beeAnimator.SetTrigger("cherry_bee");
                break;
            case EFruitShopState.PlayGrapes:
                beeAnimator.SetTrigger("grapes_bee");
                break;
            default:
                break;
        }
    }

}
