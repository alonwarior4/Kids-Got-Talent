using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoePlayAnim : MonoBehaviour
{

    ClothShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<ClothShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimation(EClothShopState.PlayShoe, "Shoe_GTC");
    }
}
