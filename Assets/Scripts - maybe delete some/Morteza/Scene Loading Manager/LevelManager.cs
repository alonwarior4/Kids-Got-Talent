using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;

public class LevelManager : MonoBehaviour
{
    public SO_UiElements uiElements;

    public GameObject numbersLevel;
    public GameObject dialogueLevel;

    private void Start()
    {
        CheckNumbersLevel();
        CheckDialogueLevel();
    }

    public void LoadLastColoringMission()
    {
        SceneManager.LoadScene(uiElements.lastColoringMission);
    }

    public void LoadLastDialogueMission()
    {
        SceneManager.LoadScene(uiElements.lastDialogueMission);
    }

    public void LoadLastNumbersMission()
    {
        SceneManager.LoadScene("CyrusTemple");
    }

    private void CheckNumbersLevel()
    {
        if (ObscuredPrefs.HasKey("ActiveNumbers"))
        {
            numbersLevel.transform.GetChild(0).gameObject.SetActive(true);
            numbersLevel.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void CheckDialogueLevel()
    {
        if (ObscuredPrefs.HasKey("ActiveDialogue"))
        {
            dialogueLevel.transform.GetChild(0).gameObject.SetActive(true);
            dialogueLevel.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
     
}
