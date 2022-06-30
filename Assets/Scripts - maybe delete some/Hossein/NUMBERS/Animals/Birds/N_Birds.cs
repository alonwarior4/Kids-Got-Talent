using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Birds : Animals
{
    [SerializeField] SpriteRenderer numberPos;
    private bool isGoing;
    [SerializeField]
    private float dis_Going;

    BirdsSoundPlayer birdsSoundPlayer;
    // Use this for initialization
    private void Awake()
    {
        DisableColliders();
        isfinish = false;
        isGoing = false;
        dis_Going = 1;
    }
    void Start ()
	{
		//speed = 2;
		//DelayDisable = 4;
		MyType = AnimalType.Bird;
		ThisAnim = this.transform.GetChild(0).GetComponent<Animator>();

		if (IsSceneSaving == false)
		{
			Come();
		}

        birdsSoundPlayer = this.GetComponentInChildren<BirdsSoundPlayer>();
	}

    // Update is called once per frame
    void Update()
    {
        if (isGoing)
        {
            SoundFader();
        }
        if (Vector3.Distance(this.transform.position, MyTarget) > Mathf.Epsilon)
        {
            Movement();
            //	print("call move ");
        }
        else
        {
            ThisAnim.SetTrigger("IsWireHitted");

            if (isfinish == false)
            {
                  EnableColliders();
                // fly on air
            }
        }
    }

    private void SoundFader()
    {
        float fadevolume = (dis_Going -Vector3.Distance(this.transform.position, MyTarget)) / dis_Going;
        birdsSoundPlayer.fade_v = 1 - fadevolume;    
    }

    private void CounterTruee()
    {

        ///// counter number of sheep
        Questions.instance.CounterBirds += 1;
        N_UiManager.Instance.ShowSumCountBirds();

    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();

      

        if (IsAnswer)
        {
            IsAnswer = false;
            EndQuestion();
            Questions.instance.Answers -= 1;
            CounterTruee();
            ShowNumberPlaySound();
            SetBeforeTarget = true;

            if (isfinish == false)
            {
               Questions.instance.CheckingEnd();
            }

            isfinish = true;
        }
        else
        {
            ThisAnim.SetTrigger("FunPlay");
        }


    }

    private void ShowNumberPlaySound()
    {
        numberPos.sprite = N_SoundManager.Instance.numbersData.numbers[Questions.instance.CounterBirds - 1].sprite;
        ThisAnim.SetTrigger("Counting");
        AudioClip Sound = N_SoundManager.Instance.numbersData.numberSounds[Questions.instance.CounterBirds - 1];
        AudioSource.PlayClipAtPoint(Sound, Camera.main.transform.position, 2);
    }

    public override void Come()
	{
		MyTarget = N_ManagmentTargetsBirds.Instance.GetTargetCome();
		CheckingDirection(MyTarget);
	}

	public override void Go()
	{
        isGoing = true;
		if (IsSceneSaving==false)
		{
			MyTarget = N_ManagmentTargetsBirds.Instance.GetTargetGo();
		}
		CheckingDirection(MyTarget);

        dis_Going = Vector3.Distance(this.transform.position, MyTarget); 
	}

    public override void EndQuestion()
    {
        if (SetBeforeTarget == false)
        {
            StartCoroutine(EndQuenstionCoroutine());
        }
    }

    IEnumerator EndQuenstionCoroutine()
    {
        DisableColliders();
        
        yield return new WaitForSeconds(0.5f);
        if (Name == "BlackParrot")
        {

        }
        else
        {
            Go();
        }            
        ThisAnim.ResetTrigger("IsWireHitted");
        ThisAnim.ResetTrigger("FunPlay");       
        ThisAnim.SetTrigger("Fly Up");
        Remove();
    }

    public override void CheckingDirection(Vector3 v)
    {
        if((this.transform.position.x - v.x ) <= Mathf.Epsilon)
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
