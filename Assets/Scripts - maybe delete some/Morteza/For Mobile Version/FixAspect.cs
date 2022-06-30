using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAspect : MonoBehaviour
{
    [SerializeField] GameObject forceAspect;

	void Start ()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float aspectRate = screenWidth / screenHeight;
        
        if(aspectRate < 1.76)
        {
            forceAspect.SetActive(true);
        }
        else
        {
            RectTransform canvasRect = GetComponent<RectTransform>();

            float canvasHeight = canvasRect.sizeDelta.y;
            float newCanvasWidth = canvasHeight * aspectRate;

            canvasRect.sizeDelta = new Vector2(newCanvasWidth, canvasHeight);
        }
	}
	
	
	
}
