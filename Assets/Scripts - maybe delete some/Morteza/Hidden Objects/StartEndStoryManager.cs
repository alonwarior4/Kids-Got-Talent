using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndStoryManager : MonoBehaviour
{
    public static StartEndStoryManager instance;

    [SerializeField] GameObject objectParents;
    [SerializeField] OtherUIManagement uiManager;
    Collider2D[] objectColliders;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        objectColliders = objectParents.GetComponentsInChildren<Collider2D>();       
    }

    IEnumerator Start()
    {
        DisableAllColliders();
        yield return new WaitUntil(() => uiManager.isEndOfTyping);
        EnableAllColliders();
    }



    public void DisableAllColliders()
    {
        for (int i = 0; i < objectColliders.Length; i++)
        {
            objectColliders[i].enabled = false;
        }
    }

    public void EnableAllColliders()
    {
        for (int i = 0; i < objectColliders.Length; i++)
        {
            objectColliders[i].enabled = true;
        }
    }

    public void EndOfHiddenObj()
    {
        uiManager.TellFinalStory();
        uiManager.isEndOfScene = true;
    }



}
