using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;

public class MainStoryBox : MonoBehaviour
{
    [SerializeField] CanvasRenderer storyBox;
    [SerializeField] AudioClip sound;
    [TextArea] [SerializeField] string firstTalkTxt;
    [SerializeField] Button okButton;


    private void Awake()
    {
        okButton.interactable = false;
    }


    private void Start()
    {
        if (!ObscuredPrefs.HasKey("MainStoryBox"))
        {
            StartCoroutine(_Start());
        }
        else
        {
            storyBox.gameObject.SetActive(false);
        }

    }
    IEnumerator _Start ()
    {
        Time.timeScale = 0;
        storyBox.gameObject.SetActive(true);
        storyBox.gameObject.GetComponent<Animator>().SetTrigger("StartStory");
        yield return new WaitForSecondsRealtime(1.75f);
        GetComponent<AudioSource>().PlayOneShot(sound);
        StartCoroutine(Type(firstTalkTxt));
        yield return null;

	}
	
    IEnumerator Type(string firstTalk)
    {
        for(int i=0; i <= firstTalk.Length; i++)
        {
            storyBox.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = firstTalk.Substring(0, i);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        yield return new WaitForSecondsRealtime(1f);
        okButton.interactable = true;
    }
    

    public void Ok()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        storyBox.gameObject.GetComponent<Animator>().SetTrigger("EndStory");
        yield return new WaitForSecondsRealtime(1.75f);
        Time.timeScale = 1;
        storyBox.gameObject.SetActive(false);
        ObscuredPrefs.SetBool("MainStoryBox",true);
    }
    

}
