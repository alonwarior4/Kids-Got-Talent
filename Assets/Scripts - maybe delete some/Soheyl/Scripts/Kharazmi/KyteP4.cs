using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyteP4 : MonoBehaviour {

    KyteParent kyteParentRef;
    [SerializeField] SpriteRenderer ZirRef;
    private void Awake()
    {
        kyteParentRef = GetComponentInParent<KyteParent>();
    }
    private void OnMouseDown()
    {
        kyteParentRef.PlayAnim(ZirRef, "KiteP4_GTC");
        //kyteParentRef.SetPartPlayed(4);
        kyteParentRef.CheckState();
    }
}
