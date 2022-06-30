using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneType
{
    Color , Numbers , Dialogue
}

public enum NextScene
{
    ColorHajiFirus , Category , LevelSelecting , Nothing
}

public class LoadAndSave : MonoBehaviour
{

    [SerializeField] bool DeleteAllKeys = false;

    [SerializeField] SceneType sceneType;
    [SerializeField] NextScene nextSceneToLoad;
    [SerializeField] SO_UiElements uiElements;
    [SerializeField] GameObject[] missions;


    string[] colorSceneNames = { "ColorKharazmi", "ColorHajiFiruz" };
    string[] dialogueSceneNames = { "Class", "Category" };


    private void Start()
    {
        SetCurrentSceneSave();
        CheckForLoadedScenes(sceneType);
        SetLastScene();
        if (DeleteAllKeys)
        {
            ObscuredPrefs.DeleteAll();
        }
    }

    private void SetLastScene()
    {
        switch (sceneType)
        {
            case SceneType.Color:
                uiElements.lastColoringMission = SceneManager.GetActiveScene().name;
                break;
            case SceneType.Dialogue:
                uiElements.lastDialogueMission = SceneManager.GetActiveScene().name;
                break;
            default:
                break;
        }
    }

    public void SetCurrentSceneSave()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        ObscuredPrefs.SetBool("Current" + currentSceneName, true);
    }

    public void CheckForLoadedScenes(SceneType type)
    {
        switch (sceneType)
        {
            case SceneType.Color:
                for(int i=0 ; i < colorSceneNames.Length; i++)
                {
                    if(ObscuredPrefs.HasKey("Current" + colorSceneNames[i]))
                    {
                        missions[i].GetComponent<Image>().sprite = uiElements.ColorUiImages[i].activeSprite;
                        missions[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        missions[i].GetComponent<Image>().sprite = uiElements.ColorUiImages[i].inActiveSprite;
                        missions[i].GetComponent<Button>().interactable = false;
                    }
                }
                break;
            case SceneType.Dialogue:
                for (int i = 0; i < dialogueSceneNames.Length; i++)
                {
                    if (ObscuredPrefs.HasKey("Current" + dialogueSceneNames[i]))
                    {
                        missions[i].GetComponent<Image>().sprite = uiElements.DialogueUiImages[i].activeSprite;
                        missions[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        missions[i].GetComponent<Image>().sprite = uiElements.DialogueUiImages[i].inActiveSprite;
                        missions[i].GetComponent<Button>().interactable = false;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void LoadSpecificScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void LoadNextScene()
    {
        switch (nextSceneToLoad)
        {
            case NextScene.ColorHajiFirus:                
                SceneManager.LoadScene("ColorHajiFiruz");
                ObscuredPrefs.SetBool("ActiveNumbers", true);
                break;
            case NextScene.Category:
                SceneManager.LoadScene("Category");
                break;
            case NextScene.LevelSelecting:                
                SceneManager.LoadScene("Level Selecting");
                ObscuredPrefs.SetBool("ActiveDialogue", true);
                break;
            default:
                break;
        }
    }

    

 
}
