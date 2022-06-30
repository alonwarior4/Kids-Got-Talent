using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPlayAnim : MonoBehaviour
{
    ClothShopAnimPlayer AnimPlayerRef;
    private void Awake()
    {
        AnimPlayerRef = GetComponentInParent<ClothShopAnimPlayer>();
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => Paper.instance.IsPaperEnd);
        //this.GetComponentInParent<Animator>().SetTrigger("StartAnim");
    }
    private void OnMouseDown()
    {
        AnimPlayerRef.CheckAnimation(EClothShopState.PlayBag, "Bag_GTC");
    }
}
