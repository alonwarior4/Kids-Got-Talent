using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPlayAnim : MonoBehaviour
{

    FruitShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<FruitShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimState(EFruitShopState.PlayCherry, "Cherry_GTC");
    }
}
