using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlayAnim : MonoBehaviour
{
    FruitShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<FruitShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimState(EFruitShopState.PlayOrange, "Orange_GTC");
    }
}
