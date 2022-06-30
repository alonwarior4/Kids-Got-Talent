using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class N_ResultInUi : MonoBehaviour {

    public static N_ResultInUi instance;
    public GameObject countingPanelAnimalBase;

    public TMP_Text countBirdsResult ;
    public Image[] imgBirdsResult;

    [Space]
    public TMP_Text countSheepsResult;
    public Image imgSheep;


    public Animator Anim_PanelBirds;
    public Animator Anim_PanelSheeps;
    private int totalbirds;

    public void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {

        totalbirds = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SetInUiBirds(int CountBirds, Sprite SpBirds , int index)
    {
        totalbirds += CountBirds;
        countBirdsResult.text = totalbirds.ToString();
        imgBirdsResult[index].sprite = SpBirds;
    }

    public void SetInUiSheep(int CountSheep, Sprite SpSheep)
    {
        countSheepsResult.text = CountSheep.ToString();
        imgSheep.sprite = SpSheep;
    }

    public void ShowResult()
    {
        countingPanelAnimalBase.SetActive(false);
    }

    public void ShowOffResultBirds()
    {
        totalbirds = 0;
        Anim_PanelBirds.gameObject.SetActive(false);
        countingPanelAnimalBase.SetActive(true);
    }
    public void ShowOffResultSheep()
    {
        Anim_PanelSheeps.gameObject.SetActive(false);
        countingPanelAnimalBase.SetActive(true);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
