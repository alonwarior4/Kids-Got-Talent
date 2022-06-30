using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour 
{
    [SerializeField] LoadData loadData;

    public void SetTargetScene(string sceneName)
    {
        loadData.TargetMission = sceneName;
        //LoadingTargetMission.IsLoadingMission = true;
        LoadingTargetMission.loadingTarget = LoadingTarget.TargetMission;
        SceneManager.LoadScene(8);
    }

    public void GoToMainMenu()
    {
        loadData.LoadMainMenu.allowSceneActivation = true;        
        loadData.LoadLevelSelecing = SceneManager.LoadSceneAsync("Level Selecting", LoadSceneMode.Single);
        loadData.LoadLevelSelecing.allowSceneActivation = false;
        Resources.UnloadUnusedAssets();
    }

  

}
