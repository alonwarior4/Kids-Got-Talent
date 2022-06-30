using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoatPlayAnim : MonoBehaviour
{

    ClothShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<ClothShopAnimPlayer>();
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimation(EClothShopState.PlayCoat, "Coat_GTC");
    }
}
