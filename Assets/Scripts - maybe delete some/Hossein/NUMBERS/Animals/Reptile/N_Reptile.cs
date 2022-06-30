using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Reptile : Animals
{
    [SerializeField] SpriteRenderer numberPos;

	// Use this for initialization
	private bool OnceIdle;
    int numberDefaultLayer;
    public AudioClip S_sheep;
    private void Awake()
    {
        DisableColliders();
    }

    void Start()
	{
        numberDefaultLayer = numberPos.sortingOrder;
		MyType = AnimalType.Reptile;
		ThisAnim = this.GetComponent<Animator>();
		Come();
		OnceIdle = true;
		
		StartCoroutine(PlaySoundSheep());
	}

	// Update is called once per frame
	void Update()
	{
        if (Vector2.Distance(this.transform.position, MyTarget) >= 0.25f)
        {
            Movement();
            SetLayerAndZ();
        }
        else
        {
            if (OnceIdle)
            {
                PlayIdleAnimation();
                OnceIdle = false;
            }
            if (isfinish==false)
            {
                EnableColliders();
            }
        }
    }
    public virtual void Movement()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, MyTarget, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x , transform.position.y , transform.position.y);
    }
    public IEnumerator PlaySoundSheep()
    {
        int r = UnityEngine.Random.Range(0, 10);
        if (r<=3)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 8));
            AudioSource.PlayClipAtPoint(S_sheep, Camera.main.transform.position, .2f);
        }
    }

    private void PlayIdleAnimation()
	{
		int r =UnityEngine.Random.Range(1, 4);
		switch (r)
		{
			case 1:
				ThisAnim.SetTrigger("Breathe");
				break;
			case 2:
				ThisAnim.SetTrigger("Eat");
				break;
			case 3:
				ThisAnim.SetTrigger("Sleep");
				break;
				
		}
	}

	public override void OnMouseDown()
	{
		base.OnMouseDown();

        if (IsAnswer)
		{

            IsAnswer = false;
            CounterTruee();
			EndQuestion();
			Questions.instance.Answers -= 1;
            PlaySoundShowNumber();
            SetBeforeTarget = true;

            if (isfinish == false)
            {
                Questions.instance.CheckingEnd();
            }

            isfinish = true;

        }
    }

    private void PlaySoundShowNumber()
    {
        numberPos.sprite = N_SoundManager.Instance.numbersData.numbers[Questions.instance.CounterSheep - 1].sprite;
        ThisAnim.SetTrigger("Counting");
        AudioClip Sound = N_SoundManager.Instance.numbersData.sheepSounds[Questions.instance.CounterSheep - 1];
        AudioSource.PlayClipAtPoint(Sound, Camera.main.transform.position, 2);
    }

    private void CounterTruee()
    {

        ///// counter number of sheep
        Questions.instance.CounterSheep += 1;
        N_UiManager.Instance.ShowSumCounterSheeps();

    }

    public override void Come()
	{
		MyTarget = N_managmentTargetsReptile.Instance.forCome[N_managmentTargetsReptile.Instance.GetTargetCome()].Vec3.position;
		CheckingDirection(MyTarget);

	}
	public override void Go()
	{
		MyTarget = N_managmentTargetsReptile.Instance.GetTargetGo();
		CheckingDirection(MyTarget);
	}

    public override void EndQuestion()
    {
        if (SetBeforeTarget == false)
        {
            StartCoroutine(EndQuestionCoroutine());
        }
    }

    IEnumerator EndQuestionCoroutine()
    {
        DisableColliders();
        yield return new WaitForSeconds(0.5f);
        Go();     
        ThisAnim.SetTrigger("Walk");
        Remove();
    }


    public void SetLayerAndZ()
    {      
        float z = this.transform.position.y;
        int layer;
        z = Mathf.Abs(z);
        layer = Mathf.RoundToInt(z) ;
        z = Mathf.Round(z);
        Vector3 v = new Vector3(this.transform.position.x, this.transform.position.y, -z);
	    transform.position = Vector3.Lerp(this.transform.position , v , 10 * Time.deltaTime);
        numberPos.sortingOrder = numberDefaultLayer + layer;
    }

    public override void CheckingDirection(Vector3 v)
    {
        if (this.transform.position.x - v.x <= Mathf.Epsilon)
        {
            this.transform.GetComponent<SpriteRenderer>().flipX = true;
            //this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().flipX = false;
            //this.transform.eulerAngles = new Vector3(0, 0, 0);

        }
    }

}
