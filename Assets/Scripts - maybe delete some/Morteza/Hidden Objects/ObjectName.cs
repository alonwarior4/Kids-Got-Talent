using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] objectIndex = new TextMeshProUGUI[13];

    private void Start()
    {
        foreach(TextMeshProUGUI index in objectIndex)
        {
            GetComponent<TextMeshProUGUI>();
        }
    }

    public void changeText(int hintIndex)
    {
        objectIndex[hintIndex].color = new Color(0.11f , 0.68f , 0.67f);
        objectIndex[hintIndex].fontStyle = FontStyles.Strikethrough;
    }

}
