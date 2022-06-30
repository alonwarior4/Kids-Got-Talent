using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtPlayAnim : MonoBehaviour
{
    ClothShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<ClothShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimation(EClothShopState.PlayShirt,"Shirt_GTC");
    }
}
