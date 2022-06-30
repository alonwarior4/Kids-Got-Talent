using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapesPlayAnim : MonoBehaviour
{
    FruitShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<FruitShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimState(EFruitShopState.PlayGrapes, "Grapes_GTC");
    }
}
