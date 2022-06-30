using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddBird : MonoBehaviour
{
    public static AddBird Instance;

    int BirdNumber = 0;
    int Score = 0;

    TextMeshProUGUI scoreTxt;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        scoreTxt = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        scoreTxt.text = Score.ToString();
        Score = (int)( (Mathf.Pow(S_ScriptManager.Instance.BaseSpeed, 2) * Time.timeSinceLevelLoad)/4 ) +BirdNumber*100;
    }

    private void Start()
    {
        
    }

    public void AddNumber()
    {
        BirdNumber++;
        //Score = Score + 50;

    }

    public int GetNumber()
    {
        return Score;
    }


}
