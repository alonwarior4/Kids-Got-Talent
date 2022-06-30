using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class PlayerPrefController : MonoBehaviour
{
    const string MASTER_SOUND = "master sound";
    const string MASTER_MUSIC = "master music";

    const float MIN_MUSIC = 0F;
    const float MAX_MUSIC = 10F;

    const float MIN_SOUND = 0F;
    const float MAX_SOUND = 10F;

	

    public static void SetMasterMusic(bool statevolume)
    {
        ObscuredPrefs.SetBool(MASTER_MUSIC, statevolume);
    }


    public static bool GetMasterMusic()
    {
        return ObscuredPrefs.GetBool(MASTER_MUSIC);
    }
   
}
