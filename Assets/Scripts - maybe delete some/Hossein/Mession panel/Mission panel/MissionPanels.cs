using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.UI;



public class MissionPanels : MonoBehaviour {

    public static MissionPanels Instance;
    private string NameNextPanel;

    public NamesScenes ThisNameScene;
    public NamesScenes AutoDestinationScene;

    public GameObject ParentMissions;
    public GameObject AcceptPanel;
    public ML_DataMissionPanels Datalevels;

    // Use this for initialization
    void Awake() {


        if (!Instance)
        {
            Instance = this;
        }

        ObscuredPrefs.SetBool(ThisNameScene.ToString(),true);
    }

    void Start()
    {
        CheckFinishedLevels();
    }
    // Update is called once per frame
    void Update() {

    }

    public void ClickMissionPanel(string mission)        /// Click on evry mission 
    {
        if (ObscuredPrefs.GetBool(mission))   /// todo beta
          {
            NameNextPanel = mission;
            AcceptPanel.SetActive(true);
        }
        else
        {
            //print("the mission not Completed");
        }
    }

    public void ClickSurePanelNextMission()         /// click on panel sure 
    {
       StartCoroutine(_SceneManager.Instance.NextInMissionPanel(NameNextPanel));
    }

    public string getminigame()
    {
        string ret = "Colors";
        switch (ThisNameScene)
        {
            case NamesScenes.ColorFruits:
            case NamesScenes.ColorHajiFiruz:
            case NamesScenes.ColorJungle:
            case NamesScenes.ColorKharazmi:
            case NamesScenes.ColorStoreCloth:
                ret = "Colors";
                break;

            case NamesScenes.CyrusTemple:
            case NamesScenes.Damavand:
            case NamesScenes.KolahFarangi:
            case NamesScenes.Alighapoo:
                ret = "Numbers";
                break;

            case NamesScenes.SchoolYard:
            case NamesScenes.MiniRunner:
            case NamesScenes.BodyPart:
            case NamesScenes.Category:
            case NamesScenes.HiddenObject:
            case NamesScenes.Class:
                ret = "Dialogue";
                break;

        }

        return ret;
    }

    public void CheckFinishedLevels()
    {
     

        for (int i = 0; i < ParentMissions.transform.childCount; i++)
        {
            if (ObscuredPrefs.GetBool(Datalevels.Data[i].Scenename.ToString()))
            {
                ParentMissions.transform.GetChild(i).GetComponent<Image>().sprite = Datalevels.Data[i].AfterFinished;
            }
            else
                ParentMissions.transform.GetChild(i).GetComponent<Image>().sprite = Datalevels.Data[i].BeforeFinished;
        }
    }

    public void IslastminiGame()
    {
        switch (ThisNameScene)
        {
            case NamesScenes.ColorFruits:
                ObscuredPrefs.SetBool("Numbers",true);
                break;

            case NamesScenes.KolahFarangi:
                ObscuredPrefs.SetBool("Dialogue", true);
                ObscuredPrefs.SetBool("SavingPrivateBird", true);
                break;

            case NamesScenes.HiddenObject:
                ObscuredPrefs.SetBool("SavingPrivateBird", true);
                break;
        }

    }

}
