using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Ca_Questions: MonoBehaviour
{
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject SnailParentprogress;
    private bool isFinishedCategoryCounting;

    private bool TeacherTellWrong;

    public static Ca_Questions Instance;
    public OtherUIManagement uiManager;
    public GameObject Teacher;
    public int CountQuestion;

    public List<Ca_TypesObjectsEnum> List = new List<Ca_TypesObjectsEnum>();


    [Header("Just for showing")] [SerializeField] private Ca_TypesObjectsEnum Question;


    public Ca_DataObjects [] Ca_Objects = new Ca_DataObjects[3];

    //delay between animations
    private WaitForSeconds waitSome = new WaitForSeconds(0.2f);



    
    
   void Awake()
    {
        if (!Instance)
        {
            Instance = this;

        }

        isFinishedCategoryCounting = false;
        TeacherTellWrong = false;

        CountQuestion = 0;
    }

    IEnumerator Start()
    {
        SnailParentprogress.SetActive(false);
        yield return new WaitUntil(() => MovingTeacher.instance.IsReccive);
        uiManager.StartStory();
        yield return new WaitForSeconds(1.75f);
        Teacher.GetComponent<Animator>().SetInteger("State", 7);
        StartCoroutine(waitAnimTeacher());
        yield return new WaitUntil(() => uiManager.isEndOfTyping);
        yield return new WaitForSeconds(1.75f);
        SnailParentprogress.SetActive(true);
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Zoom");
        Teacher.GetComponent<Animator>().SetInteger("State", -1);
        yield return new WaitForSeconds(1f);
        StartQuesion();
    }

    IEnumerator waitAnimTeacher()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(()=>Teacher.GetComponent<Animator>().GetCurrentAnimatorStateInfo(1).normalizedTime >.98f);
        Teacher.GetComponent<Animator>().SetInteger("State", -1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuesion()
    {
        List.RemoveRange(0, List.Count);
        SetInitObjects();
        SetList();
        Ca_UiManager.Instance.SetInit();
        SetQuestion();
        SetTrueAnswer();
        SetOtherAnswers();
        StartCoroutine(ComeAll());
        //Question = GetRandomType(true);
        //Sprite sp = GetRandomSpObject(Question).Sp;
        //Ca_UiManager.Instance.SetQuestion(sp);        /// set answer

        //////////////////////////////set true option
        //  Sprite spt = GetRandomSpObject(Question).Sp;
        //Ca_UiManager.Instance.SetTrueAnswer(spt ,Question );
        //////////////////////////
        //  for (int i = 0; i <=2 ; i++)
        //{
        //    Ca_TypesObjectsEnum TempType = GetRandomType(false);
        //    Objects TempObj = GetRandomSpObject(TempType);
        //    Ca_UiManager.Instance.SetAnswer(TempObj , TempType);        /// set gozineha
        //}
        //  ///////////////////////////////////
    }

    private void SetInitObjects()
    {
        for (int i = 0; i <Ca_Objects.Length; i++)
        {
            for (int j = 0; j <Ca_Objects[i].Objects.Length; j++)
            {
                Ca_Objects[i].Objects[j].Choosed = false;
            }
        }
    }

    private void SetList()
    {
        for (int i = 0; i < Ca_Objects.Length; i++)
        {        
            List.Add(Ca_Objects[i].ObjectsType);
        }
    }
    
    private Objects GetRandomSpObject(Ca_TypesObjectsEnum WhichType )
    {
        int index = 0;
        int RanObj=0;
        for (int i = 0; i <Ca_Objects.Length; i++)
        {
            if (Ca_Objects[i].ObjectsType == WhichType)
            {
                index = i;
                RanObj = Random.Range(0, Ca_Objects[index].Objects.Length);
                while (Ca_Objects[index].Objects[RanObj].Choosed)
                {
                    RanObj = Random.Range(0, Ca_Objects[index].Objects.Length);                    
                }
                Ca_Objects[index].Objects[RanObj].Choosed = true;
            }
        }

        return Ca_Objects[index].Objects[RanObj];
    }
    
    private Ca_TypesObjectsEnum GetRandomType(bool Quest)
    {
        Ca_TypesObjectsEnum str;
        int Ran = Random.Range(0, List.Count);
        str = List[Ran];
        List.Remove(str);
        
        return str;
    }

    public void ClickOptions(int options)
    {
        if (Ca_UiManager.Instance.UiAnswer[options].GetComponent<Ca_InfoUi>().Type == Question)
        {
            Ca_UiManager.Instance.UiAnswer[options].GetComponent<Collider2D>().enabled = false;
            StartCoroutine(AnswerIsTrue(options));
        }
        else
        {
            float randomF = UnityEngine.Random.Range(0f, 1f); 
            if(TeacherTellWrong == false)
            {
                if(randomF <= 0.45f)
                {
                    StartCoroutine(WrongAnswer());
                }               
            }
            GetComponent<AudioSource>().PlayOneShot(Ca_UiManager.Instance.SoundWrong);
            Ca_UiManager.Instance.UiAnswer[options].GetComponent<Animator>().SetTrigger("Wrong");
            Ca_UiManager.Instance.UiAnswer[options].GetComponent<BoxCollider2D>().enabled = false;
        }
    }


    IEnumerator AnswerIsTrue(int index)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(Ca_UiManager.Instance.SoundCorrect);
        
        Ca_UiManager.Instance.ResetAllTriggers();

        foreach (GameObject answer in Ca_UiManager.Instance.UiAnswer)
        {
            if(answer.GetComponent<Ca_InfoUi>().Type == Question)
            {
                //answer.GetComponent<BoxCollider2D>().enabled = false;
                answer.GetComponent<Animator>().SetTrigger("Correct");
                yield return waitSome;
            }
            else
            {
                //answer.GetComponent<BoxCollider2D>().enabled = false;
                answer.GetComponent<Animator>().SetTrigger("Go");
                yield return waitSome;
            }           
        }

        yield return new WaitForSeconds(1.5f);
        CountQuestion += 1;
        Endcategory();

        if (isFinishedCategoryCounting == false)
        {
            StartQuesion();
        }
      
    }

    IEnumerator ComeAll()
    {
        for(int i = 0; i< Ca_UiManager.Instance.UiAnswer.Length; i++)
        {
            GameObject answerObj = Ca_UiManager.Instance.UiAnswer[i];
            answerObj.GetComponent<Animator>().SetTrigger("Come");
            float xSize = answerObj.GetComponent<SpriteRenderer>().bounds.size.x * 6.67f;
            float ySize = answerObj.GetComponent<SpriteRenderer>().bounds.size.y * 6.67f;
            answerObj.GetComponent<BoxCollider2D>().size = new Vector2(xSize, ySize);
            yield return waitSome;
        }
    }


    private void SetQuestion()
    {
        Question = GetRandomType(true);
        Objects questionObj = GetRandomSpObject(Question);
        Sprite questionSp = questionObj.Sp;
        Ca_UiManager.Instance.UiQuestion.GetComponent<Ca_InfoUi>().thisObjSound = questionObj.nameSound;
        Ca_UiManager.Instance.SetQuestion(questionSp);       
    }
    
    private void SetTrueAnswer()
    {
        Objects AnswerObj = GetRandomSpObject(Question);
  //      Sprite AnswerSp = AnswerObj.Sp;        
        Ca_UiManager.Instance.SetTrueAnswer(AnswerObj, Question);
    }

    private void SetOtherAnswers()
    {
        for (int i = 0; i <= 2; i++)
        {
            Ca_TypesObjectsEnum TempType = GetRandomType(false);
            Objects TempObj = GetRandomSpObject(TempType);
            Ca_UiManager.Instance.SetAnswer(TempObj, TempType);        /// set gozineha
        }
    }


    public void Endcategory()
    {

        if (CountQuestion >= 10)
        {
            SnailWalking.instance.ChangeTarget();
            isFinishedCategoryCounting = true;
            StartCoroutine(EndOfCategory());
        }
        else if (CountQuestion > 0)
        {
            SnailWalking.instance.ChangeTarget();
        }

    }

    IEnumerator EndOfCategory()
    {
        yield return new WaitForSeconds(1.5f);
        uiManager.TellFinalStory();
        uiManager.isEndOfScene = true;
        yield return new WaitForSeconds(1.75f);
        Teacher.GetComponent<Animator>().SetInteger("State", 10);
        yield return new WaitForEndOfFrame();
        Teacher.GetComponent<Animator>().SetInteger("State", -1);      
    }

    IEnumerator WrongAnswer()
    {
        TeacherTellWrong = true;
        int randomState = UnityEngine.Random.Range(11, 15);
        yield return new WaitForSeconds(1f);
        Teacher.GetComponent<Animator>().SetInteger("State", randomState);
        yield return new WaitForEndOfFrame();
        Teacher.GetComponent<Animator>().SetInteger("State", -1);
        yield return new WaitForSeconds(2f);
        TeacherTellWrong = false;
    }
 
}
