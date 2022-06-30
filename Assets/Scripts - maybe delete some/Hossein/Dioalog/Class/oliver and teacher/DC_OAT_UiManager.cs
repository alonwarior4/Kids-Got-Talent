using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DC_OAT_UiManager : MonoBehaviour {

    public static DC_OAT_UiManager instance;

    public GameObject txt_Teacher;
    public Button[] Btn_oliver = new Button[4];
    // Use this for initialization
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void DisableUIOliver()
    {
        for (int i = 0; i < Btn_oliver.Length; i++)
        {
            Btn_oliver[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            Btn_oliver[i].gameObject.SetActive(false);
        }
    }
    public void DisableUITeacher()
    {
        txt_Teacher.SetActive(false);
    }
    public void enableUIOliver(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Btn_oliver[i].gameObject.SetActive(true);
        }
    }
    public void enableUITeacher()
    {
        txt_Teacher.SetActive(true);
    }



}
