using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LoadingState
{
    ColorsLoading,
    NumbersLoading,
    DilaoguesLoading
}
public class LoadingManager : MonoBehaviour
{
    public LoadingState loadingState ;
    public bool LoadingIsFinished;
    public int MissionNumber;
    public static LoadingManager instance;
    // loadings
    [SerializeField] GameObject ColoringLoading;
    [SerializeField] GameObject NumberLoading;
    [SerializeField] GameObject DialogueLoading;

    

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        ColoringLoading.SetActive(false);
        NumberLoading.SetActive(false);
        DialogueLoading.SetActive(false);

    }

    private void Start()
    {
        switch (loadingState)
        {
            case LoadingState.ColorsLoading:
                ColoringLoading.SetActive(true);
                NumberLoading.SetActive(false);
                DialogueLoading.SetActive(false);
                break;
            case LoadingState.NumbersLoading:
                ColoringLoading.SetActive(false);
                NumberLoading.SetActive(true);
                DialogueLoading.SetActive(false);
                break;
            case LoadingState.DilaoguesLoading:
                ColoringLoading.SetActive(false);
                NumberLoading.SetActive(false);
                DialogueLoading.SetActive(true);
                break;
        }
    }
    
   
}
