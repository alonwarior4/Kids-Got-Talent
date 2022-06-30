using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;
using VIDE_Data;


public class _SceneManager : MonoBehaviour
{

    public static _SceneManager Instance;

    // Use this for initialization
	void Start () {

        if (!Instance)
        {
            Instance = this;
        }
        ObscuredPrefs.SetString("Last" + MissionPanels.Instance.getminigame(), MissionPanels.Instance.ThisNameScene.ToString());
    }

  
    public void NextScene(String next)			
    {
        if (MissionPanels.Instance.getminigame() == "Dialogue")
        {
            VD.EndDialogue();
        }
        ObscuredPrefs.SetString("SourceScene" , MissionPanels.Instance.ThisNameScene.ToString());
	    ObscuredPrefs.SetString("DestinationScene" , next);
        SceneManager.LoadScene("Mission Loading");
    }

    public IEnumerator NextAutoScene()							/// call faunction in last object of level
    {
        Paper.instance.EndOfLevel();
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => Paper.instance.PaperAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f);
        MissionPanels.Instance.IslastminiGame();
        ObscuredPrefs.SetBool(MissionPanels.Instance.AutoDestinationScene.ToString(), true);
        if (MissionPanels.Instance.ThisNameScene != NamesScenes.HiddenObject)
        {
            NextScene(MissionPanels.Instance.AutoDestinationScene.ToString());
        }
        else
            SceneManager.LoadScene(NamesScenes.SavingPrivateBird.ToString());

    }

    public IEnumerator NextInMissionPanel(String nextScene)							/// call faunction in last object of level
    {
        Paper.instance.EndOfLevel();
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => Paper.instance.PaperAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f);
        NextScene(nextScene.ToString());

    }
}
