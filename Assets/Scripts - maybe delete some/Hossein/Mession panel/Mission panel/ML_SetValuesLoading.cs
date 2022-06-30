using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.SceneManagement;
public class ML_SetValuesLoading : MonoBehaviour
{
	private string nextS;
    public NamesScenes firstdialogue;
    public NamesScenes firstcolors;
    public NamesScenes firstnumber;

    private void Awake()
	{
		StartCoroutine( GetData());
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {

    }

	IEnumerator GetData()
    {
        ObscuredPrefs.SetBool(firstcolors.ToString(), true);
        ObscuredPrefs.SetBool(firstdialogue.ToString(), true);
        ObscuredPrefs.SetBool(firstnumber.ToString(), true);


        if (ObscuredPrefs.HasKey("ClickMini"))
        {
            string ClickMinigame = ObscuredPrefs.GetString("ClickMini");        ///  lastdialogue , lastnumber  ,, lastcolors

            if (ObscuredPrefs.HasKey(ClickMinigame))
            {
                string LastLevelSaved = ObscuredPrefs.GetString(ClickMinigame);
                nextS = LastLevelSaved;
            }
            else
            {
                switch (ObscuredPrefs.GetString("ClickMini"))
                {
                    case "LastDialogue":
                        
                        nextS = firstdialogue.ToString();
                        break;

                    case "LastColors":
                        nextS = firstcolors.ToString();
                        break;
                    case "LastNumbers":
                        nextS = firstnumber.ToString();
                        break;

                }
            }
        }
        else
        {
           // string Source = ObscuredPrefs.GetString("SourceScene");/// now not used
            string Des = ObscuredPrefs.GetString("DestinationScene");
            nextS = Des;
        }
        //////////////////////////
        SetLoadingParameter(nextS);
        ///////////////////////////
        while (true)
        {
            if (LoadingManager.instance.LoadingIsFinished)
            {
                ObscuredPrefs.DeleteKey("SourceScene");
                ObscuredPrefs.DeleteKey("DestinationScene");
                ObscuredPrefs.DeleteKey("ClickMini");
                SceneManager.LoadScene(nextS);

                break;

            }
            yield return new WaitForEndOfFrame();
        }

    }

    private static void SetLoadingParameter(string next)
    {
        try
        {
        NamesScenes t = (NamesScenes)Enum.Parse(typeof(NamesScenes), next);
        switch (t)
            {
                case NamesScenes.Alighapoo:

                    LoadingManager.instance.loadingState = LoadingState.NumbersLoading;
                    LoadingManager.instance.MissionNumber = 3;

                    break;
                case NamesScenes.CyrusTemple:

                    LoadingManager.instance.loadingState = LoadingState.NumbersLoading;
                    LoadingManager.instance.MissionNumber = 2;

                    break;
                case NamesScenes.Damavand:

                    LoadingManager.instance.loadingState = LoadingState.NumbersLoading;
                    LoadingManager.instance.MissionNumber = 1;


                    break;
                case NamesScenes.KolahFarangi:

                    LoadingManager.instance.loadingState = LoadingState.NumbersLoading;
                    LoadingManager.instance.MissionNumber = 4;
                    break;

                case NamesScenes.SchoolYard:
                case NamesScenes.MiniRunner:
                case NamesScenes.Class:
                case NamesScenes.Category:
                case NamesScenes.BodyPart:
                case NamesScenes.HiddenObject:
                case NamesScenes.SavingPrivateBird:

                    LoadingManager.instance.loadingState = LoadingState.DilaoguesLoading;
                    break;

                case NamesScenes.ColorFruits:
                case NamesScenes.ColorHajiFiruz:
                case NamesScenes.ColorJungle:
                case NamesScenes.ColorKharazmi:
                case NamesScenes.ColorStoreCloth:

                    LoadingManager.instance.loadingState = LoadingState.ColorsLoading;
                    break;

            }
        }
        catch (Exception)
        {
            print("not found namescene");
        }

    }

    private string EnumtoString(NamesScenes n)
    {

        return n.ToString();
    }


}

