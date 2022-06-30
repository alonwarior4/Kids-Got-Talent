using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


public class Ca_UiManager : MonoBehaviour
{
    public static Ca_UiManager Instance;


    public GameObject UiQuestion;
    public GameObject[] UiAnswer = new GameObject[4];

    public AudioClip SoundWrong;
    public AudioClip SoundCorrect;


    public Sprite Empty;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

   private void Start()
    {

    }


    public void SetAnswer(Objects otherObj, Ca_TypesObjectsEnum type)
    {

        for (int i = 0; i < UiAnswer.Length; i++)
        {
            if (UiAnswer[i].GetComponent<Ca_InfoUi>().StateOption != "Busy")
            {
                Ca_InfoUi answerInfo = UiAnswer[i].GetComponent<Ca_InfoUi>();

                answerInfo.StateOption = "Busy";
                UiAnswer[i].GetComponent<SpriteRenderer>().sprite = otherObj.Sp;
                answerInfo.Type = type;
                answerInfo.thisObjSound = otherObj.nameSound;


                break;
            }
        }

    }


    public void SetQuestion(Sprite sp)
    {
        SpriteRenderer Q_sprite = UiQuestion.GetComponent<SpriteRenderer>();
        Q_sprite.sprite = sp;
        float xSize = Q_sprite.bounds.size.x * 3.33f;
        float ySize = Q_sprite.bounds.size.y * 3.33f;
        UiQuestion.GetComponent<BoxCollider2D>().size = new Vector2(xSize, ySize);
    }


    public void SetInit()
    {
        for (int i = 0; i < UiAnswer.Length; i++)
        {
            UiAnswer[i].GetComponent<Ca_InfoUi>().StateOption = "";
            UiAnswer[i].GetComponent<SpriteRenderer>().sprite = Empty;
            UiAnswer[i].GetComponent<SpriteRenderer>().color = Color.white;
            UiAnswer[i].GetComponent<Ca_InfoUi>().Type = Ca_TypesObjectsEnum.None;
        }

        UiQuestion.GetComponent<SpriteRenderer>().sprite = Empty;
    }


    public void SetTrueAnswer(Objects trueObj  , Ca_TypesObjectsEnum type)
    {
        int r = UnityEngine.Random.Range(0, 4);
        Ca_InfoUi trueInfo = UiAnswer[r].GetComponent<Ca_InfoUi>();
        
        UiAnswer[r].GetComponent<SpriteRenderer>().sprite = trueObj.Sp;
        trueInfo.Type = type;
        trueInfo.StateOption = "Busy";
        trueInfo.thisObjSound = trueObj.nameSound;
    }


    public void ResetAllTriggers()
    {
        foreach(GameObject sprite in UiAnswer)
        {
            Animator spriteAnimator = sprite.GetComponent<Animator>();
            spriteAnimator.ResetTrigger("Correct");
            spriteAnimator.ResetTrigger("Wrong");
            spriteAnimator.ResetTrigger("Come");
            spriteAnimator.ResetTrigger("Go");
        }
    }


   
}
