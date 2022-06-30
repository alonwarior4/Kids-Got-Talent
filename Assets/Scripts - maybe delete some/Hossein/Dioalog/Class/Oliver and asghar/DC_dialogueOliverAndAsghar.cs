using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VIDE_Data;
using UnityEngine.SceneManagement;
using TapsellSDK;

public class DC_dialogueOliverAndAsghar : MonoBehaviour {

    private int CommentIndex;
    public static DC_dialogueOliverAndAsghar Instance;
    public float Delay;
    public GameObject Oliver;
    public GameObject Asghar;
    public GameObject teacher;
    public Animator _camera;
    

    //////////////////
    private Animator Anim_Oliver;
    private Animator Anim_Asghar;
    private Animator Anim_teacher;
    
    private bool F_StartDialogue;
    private VD.NodeData actor;
    private bool EndedEverydialogue;
    private bool isclicked;

    #region  3 bool ke be har kodam az se soal pasokh dad true mishan va check mishan
    private string q1;
    private string q2;
    private string q3;
    #endregion

    public GameObject PanelDlgOliverAndTeacher;
    public GameObject PanelDlgOliverAndAsghar;

    const string ZONE_ID = "5f71e0937b431900012ce40b";
    TapsellAd tapsellLoadedAd = null;


    //for loading
    public AsyncOperation LoadCategory;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(RequestForAd());

        q1 = "";
        q2 = "";
        q3 = "";
        
        
        Anim_Asghar = Asghar.GetComponent<Animator>();
        Anim_Oliver = Oliver.GetComponent<Animator>();
        Anim_teacher = teacher.GetComponent<Animator>();

        EndedEverydialogue = true;
        isclicked = false;
        F_StartDialogue = false;
        DC_OAA_Uimanager.instance.DisableUIOliver();
        DC_OAA_Uimanager.instance.DisableUIAsghar();
        PanelDlgOliverAndAsghar.SetActive(true);
        PanelDlgOliverAndTeacher.SetActive(false);

        LoadCategory = SceneManager.LoadSceneAsync("Category", LoadSceneMode.Single);
        LoadCategory.allowSceneActivation = false;
    }

    IEnumerator RequestForAd()
    {
        SendRequestForAd();
        yield return new WaitForSeconds(5.5f);
        if (tapsellLoadedAd == null)
        {
            SendRequestForAd();
        }
    }

    private void SendRequestForAd()
    {
        Tapsell.RequestAd(ZONE_ID, true,
            (TapsellAd resualtAd) => { tapsellLoadedAd = resualtAd; },
            (string noAdError) => { Debug.Log("no ad awailable"); },
            (TapsellError error) => { Debug.Log(error.message); },
            (string noNetError) => { Debug.Log("NO Network Awailable"); },
            (TapsellAd expireAd) => { tapsellLoadedAd = null; });
    }

    // Update is called once per frame
    void Update()
    {

        if (F_StartDialogue)
        {
            //   print("Start conv");
            Conversation();
        }
    }

    public void StartDialogue()
    {
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
        // Anim_.SetTrigger("SpeakPos");
        F_StartDialogue = true;

         // starting dialogue between teacher and oliver
    }

    private void ShowLoadedAd()
    {
        if (tapsellLoadedAd == null) return;

        Tapsell.ShowAd(tapsellLoadedAd, new TapsellShowOptions());
    }

    public void Conversation()
    {

        if (VD.isActive)
        {
            
            actor = VD.nodeData;
            if (actor.isEnd)
            {
                ShowLoadedAd();

                F_StartDialogue = false;
                VD.EndDialogue();
                //StartCoroutine(_SceneManager.Instance.NextAutoScene());
                //FindObjectOfType<LoadAndSave>().LoadNextScene();

                //TODO : Load Categry To ram
                LoadCategory.allowSceneActivation = true;
                Resources.UnloadUnusedAssets();
            }
            else
            {
                if (actor.isPlayer)
                {
                    if (EndedEverydialogue)
                    {
                        DC_OAA_Uimanager.instance.enableUIOliver(actor.comments.Length);
                       
                        for (int i = 0; i < actor.comments.Length; i++)
                        {
                            DC_OAA_Uimanager.instance.Btn_oliver[i].transform.GetChild(0).GetComponent<TMP_Text>().text = actor.comments[i];
                        }

                        if (actor.tag == "Special")
                        {
                            if (q1.Equals("true"))
                            {
                                DC_OAA_Uimanager.instance.Btn_oliver[0].gameObject.SetActive(false);
                            }
                            if (q2.Equals("true"))
                            {
                                DC_OAA_Uimanager.instance.Btn_oliver[1].gameObject.SetActive(false);
                            }
                            if (q3.Equals("true"))
                            {
                                DC_OAA_Uimanager.instance.Btn_oliver[2].gameObject.SetActive(false);
                            }
                            
                        }

                        if (isclicked)
                        {
                            StartCoroutine(waitAnim(Anim_Oliver));
                            actor.commentIndex = CommentIndex;
                            Anim_Oliver.SetInteger("State", int.Parse(actor.extraData[CommentIndex]));
                        }
                    }
                }
                else
                {
                    if (EndedEverydialogue)
                    {
                        DC_OAA_Uimanager.instance.enableUIAsghar();
                        DC_OAA_Uimanager.instance.DisableUIOliver();

                        if (actor.tag == "q1")
                        {
                            q1 = "true";
                        }
                        else if (actor.tag == "q2")
                        {
                            q2 = "true";
                        }
                        else if (actor.tag == "q3")
                        {
                            q3 = "true";
                        }

                        
                        DC_OAA_Uimanager.instance.asgharBullshit.text = actor.comments[0];
                        
                        if (actor.tag == "teacher")
                        {
                            DC_OAA_Uimanager.instance.SetFaceteacher();
                            Anim_teacher.SetInteger("State", int.Parse(actor.extraData[0]));
                            StartCoroutine(waitAnim(Anim_teacher));
                        }
                        else
                        {
                            Anim_Asghar.SetInteger("State", int.Parse(actor.extraData[0]));
                            StartCoroutine(waitAnim(Anim_Asghar));
                            DC_OAA_Uimanager.instance.SetFaceAsghar();
                        }

                    }
                }
                
            }
        }
    }


    public void ClickOptionOliver(int option)
    {
        if (isclicked == false)
        {
            CommentIndex = option;
            isclicked = true;
        }

    }

    IEnumerator waitAnim(Animator a)
    {
        EndedEverydialogue = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        a.SetInteger("State", -1);
        yield return new WaitForSeconds(a.GetCurrentAnimatorStateInfo(1).length);
        //        yield return new WaitUntil(() => a.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.99f);     
        //  a.SetInteger("State", -1);
        if (actor.isPlayer)
        {
            isclicked = false;
        }
        
        EndedEverydialogue = true;
        VD.Next();
    }

    public void Check3Question()
    {

        if (q1 == "true" && q2 == "true" && q3 == "true" )
        {
            /// truly no use
         
        }
        
        else
        {
            VD.SetNode(6);
        }
        
    }
    
    public void SpeakingTeacher()
    {
        _camera.SetTrigger("SpeakTeacher");
        Anim_Oliver.SetTrigger("HeadRevers");
        Anim_Asghar.SetTrigger("HeadRevers");
        Oliver.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        Asghar.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;;
        
    }

  
}
