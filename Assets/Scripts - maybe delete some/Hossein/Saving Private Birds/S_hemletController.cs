using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using System;

public class S_hemletController : MonoBehaviour
{


    [SerializeField] float rotationDegree;
    [SerializeField] float speedUpRotation;
    [SerializeField] float speedDownRotation;
    [SerializeField] AudioClip death_AC;

    float distance;


       
    public float speed;
    [HideInInspector] public Animator anim;
    public States This_StatelFlyng;
    public StateInfo[] Stateinfo = new StateInfo[4];
    public Vector3 This_TargetState;

    bool isAlive;

    //////////////////////
    private Transform player;
    private Rigidbody2D rigid;
    ////////////
    public GameObject tuto_up;
    public GameObject tuto_Down;

    private bool OneClickUp;
    private bool OneClickDown;


    void Start()
    {
        isAlive = true;
        tuto_Down.SetActive(false);
        tuto_up.SetActive(false);

        OneClickDown = false;
        OneClickUp = false;

        anim = transform.GetChild(0).GetComponent<Animator>();
        anim.SetTrigger("Fly");
        anim.SetFloat("Speed",1);
        
        rigid = GetComponent<Rigidbody2D>();
        player = this.GetComponent<Transform>();
        This_StatelFlyng = States.middle;


        if (ObscuredPrefs.HasKey("FirstSaving"))
        {
            //nothing
        }
        else
        {
            ObscuredPrefs.SetBool("FirstSaving" , true);
            StartCoroutine(Tutorial());
        }

       /// ObscuredPrefs.DeleteKey("FirstSaving");  ///for test // debug mode
    }

    IEnumerator Tutorial()
    {
        //////// down
        Time.timeScale = .25f;
        yield return new WaitForSecondsRealtime(1);
        tuto_Down.SetActive(true);
        //tuto_Down.GetComponent<Animator>().SetTrigger("Showing");

        yield return new WaitUntil(() => OneClickDown);

        Time.timeScale = 1;

        tuto_Down.SetActive(false);
        OneClickUp = false;
        
        /////////// up
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = .25f;

        tuto_up.SetActive(true);
       // tuto_up.GetComponent<Animator>().SetTrigger("Showing");
        yield return new WaitUntil(() => OneClickUp);
        tuto_up.SetActive(false);

        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                GotoUp();
                OneClickUp = true;
            }
            else
            {
                GotoDown();
                OneClickDown = true;
            }          
        }
        
        Rotate();
        Transition();       
    }

    private void GotoDown()
    {
        switch (This_StatelFlyng)
        {
            case States.Down :
                This_StatelFlyng = States.Down;
                break;
            case States.middle :
                This_StatelFlyng = States.Down;
                GotoAnotherState();
                break;
            case States.Up :
                This_StatelFlyng = States.middle;
                GotoAnotherState();
                break;                            
        }      
    }

    private void GotoUp()
    {     
        switch (This_StatelFlyng)
        {
            case States.Down :
                This_StatelFlyng = States.middle;
                GotoAnotherState();
                break;
            case States.middle :
                This_StatelFlyng = States.Up;
                GotoAnotherState();
                break;
            case States.Up :
                This_StatelFlyng = States.Up;
                break;              
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Death")
        {
            if (isAlive)
            {
                S_ScriptManager.Instance.Stop_();
                FindObjectOfType<SavigBirdUiManager>().DeadUi();
                TypeDeath();
                isAlive = false;
                S_ScriptManager.Instance.ISdeath = true;
                S_ScriptManager.Instance.BaseSpeed = 0;
                AudioSource.PlayClipAtPoint(death_AC, Camera.main.transform.position, 1f);
            }
        }
    }

    private void TypeDeath()
    {
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rigid = player.GetComponent<Rigidbody2D>();
        int ran = UnityEngine.Random.Range(1, 4);
        switch (ran)
        {
            case 1:
                anim.SetTrigger("Death1");
                rigid.AddForce(new Vector2(2,-3) * 65);
                break;
            case 2:
                anim.SetTrigger("Death2");
                rigid.AddForce(new Vector2(2, -3) * 65);
                break;
            case 3:
                anim.SetTrigger("Death3");
                rigid.AddForce(new Vector2(-2, -3) * 50);
                break;
        }
    }

    public enum States
    {
        Up , middle , Down
    }
    
    [System.Serializable]
    public class StateInfo
    {
        public Vector3 vecTarget;
        public States state;
    }

    public void GotoAnotherState()
    {
        for (int i = 0; i <Stateinfo.Length ; i++)
        {
            if (Stateinfo[i].state == This_StatelFlyng )
            {
                This_TargetState = Stateinfo[i].vecTarget;
                break;
            }
        }
    }


    public void Transition()
    {      
        transform.position = Vector3.MoveTowards(this.transform.position, This_TargetState, speed * Time.deltaTime);
    }


    public void Increase_velocity(float f)
    {
        anim.SetFloat("Speed",f);    
    }

    private void Rotate()
    {
        float Offset = Input.GetAxis("MyAxis");

        if (CheckDistance())
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * Offset * rotationDegree);
        }
        else
        {
            if (transform.rotation.z > 0)
            {
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, speedUpRotation * Time.deltaTime);
            }
            else if(transform.rotation.z < 0)
            {
                float angle = Mathf.LerpAngle(transform.eulerAngles.z - 359, 0, speedDownRotation * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0, angle);             
            }
        }
        
    }

    private bool CheckDistance()
    {
        distance = Vector3.Distance(transform.position, This_TargetState);
        return distance >= Mathf.Epsilon;
    }



}

