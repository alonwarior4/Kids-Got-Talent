using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using TMPro;

public class N_UiManager : MonoBehaviour
{
    public GameObject[]  UiAnimalBirds = new GameObject[6];
    public GameObject[] UiAnimalSheeps = new GameObject[6];
    public Sprite[]  ImageBirds = new Sprite[6];
    public Sprite[] ImageSheeps = new Sprite[6];
    [SerializeField] Sprite uiMask;


    //public TMP_Text[] TxtNumberBirdsUi = new TMP_Text[2];
    //public TMP_Text[] TxtNumberSheepsUi = new TMP_Text[2];
    [Space]
    public TMP_Text TxtSumCountBirds;
    public TMP_Text TxtSumCountSheeps;


    // Start is called before the first frame update
    public static N_UiManager Instance; 
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    [System.Serializable]
    public class AnswerUI
    {
        public Image ImgQuest;
        public string Answers;
    }
    
    public void ShowInUIBirds(int NumberAnimal , int count)
    {
        for (int i = 0; i <UiAnimalBirds.Length; i++)
        {
            if (UiAnimalBirds[i].GetComponent<N_InfoUi>().StatusthisUi == "Null")
            {
                UiAnimalBirds[i].GetComponent<Image>().sprite = ImageBirds[NumberAnimal];
                UiAnimalBirds[i].GetComponent<N_InfoUi>().StatusthisUi = "busy";
                UiAnimalBirds[i].GetComponent<N_InfoUi>().AnimalName = Questions.instance.birdsPrefabs[NumberAnimal].GetComponent<Animals>().Name;
                //  TxtNumberBirdsUi[i].text = count + "";
                N_ResultInUi.instance.SetInUiBirds(count, ImageBirds[NumberAnimal], i);
                break;
            }
        }
    }

    public void ShowInUISheeps(int NumberAnimal , int count)
    {
        for (int i = 0; i < UiAnimalSheeps.Length; i++)
        {
            if (UiAnimalSheeps[i].GetComponent<N_InfoUi>().StatusthisUi == "Null")
            {
                UiAnimalSheeps[i].GetComponent<Image>().sprite = ImageSheeps[NumberAnimal];
                UiAnimalSheeps[i].GetComponent<N_InfoUi>().StatusthisUi = "busy";
                UiAnimalSheeps[i].GetComponent<N_InfoUi>().AnimalName = Questions.instance.ReptilePrefabs[NumberAnimal].GetComponent<Animals>().Name;
                N_ResultInUi.instance.SetInUiSheep(count,ImageSheeps[NumberAnimal]);

                break;
            }
        }
    }

    /// ///////////////////
    public void ShowSumCountBirds()
    {
        ///show in ui
        TxtSumCountBirds.text = Questions.instance.CounterBirds.ToString();
    }

    public void ShowSumCounterSheeps()
    {
        /// show in ui
        TxtSumCountSheeps.text = Questions.instance.CounterSheep.ToString();
    }

    public void SetShowUI()
    {
        TxtSumCountBirds.text = 0 + "";
        TxtSumCountSheeps.text = 0 + "";


        for (int i = 0; i < UiAnimalBirds.Length; i++)
        {
            UiAnimalBirds[i].GetComponent<N_InfoUi>().StatusthisUi = "Null";
            UiAnimalBirds[i].GetComponent<N_InfoUi>().AnimalName = "";

        }

        for (int i = 0; i < UiAnimalSheeps.Length; i++)
        {
            UiAnimalSheeps[i].GetComponent<N_InfoUi>().StatusthisUi = "Null";
            UiAnimalSheeps[i].GetComponent<N_InfoUi>().AnimalName = "";

        }

    }

    public void EmptyUi()
    {
        foreach (GameObject uiBird in UiAnimalBirds)
        {
            uiBird.GetComponent<Image>().sprite = uiMask;
        }

        foreach (GameObject uiSheep in UiAnimalSheeps)
        {
            uiSheep.GetComponent<Image>().sprite = uiMask;
        }
    }

    //internal void SetActiveCountTypeAnimals(bool b)
    //{
    //    for (int i = 0; i <TxtNumberBirdsUi.Length ; i++)
    //    {
    //       TxtNumberBirdsUi[i].gameObject.SetActive(b);
    //    }
    //}


    //TODO for test delete


    private void OnDestroy()
    {
        Instance = null;
    }

}
