using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHajiFiroozColors : MonoBehaviour
{
    #region ColorsRef
    [SerializeField] Color32 Yellow;
    [SerializeField] Color32 Brown;
    [SerializeField] Color32 Green;
    [SerializeField] Color32 Orange;
    [SerializeField] Color32 LightBlue;
    [SerializeField] Color32 Black;
    [SerializeField] Color32 DarkRed;
    [SerializeField] Color32 DarkBlue;
    [SerializeField] Color32 Gray;
    #endregion
    ColorManager ColorManagerRef;

    private void Awake()
    {
        ColorManagerRef = GetComponent<ColorManager>();
    }

    public Color32 GetColors()
    {
        switch (ColorManagerRef.GetCurrentColorName())
        {
            case "Yellow":
                return Yellow;
            case "Brown":
                return Brown;
            case "Green":
                return Green;
            case "Orange":
                return Orange;
            case "LightBlue":
                return LightBlue;
            case "Black":
                return Black;
            case "DarkRed":
                return DarkRed;
            case "DarkBlue":
                return DarkBlue;
            case "Gray":
                return Gray;
            default:
                return new Color32(255, 255, 255, 255);
        }
    }
}
