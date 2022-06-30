using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
using TapsellSDK;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] CanvasRenderer settingPanel;
    [SerializeField] CanvasRenderer quitAccepPanel;
    [SerializeField] CanvasRenderer resetAcceptPanel;
    [SerializeField] CanvasRenderer creditPanel;
 
    [SerializeField] GameObject G_o_Sound;
    [SerializeField] Sprite Sp_soundOff;
    [SerializeField] Sprite Sp_soundOn;

    const string TAPSELL_KEY = "tkkojelmohgebklippdqfserasssgirgbaqhbonmjchbotrmknfhebrhnadednmhldtbnl";




    [Tooltip("seconds before button function")] [SerializeField] float playButtonDelay;

    [SerializeField] LoadData loadData;

    private void Awake()
    {
        //musicSlider.value = PlayerPrefController.GetMasterMusic();
        //AudioListener.volume = PlayerPrefController.GetMasterSound();

        quitAccepPanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        Tapsell.Initialize(TAPSELL_KEY);

        creditPanel.gameObject.SetActive(false);
        //settingPanel.gameObject.SetActive(false);
        resetAcceptPanel.gameObject.SetActive(false);
        quitAccepPanel.gameObject.SetActive(false);
        SetSpriteVolume(PlayerPrefController.GetMasterMusic());
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



    //********************************BUTTONS******************************************//

    public void QuitGame()
    {
        quitAccepPanel.gameObject.SetActive(true);
    }

    public void Play()
    {
        StartCoroutine(PlayButton());
        
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");  // string ref for credit scene
    }

    public void Settings()
    {
        settingPanel.gameObject.SetActive(true);  
        Time.timeScale = 0.3f;
    }

    IEnumerator PlayButton()
    {
        yield return new WaitForSeconds(playButtonDelay);
        //SceneManager.LoadScene("Level Selecting");
        loadData.LoadLevelSelecing.allowSceneActivation = true;       
        loadData.LoadMainMenu = SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
        loadData.LoadMainMenu.allowSceneActivation = false;
        Resources.UnloadUnusedAssets();
    }

    //*******************************SETTING PANEL****************************************//


    public void OkInSetting()
    {
        settingPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
     

    //**************************************QUIT  ACCEPT PANEL*************************************//

    //YES button
    public void Yes()
    {       
        Application.Quit();
    }


    //No button
    public void No()
    {
        quitAccepPanel.gameObject.SetActive(false);
    }


    //***************************************RESET ACCEPT PANE***************************************//

    public void ResetGame()
    {
        resetAcceptPanel.gameObject.SetActive(true);
        settingPanel.gameObject.SetActive(false);
        
    }

    public void YesToReset()
    {
        ObscuredPrefs.DeleteAll();
        resetAcceptPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(true);
        ObscuredPrefs.SetBool("Active",true);
        PlayerPrefController.SetMasterMusic(true);
        SetSpriteVolume(PlayerPrefController.GetMasterMusic());
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void NoToReset()
    {
        resetAcceptPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(true);
    }

    //************************************** CREDIT PANEL******************************************//

    public void OpenCreditPanel()
    {
        creditPanel.gameObject.SetActive(true);
    }

    public void OkCreditPanel()
    {
        creditPanel.gameObject.SetActive(false);
    }
}
