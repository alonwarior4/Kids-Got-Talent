using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_Top : MonoBehaviour
{
    FlagParent ParentRef;
    private void Awake()
    {
        ParentRef = GetComponentInParent<FlagParent>();
    }
    private void OnMouseDown()
    {
        ParentRef.PlayAnim("FlagTop_GTC",true);
    }
}
