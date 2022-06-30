using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System;

public class AC_ActiveOnline : MonoBehaviour {

    public string Url;
    public TMP_InputField inputText;
    public GameObject G_o_Erorr;

    public Sprite WrongCdkey;
    public Sprite CdkeyAnother;

    public Sprite uiMask;

    void OnEnable()
    {
        SetnullErorr();
    }

    private void SetnullErorr()
    {
        G_o_Erorr.GetComponent<Image>().sprite = uiMask;
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ClickToSend()
    {
        if (inputText.text.Length >= 6 && CheckCdCode())
        {

            StartCoroutine(SendCode());
        }
        else
        {
          ///  Debug.LogError("is not real cd!!!");
            G_o_Erorr.GetComponent<Image>().sprite = WrongCdkey;
        }

    }

    private bool CheckCdCode()
    {
        int c5 = (int)inputText.text[0];
        int c6 = (int)inputText.text[inputText.text.Length - 1];

        int n = c5 - c6;

        if (Mathf.Abs(n) == 13)
        {
            return true;
        }
        else
            return false;
    }


    private  IEnumerator SendCode()
    {

        WWWForm form = new WWWForm();
        string cdkey = inputText.text[1].ToString()+ inputText.text[2].ToString() + inputText.text[3].ToString() + inputText.text[4].ToString() ;
        print(cdkey);
        form.AddField("cdkey",cdkey);
        form.AddField("devicekey", SystemInfo.deviceUniqueIdentifier);
        form.AddField("devicename", SystemInfo.deviceName);

        /////////////////////////////////////////////////////

        UnityWebRequest www = UnityWebRequest.Post(Url, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Debug.Log(www.error);
            G_o_Erorr.GetComponent<Image>().sprite = WrongCdkey;
        }
        else
        {
            yield return www;
            print(www.downloadHandler.text);
            if (www.downloadHandler.text.Equals("ok"))
            {
               // Debug.Log("Form upload complete!");
                AC_UIManager.Instance.Activegame();
            }
            else
            {
                inputText.text = www.downloadHandler.text.ToString();
            }
        }
    }
}
