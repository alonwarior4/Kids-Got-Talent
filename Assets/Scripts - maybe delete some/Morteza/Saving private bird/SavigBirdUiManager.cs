using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.UI;



public class SavigBirdUiManager : MonoBehaviour
{
    [SerializeField] GameObject ReplayPanel;
    [SerializeField] GameObject newRecord;
    [SerializeField] GameObject HigthRecord;
    [SerializeField] GameObject Record;
    [SerializeField] GameObject PanelScoreTop;


    [SerializeField] float timeScale;



    [Header("Menu Panels")]
    [SerializeField] CanvasRenderer pausePanel;
    [SerializeField] CanvasRenderer exitPanel;
    [SerializeField] CanvasRenderer replayAcceptPanel;
  

    [Header("Sliders")]
    [SerializeField] Slider soundSlider;
    //[SerializeField] Slider musicSlider;


    [Header("big button for end pick function")]



    //cash refrences
    Animator storyBoxAnimator;
    Animator PickMissionAnimator;

    [HideInInspector] public bool isInUI = false;



    [SerializeField] GameObject G_o_Sound;
    [SerializeField] Sprite Sp_soundOff;
    [SerializeField] Sprite Sp_soundOn;


    private void Awake()
    {
        ReplayPanel.SetActive(false);
        PanelScoreTop.SetActive(true);
    }

    private void Start()
    {
        SetSpriteVolume(PlayerPrefController.GetMasterMusic());

        //AudioListener.volume = soundSlider.value;


        pausePanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(false);
        replayAcceptPanel.gameObject.SetActive(false);
    }

    public void DeadUi()
    {
        newRecord.SetActive(false);
        ReplayPanel.SetActive(true);
        PanelScoreTop.SetActive(false);
        Record.GetComponent<TMP_Text>().text = AddBird.Instance.GetNumber().ToString();
        if (ObscuredPrefs.HasKey("ScoreSaving"))
        {
            if (ObscuredPrefs.GetInt("ScoreSaving") >= AddBird.Instance.GetNumber())
            {
                HigthRecord.GetComponent<TMP_Text>().text ="highest record : " + ObscuredPrefs.GetInt("ScoreSaving").ToString();
            }
            else    
            {
                HigthRecord.GetComponent<TMP_Text>().text = "highest record : " + ObscuredPrefs.GetInt("ScoreSaving").ToString();
                newRecord.SetActive(true);
                ObscuredPrefs.DeleteKey("ScoreSaving");
                ObscuredPrefs.SetInt("ScoreSaving", AddBird.Instance.GetNumber());
            }
        }
        else
        {
            newRecord.SetActive(true);
            HigthRecord.GetComponent<TMP_Text>().text = "highest record : 0 " ;
            ObscuredPrefs.SetInt("ScoreSaving", AddBird.Instance.GetNumber());

        }
    }

    public void ReplayButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("Main Menu");
    }



    //***********************PAUSE PANEL************************//

    // PAUSE button function
    public void OpenPauseMenu()
    {
        isInUI = true;
        pausePanel.gameObject.SetActive(true);
        Time.timeScale = timeScale;
    }

    //CONTINUE button function
    public void ContinueGamePlay()
    {
        isInUI = false;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
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



    //**************************** SETTINGS**************************//

    ////ACCEPT button function
    //public void Accept()
    //{
    //    //PlayerPrefController.SetMasterMusic(musicSlider.value);
    //    PlayerPrefController.SetMasterSound(soundSlider.value);
    //    //AudioListener.volume = soundSlider.value;
    //    settings.gameObject.SetActive(false);
    //    pausePanel.gameObject.SetActive(true);
    //}

    ////IGNORE button function
    //public void Ignore()
    //{
    //    //musicSlider.value = PlayerPrefController.GetMasterMusic();
    //    soundSlider.value = PlayerPrefController.GetMasterSound();
    //    //AudioListener.volume = soundSlider.value;
    //    settings.gameObject.SetActive(false);
    //    pausePanel.gameObject.SetActive(true);
    //}

    //*******************************EXIT PANEL**************************//

    //YES button function
    public void Yes()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    //NO button function
    public void No()
    {
        exitPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }

    //*******************************RESTART PANEL*********************************//

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //******************************REPLAY ACCEPT PANEL****************************//

    public void S_OpenReplayPanel()
    {
        pausePanel.gameObject.SetActive(false);
        replayAcceptPanel.gameObject.SetActive(true);
    }

    public void S_Replay()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentBuildIndex);
    }

    public void S_NoReplay()
    {
        replayAcceptPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }

}



	

