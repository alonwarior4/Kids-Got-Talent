using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsPlayAnim : MonoBehaviour
{
    ClothShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<ClothShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimation(EClothShopState.PlayPants, "Pants_GTC");
    }
}
