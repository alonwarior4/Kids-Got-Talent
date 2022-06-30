using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLoading : MonoBehaviour
{

    [Tooltip("deloy to select animation trigger")][SerializeField] float animationTriggerDelay;
    string[] cloudTriggers = { "Left", "Right", "Up", "Down" };
    int choosedTriggerIndex = 3;
    Animator cloudAnimator;


    private void Awake()
    {
        cloudAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(SelectCloudTrigger());
    }

    private void SelectTrigger()
    {
        int randomTriggerIndex = UnityEngine.Random.Range(0, cloudTriggers.Length);
        while(randomTriggerIndex == choosedTriggerIndex)
        {
            randomTriggerIndex = UnityEngine.Random.Range(0, cloudTriggers.Length);
        }
        choosedTriggerIndex = randomTriggerIndex;
    }

    IEnumerator SelectCloudTrigger()
    {       
        while (true)
        {                   
            SelectTrigger();
            cloudAnimator.SetTrigger(cloudTriggers[choosedTriggerIndex]);
            yield return new WaitForSeconds(animationTriggerDelay);                       
        }      
    }
}
