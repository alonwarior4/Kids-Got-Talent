using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AC_WithSms : MonoBehaviour {

    public TMP_InputField inputCd;
    public TMP_InputField inputreccivecode;
    public TMP_Text textencoded;
    public GameObject GetCD;
    public GameObject panelSendmsg;
    public GameObject panelGetcodeserver;

    public GameObject G_o_Erorr;

    public Sprite WrongCdkey;
    public Sprite CdkeyAnother;
    public Sprite uiMask;
    // Use this for initialization

    void OnEnable()
    {
        SetnullErorr();
    }
    void Start() {

        GetCD.SetActive(true);
        panelGetcodeserver.SetActive(false);
        panelSendmsg.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }


    public void encode()
    {
        if (inputCd.text.Length < 6)
        {
            //Debug.LogError("cd code is not correct");
            G_o_Erorr.GetComponent<Image>().sprite = WrongCdkey;
        }

        else
        {
            if (CheckCdCode() == false)
            {
               // Debug.LogError("cd code not exist");
                G_o_Erorr.GetComponent<Image>().sprite = WrongCdkey;
            }
            else
            {
                string systemcode = Getdevicecode();
                string cdkey = inputCd.text[1].ToString()+ inputCd.text[2].ToString()+ inputCd.text[3].ToString()+ inputCd.text[4].ToString();
                string finalcode = cdkey[0].ToString() + cdkey[1].ToString() + systemcode[0].ToString() + systemcode[1].ToString() + cdkey[2].ToString() + cdkey[3].ToString() + systemcode[2].ToString() + systemcode[3].ToString();

                string encoded = convertstring(finalcode);
                textencoded.text = encoded;

                GetCD.SetActive(false);
                panelSendmsg.SetActive(true);
                ///cd +device
            }
        }
    }

    private bool CheckCdCode()
    {
       int c5 = (int)inputCd.text[0];
       int c6 = (int)inputCd.text[inputCd.text.Length - 1];

        
        int n = c5 - c6;

        if (Mathf.Abs(n) == 13)
        {
            return true;
        }
        else
            return false;
    }

    private string convertstring(string St_in)
    {
        string stOut = "";
        for (int i = 0; i < St_in.Length; i++)
        {
            char cha = convertchar(St_in[i]);
            stOut +=(cha).ToString();
        }

        return stOut;
    }

    private char convertchar(char ch_in)
    {
        char c = default(char);

        switch (ch_in)
        {
            case 'a':
                c = '1';
                break;
            case '1':
                c = 'b';
                break;
            case 'b':
                c = '3';
                break;
            case '3':
                c = '8';
                break;
            case '8':
                c = 'd';
                break;
            case 'd':
                c = '9';
                break;
            case '9':
                c = '2';
                break;
            case '2':
                c = 'f';
                break;
            case 'f':
                c = 'w';
                break;
            case 'w':
                c = '0';
                break;
            case '0':
                c = 'a';
                break;
            default:
                c = ch_in;
                break;
        }
        return c;
    }

    private string Getdevicecode()
    {

        string devicekey = SystemInfo.deviceUniqueIdentifier;
        string _out = devicekey[9].ToString() + devicekey[10].ToString() + devicekey[11].ToString() + devicekey[12].ToString();
        return _out;
    }

    public void CheckRecciveCode()
    {
        if (inputreccivecode.text.Length != 6)
        {
            //Debug.LogError("the string is not correct");
            G_o_Erorr.GetComponent<Image>().sprite = WrongCdkey;
        }
        else
        {
            string str_reccive = "";
            string strin = inputreccivecode.text;
            for (int i = 0; i < strin.Length; i++)
            {
                char cha = convertchar(strin[i]);
                str_reccive += (cha).ToString();
            }

            /// removing salt
            str_reccive = str_reccive[0].ToString() + str_reccive[1].ToString() + str_reccive[2].ToString() + str_reccive[3].ToString();
            ////// get system code
            string codetemp = SystemInfo.deviceUniqueIdentifier;
            string systemcode = codetemp[9].ToString() + codetemp[10].ToString() + codetemp[11].ToString() + codetemp[12].ToString() ;
            if (systemcode.Equals(str_reccive)) // check system code with reccive code
            {
                AC_UIManager.Instance.Activegame();
            //    Debug.Log("successfully activated");
            }
            else
            {
             //   Debug.Log("the entered code is not correct");
                G_o_Erorr.GetComponent<Image>().sprite = WrongCdkey;
            }

        }
    }


    public void SetnullErorr()
    {
        G_o_Erorr.GetComponent<Image>().sprite = uiMask;
    }

}
