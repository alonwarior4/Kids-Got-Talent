using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using TapsellSDK;

public class OtherUIManagement : MonoBehaviour
{
    [Header("Block Raycaster when stroy box")]
    [SerializeField] GameObject blocker;

    [Header("Next Scene")]
    [SerializeField] string nextSceneName;

    [Header("Click Blocker")]
    [SerializeField] GameObject clickBlocker;

    [Header("Scene Config")]
    [SerializeField] float DelayEnd;
    [SerializeField] float timeScale;


    //for select word colors
    string[] words;
    int wordIndex;


    [HideInInspector] public bool isEndOfTyping;
    public bool isHasToTellStory;


    [Header("storyboard")]
    [SerializeField] Button okButton;
    [SerializeField] Story_data story_Data;
    [SerializeField] TextMeshProUGUI storyText;
    public bool isEndOfScene;



    [Header("Menu Panels")]
    [SerializeField] CanvasRenderer storyBox;
    [SerializeField] CanvasRenderer pausePanel;
    [SerializeField] CanvasRenderer exitPanel;
    //[SerializeField] CanvasRenderer missionPanel;
    [SerializeField] CanvasRenderer replayPanel;



    //[Header("big button for end pick function")]
    //[SerializeField] Button endPickBigButton;



    //cash refrences
    Animator storyBoxAnimator;
    Animator PickMissionAnimator;
    bool isDownMissionPanel = false;


    [HideInInspector] public bool isInUI = false;

    private int index;


    [SerializeField] GameObject G_o_Sound;
    [SerializeField] Sprite Sp_soundOff;
    [SerializeField] Sprite Sp_soundOn;

    const string ZONE_ID = "5f71e0937b431900012ce40b";
    TapsellAd tapsellLoadedAd = null;


    private void Awake()
    {
        isEndOfTyping = false;
        blocker.SetActive(false);

        storyBoxAnimator = storyBox.gameObject.GetComponent<Animator>();
        //PickMissionAnimator = missionPanel.gameObject.GetComponent<Animator>();
    }



    private void Start()
    {
        StartCoroutine(RequestForAd());

        SetSpriteVolume(PlayerPrefController.GetMasterMusic());

        //AudioListener.volume = soundSlider.value;

        isEndOfScene = false;

        storyBox.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(false);
        //endPickBigButton.gameObject.SetActive(false);
        replayPanel.gameObject.SetActive(false);

        if (isHasToTellStory)
        {
            StartCoroutine(StartToTellStory());
        }
    }

    IEnumerator RequestForAd()
    {
        SendRequestForAd();
        yield return new WaitForSeconds(5.5f);
        if (tapsellLoadedAd == null)
        {
            SendRequestForAd();
        }
    }

    private void SendRequestForAd()
    {
        Tapsell.RequestAd(ZONE_ID, true,
            (TapsellAd resualtAd) => { tapsellLoadedAd = resualtAd; },
            (string noAdError) => { Debug.Log("no ad awailable"); },
            (TapsellError error) => { Debug.Log(error.message); },
            (string noNetError) => { Debug.Log("NO Network Awailable"); },
            (TapsellAd expireAd) => { tapsellLoadedAd = null; });
    }


    //***********************PAUSE PANEL************************//

    // PAUSE button function
    public void OpenPauseMenu()
    {
        isInUI = true;
        pausePanel.gameObject.SetActive(true);
        Time.timeScale = timeScale;
        if(clickBlocker)
        clickBlocker.SetActive(true);
    }

    //CONTINUE button function
    public void ContinueGamePlay()
    {
        isInUI = false;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        if(clickBlocker)
        clickBlocker.SetActive(false);
    }

    //HOME button function
    public void GoToMainMenu()
    {
        pausePanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(true);
    }

    // SETTING button function
    //public void OpenSettingMenu()
    //{
    //    settings.gameObject.SetActive(true);
    //    pausePanel.gameObject.SetActive(false);        
    //}

    //Replay Button Function
    


    private void SetSpriteVolume(bool state)
    {
        if (state)
        {
            G_o_Sound.GetComponent<Image>().sprite = Sp_soundOn;
            AudioListener.volume = 1;

        }
        else
        {
            G_o_Sound.GetComponent<Image>().sprite = Sp_soundOff;
            AudioListener.volume = 0;
        }
    }

    public void ClickChangeSpSound()       //// with  every click on  icon sound
    {
        if (G_o_Sound.GetComponent<Image>().sprite.Equals(Sp_soundOff))
        {
            G_o_Sound.GetComponent<Image>().sprite = Sp_soundOn;
            AudioListener.volume = 1;
            PlayerPrefController.SetMasterMusic(true);

        }
        else
        {
            G_o_Sound.GetComponent<Image>().sprite = Sp_soundOff;
            AudioListener.volume = 0;
            PlayerPrefController.SetMasterMusic(false);
        }
    }


    //*******************************EXIT PANEL**************************//

    //YES button function
    public void Yes()
    {
        //LoadingTargetMission.IsLoadingMission = false;
        LoadingTargetMission.loadingTarget = LoadingTarget.MainMenu;
        SceneManager.LoadScene(8);
        Time.timeScale = 1f;
    }

    //NO button function
    public void No()
    {
        exitPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }

    #region storybox
    //******************************STORY BOX*****************************//


    private void ShowTapsellAd()
    {
        if (tapsellLoadedAd == null) return;

        Tapsell.ShowAd(tapsellLoadedAd, new TapsellShowOptions());
    } 

    //ok button function
    public void Ok()
    {
        if (isEndOfScene)
        {
            LoadingTargetMission.loadingTarget = LoadingTarget.LevelSelecting;
            SceneManager.LoadScene(8);
            //SceneManager.LoadScene("Level Selecting");
        }
        else
        {
            index++;
            if(index < story_Data.StoryData.Length)
            {
                StartCoroutine(TellFirstStory());
            }           
        }
    }


    public void End()
    {
        StartCoroutine(EndOfStory());
    }

    IEnumerator EndOfStory()
    {
        DisableOkButton();
        yield return new WaitForSeconds(0.5f);
        storyBoxAnimator.SetTrigger("EndStory");
        yield return new WaitForSeconds(1.5f);
        storyBox.gameObject.SetActive(false);
        blocker.SetActive(false);
    }

    IEnumerator StartToTellStory()
    {
        
        DisableOkButton();
        storyBox.gameObject.SetActive(true);
        blocker.SetActive(true);
        storyBoxAnimator.SetTrigger("StartStory");
        yield return new WaitForSeconds(2f);
        StartCoroutine(TellFirstStory());
    }

    IEnumerator EndOfColoring()
    {
        DisableOkButton();
        yield return new WaitForSeconds(DelayEnd);

        ShowTapsellAd();

        storyBox.gameObject.SetActive(true);
        blocker.SetActive(true);
        storyBoxAnimator.SetTrigger("StartStory");
        storyText.text = "";
        StartCoroutine(TellLastStory());
    }


    //story manager

    public void DisableOkButton()
    {
        okButton.interactable = false;
    }


    public void EnableOkButton()
    {
        okButton.interactable = true;
    }


    IEnumerator TellFirstStory()
    {
        
        isEndOfTyping = false;
        DisableOkButton();
        if (story_Data.StoryData[index].isFirst)
        {
            if (story_Data.StoryData[index].Audio)
            {
                GetComponent<AudioSource>().PlayOneShot(story_Data.StoryData[index].Audio, 2f);
                float n_typeSpeed = story_Data.StoryData[index].Audio.length / story_Data.StoryData[index].text.Length;
                for (int i = 0; i <= story_Data.StoryData[index].text.Length; i++)
                {
                    storyText.text = story_Data.StoryData[index].text.Substring(0, i);
                    yield return new WaitForSeconds(n_typeSpeed);
                }
            }
            else
            {
                for (int i = 0; i <= story_Data.StoryData[index].text.Length; i++)
                {
                    storyText.text = story_Data.StoryData[index].text.Substring(0, i);
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(3f);
            }
            
        }
        else
        {
            StartCoroutine(EndOfStory());
            isEndOfTyping = true;
        }
        //yield return new WaitForSeconds(1f);
        EnableOkButton();
    }


    IEnumerator TellLastStory()
    {
        isEndOfTyping = false;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < story_Data.StoryData.Length; i++)
        {
            if (story_Data.StoryData[i].isLast)
            {
                if (story_Data.StoryData[i].Audio)
                {
                    GetComponent<AudioSource>().PlayOneShot(story_Data.StoryData[i].Audio, 2f);
                    float n_TypeSpeed = story_Data.StoryData[i].Audio.length / story_Data.StoryData[i].text.Length;
                    for (int index = 0; index <= story_Data.StoryData[i].text.Length; index++)
                    {
                        storyText.text = story_Data.StoryData[i].text.Substring(0, index);
                        yield return new WaitForSeconds(n_TypeSpeed);
                    }
                }
                else
                {
                    for (int index = 0; index <= story_Data.StoryData[i].text.Length; index++)
                    {
                        storyText.text = story_Data.StoryData[i].text.Substring(0, index);
                        yield return new WaitForSeconds(0.05f);
                    }
                    yield return new WaitForSeconds(1f);
                }              
            }
            else
            {
                continue;
            }
        }
        yield return new WaitForSeconds(1.5f);
        EnableOkButton();
    }

    public void StartStory()
    {
        StartCoroutine(StartToTellStory());
    }

    public void TellFinalStory()
    {
        StartCoroutine(EndOfColoring());
    }


    #endregion


    //*******************************RESTART PANEL*********************************//

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //*******************************REPLAY PANEL*********************************//

    public void ND_OpenReplayPanel()
    {
        pausePanel.gameObject.SetActive(false);
        replayPanel.gameObject.SetActive(true);
    }


    public void ND_Replay()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentBuildIndex);
    }

    public void ND_NoReplay()
    {
        replayPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }
}

