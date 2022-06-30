using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyteP2 : MonoBehaviour
{
    KyteParent kyteParentRef;
    [SerializeField] SpriteRenderer ZirRef;
    private void Awake()
    {
        kyteParentRef = GetComponentInParent<KyteParent>();
    }
    private void OnMouseDown()
    {
        kyteParentRef.PlayAnim(ZirRef, "KiteP2_GTC");
        //kyteParentRef.SetPartPlayed(2);
        kyteParentRef.CheckState();
    }

}
