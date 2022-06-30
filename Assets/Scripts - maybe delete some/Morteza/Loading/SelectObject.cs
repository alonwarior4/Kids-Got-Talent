using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField] LoadingObjects loadingObjects;

    public void SelectFromObjects()
    {
        loadingObjects.SelectNewObject();
    }
	
}
