using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;


public class Ru_CharcterController : MonoBehaviour
{
    [SerializeField] CanvasRenderer restartPanel;


    [SerializeField] OtherUIManagement uiManager;
    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

    private Rigidbody2D Rigid;
    public float Speed;
    public bool Isgrounded;
    public Animator Anim;
    public bool f_start;
    public Story_data story;

    bool fallingDown;
    bool isDrazkesh;
    bool isJumping;

    public GameObject tuto_up;
    public GameObject tuto_down;

    private bool once_down;
    private bool once_up;

    private void Awake()
    {
        tuto_down.SetActive(false);
        tuto_up.SetActive(false);

        if (restartPanel)
        {
            restartPanel.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        Rigid = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();

        f_start = false;

        fallingDown = false;

        StartCoroutine(Start_Typing());
    }

    IEnumerator Tutorial()
    {
        //////// down
        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(1);
        tuto_down.SetActive(true);
        //tuto_Down.GetComponent<Animator>().SetTrigger("Showing");

        yield return new WaitUntil(() => once_down);

        Time.timeScale = 1;

        tuto_down.SetActive(false);
        once_down = false;

        /////////// up
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = .25f;

        tuto_up.SetActive(true);
        // tuto_up.GetComponent<Animator>().SetTrigger("Showing");
        yield return new WaitUntil(() => once_up);
        tuto_up.SetActive(false);

        Time.timeScale = 1;

    }

    void Update()
    {       
        if (f_start)
        {
            if (fallingDown) { return; }
            GetInputButtons();
        }
    }


    private void GetInputButtons()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isDrazkesh || isJumping) { return; }
            Jump();
            once_up = true;
        }
        else if (Input.GetButtonDown("Sit"))
        {
            if(isJumping || isDrazkesh) { return; }
            Sit();
            once_down = true;
        }


        if (Input.GetButtonUp("Sit"))
        {
            Anim.SetTrigger("Run");
        }
    }

    public void Jump()
    {      
        if (Isgrounded)
        {
            isJumping = true;
            Rigid.AddForce(new Vector2(0, 10.5f), ForceMode2D.Impulse);
            Anim.SetTrigger("jump");
            
        }
    }

    public void Sit()
    {
        if (Isgrounded)
        {
            isDrazkesh = true;
            Anim.SetTrigger("sit");
        }
    }

    private IEnumerator Starting()
    {
        for (int i = 0; i < 7; i++)
        {
            //yield return new WaitForSeconds(1);
            //Ru_BgSpawner.instance.Starting();
            yield return new WaitForSeconds(Random.Range(1.75f, 2.5f));
            //Ru_SpawnerTrees.instance.Starting();
            //yield return new WaitForSeconds(Random.Range(.3f, 2.5f));
            Ru_SpwanerTrapsObject.instance.Starting();
            //Ru_SpawnerTrees.instance.Starting();
            yield return new WaitForSeconds(2f);
            Ru_SpwanerTrapsObject.instance.Starting();
            yield return new WaitForSeconds(2f);
            Ru_SpwanerTrapsObject.instance.Starting();
            //Ru_SpawnerTrees.instance.Starting();
        }

        yield return new WaitForSeconds(5);
        //Ru_BgSpawner.instance.SpawnKharazmi();
        Minirunner_BG.instance.IsArrived = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Isgrounded = true;
        }

        else if (other.tag == "Enemy")
        {
            fallingDown = true;
            GameOver();
        }
        else if (other.tag == "Zirgozar")
        {
            fallingDown = true;
            FallBack();
        }

        else if (other.transform.name == "Kharazmi")
        {
            Finish();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Isgrounded = false;
        }
    }

    public void GameOver()
    {
        if (isJumping)
        {
            Rigid.AddForce(new Vector2(-4, 0), ForceMode2D.Impulse);
            Anim.SetTrigger("FallDown");
            Minirunner_BG.instance.HasToRun = false;
            StartCoroutine(ActiveRestartPanel());
        }
        else
        {
            Rigid.AddForce(new Vector2(-6, 5), ForceMode2D.Impulse);
            Anim.SetTrigger("FallDown");
            Minirunner_BG.instance.HasToRun = false;
            StartCoroutine(ActiveRestartPanel());
        }
    }

    public void FallBack()
    {

        if (isJumping)
        {
            Rigid.AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
            Anim.SetTrigger("FallBack");
            Minirunner_BG.instance.HasToRun = false;
            StartCoroutine(ActiveRestartPanel());
        }
        else
        {
            Rigid.AddForce(new Vector2(2.5f, 1), ForceMode2D.Impulse);
            Anim.SetTrigger("FallBack");
            Minirunner_BG.instance.HasToRun = false;
            StartCoroutine(ActiveRestartPanel());
        }
    }

    private void Finish()
    {
        uiManager.TellFinalStory();
        uiManager.isEndOfScene = true;
        Anim.SetTrigger("Idle");
        Minirunner_BG.instance.HasToRun = false;
    }

    IEnumerator Start_Typing()
    {
        while(uiManager.isEndOfTyping == false)
        {
            yield return waitFrame;
        }
        yield return new WaitForSeconds(1.5f);
        f_start = true;
        once_up = false;
        once_down = false;
        if (ObscuredPrefs.HasKey("FirstMiniRunner"))
        {
            //nothing
        }
        else
        {
            ObscuredPrefs.SetBool("FirstMiniRunner", true);
            StartCoroutine(Tutorial());
            Minirunner_BG.instance.HasToRun = true;
            Anim.SetTrigger("Run");
            yield return new WaitForSeconds(5f);
        }

        Minirunner_BG.instance.HasToRun = true;
        Anim.SetTrigger("Run");
        StartCoroutine(Starting());
    }


    public void FalseJumping()
    {
        isJumping = false;
    }

    public void FalseDrazkesh()
    {
        isDrazkesh = false;
    }

    IEnumerator  ActiveRestartPanel()
    {
        yield return new WaitForSeconds(2.5f);
        restartPanel.gameObject.SetActive(true);
    }

    public void LoadSceneAgain()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

   
}