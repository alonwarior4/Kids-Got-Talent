using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.SceneManagement;

public class AC_UIManager : MonoBehaviour {

    public static AC_UIManager Instance;
    public GameObject OnlineOrSms;
    public GameObject SmsPanel;
    public GameObject OnlinePanel;
    public string NextScene;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        CheckActive();


    }
    // Use this for initialization
    void Start () {

    }


    // Update is called once per frame
    void Update () {

    }

    public void CheckActive()
    {
        if (DeviceIsActive())
        {
            //OnlineOrSms.SetActive(false);
            //SmsPanel.SetActive(false);
            //OnlinePanel.SetActive(false);
            SceneManager.LoadScene(NextScene);

        }
    }

    public void Activegame()
    {
        //OnlineOrSms.SetActive(false);
        //SmsPanel.SetActive(false);
        //OnlinePanel.SetActive(false);

        ObscuredPrefs.SetBool("Active" , true);
        //// next scene
        SceneManager.LoadScene(NextScene);
    }

    private bool DeviceIsActive()   /// code gozari 
    {
        bool active = false;
        if (ObscuredPrefs.GetBool("Active") == true)
        {
            active = true;
        }
       
        return active;
    }


}
