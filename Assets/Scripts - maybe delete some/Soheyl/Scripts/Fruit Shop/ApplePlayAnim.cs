using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePlayAnim : MonoBehaviour
{
    FruitShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<FruitShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimState(EFruitShopState.PlayApple, "Apple_GTC");
    }
}
