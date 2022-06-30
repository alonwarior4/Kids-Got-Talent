using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using TapsellSDK;

public enum SceneName
    {
     Kharazmi,
     Jungle,
     StoreCloth,
     Fruits,
     Other
    }


public class UIManagement : MonoBehaviour
{
    [Header("UI Blocker")]
    [SerializeField] GameObject blocker;


    [Header("Click Blocker")]
    [SerializeField] GameObject clickBlock;

    [Header("Objects Config")]
    [SerializeField] GameObject objectsParent;
    Collider2D[] objectsColliders;


    //for select word colors
    string[] words;
    int wordIndex;

    //coloring scene names
    [SerializeField] SceneName sceneName;

    //objects animator
    [SerializeField] Animator objectsAnimator;


    [Header("storyboard")]
    [SerializeField] Button okButton;
    [SerializeField] Story_data story_Data;
    [SerializeField] TextMeshProUGUI storyText;


    [HideInInspector] public bool isEndOfColoring;    // to not show in inspector



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
        storyBoxAnimator = storyBox.gameObject.GetComponent<Animator>();
        blocker.SetActive(false);
        //PickMissionAnimator = missionPanel.gameObject.GetComponent<Animator>();
    }

   

    private void Start()
    {
        StartCoroutine(RequestForAd());

        SetSpriteVolume(PlayerPrefController.GetMasterMusic());

        isEndOfColoring = false;

        storyBox.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(false);
        //endPickBigButton.gameObject.SetActive(false);
        replayPanel.gameObject.SetActive(false);

        StartCoroutine(StartToTellStory());
    }

    IEnumerator RequestForAd()
    {
        SendRequestForAd();
        yield return new WaitForSeconds(5.5f);
        if(tapsellLoadedAd == null)
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
        if(clickBlock)
        clickBlock.SetActive(true);
    }

    //CONTINUE button function
    public void ContinueGamePlay()
    {
        isInUI = false;
        pausePanel.gameObject.SetActive(false);
        if(clickBlock)
        clickBlock.SetActive(false);
    }

    

    //HOME button function
    public void GoToMainMenu()
    {
        pausePanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(true);
    }


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


    //******************************STORY BOX*****************************//


    private void ShowTapsellAd()
    {
        if (tapsellLoadedAd == null) return;

        Tapsell.ShowAd(tapsellLoadedAd, new TapsellShowOptions());
    }


    //ok button function
    public void Ok()
    {
        if (isEndOfColoring)
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
        EnableAllColliders();
        if (objectsAnimator)
        {
            objectsAnimator.speed = 1;
        }        
    }


    IEnumerator StartToTellStory()
    {
        DisableOkButton();
        DisableAllColliders();
        storyBox.gameObject.SetActive(true);
        blocker.SetActive(true);
        storyBoxAnimator.SetTrigger("StartStory");
        if (objectsAnimator) { objectsAnimator.speed = 0; }
        yield return new WaitForSeconds(2f);
        StartCoroutine(TellFirstStory());
    }


    IEnumerator EndOfColoring()
    {
        DisableOkButton();
        DisableAllColliders();
        yield return new WaitForSeconds(5f);

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

    private void DisableAllColliders()
    {
        objectsColliders = objectsParent.GetComponentsInChildren<Collider2D>();
        for(int i =0; i< objectsColliders.Length ; i++)
        {
            objectsColliders[i].enabled = false;
        }
    }

    private void EnableAllColliders()
    {
        objectsColliders = objectsParent.GetComponentsInChildren<Collider2D>();
        for(int i=0; i< objectsColliders.Length; i++)
        {
            objectsColliders[i].enabled = true;
        }
    }

    IEnumerator TellFirstStory()
    {
        DisableOkButton();
        if (story_Data.StoryData[index].isFirst)
        {
            AudioClip storyClip = story_Data.StoryData[index].Audio;
            GetComponent<AudioSource>().PlayOneShot(storyClip , 1f);
            float typeSpeed = storyClip.length / story_Data.StoryData[index].text.Length;

            for (int i = 0; i <= story_Data.StoryData[index].text.Length; i++)
            {
                storyText.text = story_Data.StoryData[index].text.Substring(0, i);
                yield return new WaitForSeconds(typeSpeed);
            }
            //yield return new WaitForSeconds(1f);
        }
        else
        {
            StartCoroutine(EndOfStory());
        }
        //yield return new WaitForSeconds(1f);
        EnableOkButton();
    }


    IEnumerator TellLastStory()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < story_Data.StoryData.Length; i++)
        {
            if (story_Data.StoryData[i].isLast)
            {
                AudioClip L_storyClip = story_Data.StoryData[i].Audio;
                GetComponent<AudioSource>().PlayOneShot(L_storyClip, 2f);
                float L_typeSpeed = L_storyClip.length / story_Data.StoryData[i].text.Length;

                for (int index = 0; index <= story_Data.StoryData[i].text.Length; index++)
                {
                    storyText.text = story_Data.StoryData[i].text.Substring(0, index);
                    yield return new WaitForSeconds(L_typeSpeed);
                }
                yield return new WaitForSeconds(1f);
            }
            else
            {
                continue;
            }
        }
        EnableOkButton();
    }

    public void TellFinalStory()
    {
        StartCoroutine(EndOfColoring());
    }

    //choose color for oliver dick head

    public string ConstructOliverText(string oliverText)
    {
        string coloredText = "";
        words = oliverText.Split(' ');
        for(wordIndex = 0 ; wordIndex < words.Length; wordIndex++)
        {
            coloredText += "<color=#" + SelectedScene(words[wordIndex])+ ">"  + words[wordIndex] + " ";
        }
        return coloredText;
    }

    private string SelectedScene(string wordText)
    {
        switch (sceneName)
        {
            case SceneName.Kharazmi:
                return Kharazmi_SelectColor(wordText);
            case SceneName.Jungle:
                return Jungle_SelectColor(wordText);
            case SceneName.StoreCloth:
                return Cloth_SelectColor(wordText);
            case SceneName.Fruits:
                return Fruits_SelectColor(wordText);
            case SceneName.Other:
                return null;
            default:
                return null;
        }
    }

    #region SelectColor
    private string Kharazmi_SelectColor(string S)
    {
        switch (S)
        {
            case "green":
                return "237834";
            case "blue":
                return "009BBB";
            case "orange":
                return "DE8900";
            case "brown":
                return "553323";
            case "gray":
                return "898989";
            case "pink":
                return "CE2389";
            case "red":
                return "CE0034";
            case "yellow":
                return "EFCC00";
            default:
                return "F3F3F3";
        }
    }

    private string Jungle_SelectColor(string S)
    {
        switch (S)
        {
            case "green":
                return "445510";
            case "black":
                return "343334";
            case "orange":
                return "DE6700";
            case "brown":
                return "794510";
            case "gray":
                return "898989";
            case "pink":
                return "CE55AA";
            case "red":
                return "BB1200";
            case "yellow":
                return "CEAA00";
            case "purple":
                return "89009A";
            default:
                return "F3F3F3";
        }
    }

    private string Cloth_SelectColor(string S)
    {
        switch (S)
        {
            case "black":
                return "343334";
            case "brown":
                return "895521";
            case "green":
                return "21CC00";
            case "light":
                return "00BBDE";
            case "dark":
                return "004465";
            case "blue":
                if (words[wordIndex - 1] == "light")
                {
                    return "00BBDE";
                }
                else
                {
                    return "004465";
                }
                    ;
            case "red":
                return "BB1100";
            case "pink":
                return "FF2389";
            case "yellow":
                return "DEBB00";
            case "purple":
                return "8955AA";
            default:
                return "F3F3F3";
        }
    }

    private string Fruits_SelectColor(string S)
    {
        switch (S)
        {
            case "brown":
                return "553323";
            case "orange":
                if(words[wordIndex - 1 ] == "orange")
                {
                    return "F3F3F3";
                }
                else
                {
                    return "DE6700";
                }               
            case "gray":
                return "898989";
            case "light":
                return "BB1200";
            case "dark":
                return "790000";
            case "red":
                if(words[wordIndex - 1] != null)
                {
                    if (words[wordIndex - 1] == "dark")
                    {
                        return "790000";
                    }
                    else
                    {
                        return "BB1200";
                    }
                }
                else
                {
                    return "BB1200";
                }
                    ;
            case "purple":
                return "6512FF";
            case "green":
                return "105500";
            case "black":
                return "343334";
            case "yellow":
                return "DEAA00";
            default:
                return "F3F3F3";
        }
    }
    #endregion


    //*****************************MISSION PANEL***********************************//

    //public void MissionPanel()
    //{
    //    isDownMissionPanel = !isDownMissionPanel;
    //    endPickBigButton.gameObject.SetActive(isDownMissionPanel);
    //    PickMissionAnimator.SetBool("PanelState", isDownMissionPanel);
    //    isInUI = isDownMissionPanel;
    //}

    //*******************************REPLAY PANEL***********************************//

    //REPLAY button function
    public void OpenReplayPanel()
    {
        pausePanel.gameObject.SetActive(false);
        replayPanel.gameObject.SetActive(true);
    }

    
    public void Replay()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentBuildIndex);
    }

    public void NoReplay()
    {
        replayPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }
}
