using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class MenuSound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        if (!ObscuredPrefs.HasKey("FirstEnter"))
        {
            PlayerPrefController.SetMasterMusic(true);
            AudioListener.volume = 1;
            ObscuredPrefs.SetBool("FirstEnter", true);
        }
        else
        {
            if (PlayerPrefController.GetMasterMusic())
            {
                AudioListener.volume = 1;
            }
            else
                AudioListener.volume = 0;


            //AudioListener.volume = PlayerPrefController.GetMasterSound();
        }

    }
}