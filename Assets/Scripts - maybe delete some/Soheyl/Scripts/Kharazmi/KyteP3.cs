using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyteP3 : MonoBehaviour {

    KyteParent kyteParentRef;
    [SerializeField] SpriteRenderer ZirRef;
    private void Awake()
    {
        kyteParentRef = GetComponentInParent<KyteParent>();
    }
    private void OnMouseDown()
    {
        kyteParentRef.PlayAnim(ZirRef, "KiteP3_GTC");
        //kyteParentRef.SetPartPlayed(3);
        kyteParentRef.CheckState();
    }
}
