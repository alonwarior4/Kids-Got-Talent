using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPlayAnim : MonoBehaviour
{
    ClothShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<ClothShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimation(EClothShopState.PlayGlass, "Glass_GTC");
    }
}
