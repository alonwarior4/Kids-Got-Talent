using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

///that means oliver and asghar
public class DC_OAA_Uimanager : MonoBehaviour {

    public static DC_OAA_Uimanager instance;

    public GameObject parent_ui_npc;
    public Button[] Btn_oliver = new Button[4];
    public GameObject FaceAsghar;
    public GameObject Faceteacher;
    public GameObject bgAsghar;
    public GameObject bgteacher;
    public TMP_Text asgharBullshit;
    // Use this for initialization
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        asgharBullshit.text = "";

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
    public void DisableUIAsghar()
    {
        parent_ui_npc.SetActive(false);
    }
    public void enableUIOliver(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Btn_oliver[i].gameObject.SetActive(true);
        }
    }

    public void enableUIAsghar()
    {
        parent_ui_npc.SetActive(true);
    }


    public void SetFaceAsghar()
    {
        FaceAsghar.SetActive(true);
        Faceteacher.SetActive(false);
        bgAsghar.SetActive(true);
        bgteacher.SetActive(false);
    }

    public void SetFaceteacher()
    {
        FaceAsghar.SetActive(false);
        Faceteacher.SetActive(true);
        bgAsghar.SetActive(false);
        bgteacher.SetActive(true);

    }
}
