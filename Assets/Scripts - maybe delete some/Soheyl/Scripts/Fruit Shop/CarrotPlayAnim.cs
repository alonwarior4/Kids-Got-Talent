using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPlayAnim : MonoBehaviour
{
    FruitShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<FruitShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimState(EFruitShopState.PlayCarrot, "Carrot_GTC");
    }
}
