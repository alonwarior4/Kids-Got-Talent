using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonPlayAnim : MonoBehaviour
{
    FruitShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<FruitShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimState(EFruitShopState.PlayWatermelon, "Watermelon_GTC");
    }
}
