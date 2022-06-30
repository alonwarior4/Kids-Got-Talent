using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L_LoadnextSecne : MonoBehaviour {

    public string next;
    public float delay;
    [SerializeField] LoadData loadData;

    void Start ()
    {
        StartCoroutine(NextLevel());
	}

    IEnumerator NextLevel()
    {
        loadData.LoadMainMenu = SceneManager.LoadSceneAsync(next, LoadSceneMode.Single);
        loadData.LoadLevelSelecing = SceneManager.LoadSceneAsync("Level Selecting", LoadSceneMode.Single);
        loadData.LoadLevelSelecing.allowSceneActivation = false;
        loadData.LoadMainMenu.allowSceneActivation = false;
        yield return new WaitForSeconds(delay);        
        loadData.LoadMainMenu.allowSceneActivation = true;
        Resources.UnloadUnusedAssets();
    }

}
