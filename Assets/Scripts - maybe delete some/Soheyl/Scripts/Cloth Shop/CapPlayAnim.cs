using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapPlayAnim : MonoBehaviour
{
    ClothShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<ClothShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimation(EClothShopState.PlayCap, "Cap_GTC");
    }
}
