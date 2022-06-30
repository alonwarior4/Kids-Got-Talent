using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyteP1 : MonoBehaviour
{
    KyteParent kyteParentRef;
    [SerializeField] SpriteRenderer ZirRef;
    private void Awake()
    {
        kyteParentRef = GetComponentInParent<KyteParent>();
    }
    private void OnMouseDown()
    {
        kyteParentRef.PlayAnim(ZirRef,"KiteP1_GTC");
        //kyteParentRef.SetPartPlayed(1);
        kyteParentRef.CheckState();
    }
}
