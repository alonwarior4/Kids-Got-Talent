using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPlayAnim : MonoBehaviour
{

    FruitShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<FruitShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimState(EFruitShopState.PlayBanana, "Banana_GTC");
    }
}
