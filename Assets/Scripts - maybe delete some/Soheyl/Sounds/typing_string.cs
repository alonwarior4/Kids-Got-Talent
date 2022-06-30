using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class typing_string : MonoBehaviour {
    private string typing;
    public TMP_Text header;
	// Use this for initialization
	void Start () {
        //header_set();125
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator convert_to_type()
    {
        for (int i = 0; i < typing.Length; i++)
        {
            yield return new WaitForSeconds(.05f);
            header.text += typing[i];
        }
    }

    public void header_set()
    {           
        typing = header.text;
        header.text = "";
        StartCoroutine(convert_to_type());

    }
}
