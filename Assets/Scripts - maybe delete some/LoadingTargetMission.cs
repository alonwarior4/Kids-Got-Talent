using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LoadingTarget
{
   None, MainMenu , LevelSelecting , TargetMission 
}

public class LoadingTargetMission : MonoBehaviour
{
    [SerializeField] LoadData loadData;
    [SerializeField] Animator oliAnim;
    public static bool IsLoadingMission;

    public static LoadingTarget loadingTarget = LoadingTarget.None;

    private void Start()
    {
        switch (loadingTarget)
        {
            case LoadingTarget.None:
                break;
            case LoadingTarget.MainMenu:
                StartCoroutine(LoadMainMenuOperation());
                break;
            case LoadingTarget.LevelSelecting:
                StartCoroutine(LoadLevelSelectingOperation());
                break;
            case LoadingTarget.TargetMission:
                StartCoroutine(LoadMissionOperation());
                break;
            default:
                break;
        }
    }

    private IEnumerator LoadMissionOperation()
    {
        AsyncOperation LoadTargetMission = SceneManager.LoadSceneAsync(loadData.TargetMission , LoadSceneMode.Single);
        LoadTargetMission.allowSceneActivation = false;

        while (LoadTargetMission.progress < 0.88f)
        {
            yield return new WaitForSeconds(0.5f);

            float randomNum = Random.Range(0f, 1f);
            if (randomNum > 0.6f)
            {
                oliAnim.SetTrigger("Kharesh");
            }
            else
            {
                oliAnim.SetTrigger("HandShake");
            }

            yield return new WaitUntil(() => oliAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f);

            yield return new WaitForSeconds(0.5f);
        }

        LoadTargetMission.allowSceneActivation = true;
        Resources.UnloadUnusedAssets();
    }


    private IEnumerator LoadMainMenuOperation()
    {
        loadData.LoadMainMenu = SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
        loadData.LoadMainMenu.allowSceneActivation = false;
        loadData.LoadLevelSelecing = SceneManager.LoadSceneAsync("Level Selecting", LoadSceneMode.Single);        
        loadData.LoadLevelSelecing.allowSceneActivation = false;

       

        while (loadData.LoadMainMenu.progress < 0.88f)
        {
            yield return new WaitForSeconds(0.5f);

            float randomNum = Random.Range(0f, 1f);
            if (randomNum > 0.6f)
            {
                oliAnim.SetTrigger("Kharesh");
            }
            else
            {
                oliAnim.SetTrigger("HandShake");
            }

            yield return new WaitUntil(() => oliAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f);

            yield return new WaitForSeconds(0.5f);
        }

       

        loadData.LoadMainMenu.allowSceneActivation = true;
        Resources.UnloadUnusedAssets();
    }

    private IEnumerator LoadLevelSelectingOperation()
    {      
        loadData.LoadLevelSelecing = SceneManager.LoadSceneAsync("Level Selecting", LoadSceneMode.Single);
        loadData.LoadLevelSelecing.allowSceneActivation = false;

        loadData.LoadMainMenu = SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
        loadData.LoadMainMenu.allowSceneActivation = false;


        while (loadData.LoadLevelSelecing.progress < 0.88f)
        {
            yield return new WaitForSeconds(0.5f);

            float randomNum = Random.Range(0f, 1f);
            if (randomNum > 0.6f)
            {
                oliAnim.SetTrigger("Kharesh");
            }
            else
            {
                oliAnim.SetTrigger("HandShake");
            }

            yield return new WaitUntil(() => oliAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f);

            yield return new WaitForSeconds(0.5f);
        }


        loadData.LoadLevelSelecing.allowSceneActivation = true;
        Resources.UnloadUnusedAssets();
    }
}
