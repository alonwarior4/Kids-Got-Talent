using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoPlayAnim : MonoBehaviour
{
    FruitShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<FruitShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimState(EFruitShopState.PlayMango,"Mango_GTC");
    }
}
