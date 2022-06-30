using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] string orderLayerName;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().sortingLayerName = orderLayerName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
