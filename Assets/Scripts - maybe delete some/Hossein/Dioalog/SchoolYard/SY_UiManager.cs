using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SY_UiManager : MonoBehaviour {

    public static SY_UiManager instance;
    public GameObject g_oliverfather;

    public TMP_Text txt_asghar;
    public Button[] Btn_oliver = new Button[4];
	// Use this for initialization
	void Start () {
        if (!instance)
        {
            instance = this;
        }
        g_oliverfather.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void DisableUIOliver()
    {
        for (int i = 0; i < Btn_oliver.Length; i++)
        {
            Btn_oliver[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            Btn_oliver[i].gameObject.SetActive(false);
        }
    }
    public void DisableUIAsghar()
    {
        txt_asghar.GetComponent<TMP_Text>().enabled = false;
    }
    public void enableUIOliver(int n )
    {
        for (int i = 0; i < n; i++)
        {
            Btn_oliver[i].gameObject.SetActive(true);
        }
    }
    public void enableUIAsghar()
    {
        txt_asghar.GetComponent<TMP_Text>().enabled = true;
    }



}
