using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public enum TutorialType
{
    Coloring,
    Numbers
}
public class TuturialVideo : MonoBehaviour
{
    bool isPlayedOnce = false;

    [SerializeField] TutorialType thisType;

    private void Awake()
    {
        if (thisType == TutorialType.Coloring)
        {
            if (ObscuredPrefs.HasKey("FirstColoringTutorial"))
            {
                isPlayedOnce = ObscuredPrefs.GetBool("FirstColoringTutorial");
            }
            else
            {
                isPlayedOnce = false;
            }
        }
        else 
        {
            if (ObscuredPrefs.HasKey("FirstNumbersTutorial"))
            {
                isPlayedOnce = ObscuredPrefs.GetBool("FirstNumbersTutorial");
            }
            else
            {
                isPlayedOnce = false;
            }
        }

        if (isPlayedOnce)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    void Start ()
    {
        if (!isPlayedOnce)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
	}
	
    public void ClickUnderstandButton()
    {
        StartCoroutine(ClickCoroutine());
    }


    IEnumerator ClickCoroutine()
    {
        GetComponent<Animator>().SetTrigger("Go");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        gameObject.SetActive(false);
        if(thisType == TutorialType.Coloring)
        {
            ObscuredPrefs.SetBool("FirstColoringTutorial", true);
        }
        else
        {
            ObscuredPrefs.SetBool("FirstNumbersTutorial", true);
        }
       
    }

}
