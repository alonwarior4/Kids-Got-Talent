using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VIDE_Data;


public class DC_DialogueTeacherAndOliver : MonoBehaviour {

    private int CommentIndex;
    public static DC_DialogueTeacherAndOliver Instance;
//    public float Delay;
    public Animator Anim_Oliver;
    public Animator Anim_Teacher;
    public GameObject OliverAndAsghar;
    private bool F_StartDialogue;
    private VD.NodeData actor;
    private bool EndedEverydialogue;
    public bool isclicked;
    public GameObject PanelDlgOliverAndAsghar;
    public GameObject PanelDlgOliverAndTeacher;

   private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {

        ////////////////////////////

        OliverAndAsghar.SetActive(false);
        EndedEverydialogue = true;
        isclicked = false;
        F_StartDialogue = false;
        DC_OAT_UiManager.instance.DisableUIOliver();
        DC_OAT_UiManager.instance.DisableUITeacher();
        
        PanelDlgOliverAndAsghar.SetActive(false);
        PanelDlgOliverAndTeacher.SetActive(true);


    }

    // Update is called once per frame
    void Update () {

        if (F_StartDialogue)
        {
//            print("Start conv");
            Conversation();
        }
    }

    public void StartDialogue()
    {
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
        Anim_Teacher.SetTrigger("SpeakPos");
        F_StartDialogue = true;      
        // starting dialogue between teacher and oliver
    }

    public void Conversation()
    {

        if (VD.isActive)
        {
            actor = VD.nodeData;
            if (actor.isEnd)
            {
                VD.EndDialogue();
                F_StartDialogue = false;
                // print("Dialog is ended");
                //  VD.assigned.assignedDialogue SKFDSD
                //       DESTROY 
                ///           AND ENABLE OTHER SCRIPT 
                /// 
                OliverAndAsghar.SetActive(true);
                //  Destroy(this);
            }
            else
            {
                if (actor.isPlayer)
                {
                    if (EndedEverydialogue)
                    {
                        DC_OAT_UiManager.instance.enableUIOliver(actor.comments.Length);
                        for (int i = 0; i < actor.comments.Length; i++)
                        {
                            DC_OAT_UiManager.instance.Btn_oliver[i].transform.GetChild(0).GetComponent<TMP_Text>().text = actor.comments[i];
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
                        DC_OAT_UiManager.instance.enableUITeacher();
                        DC_OAT_UiManager.instance.DisableUIOliver();
                        Anim_Teacher.SetInteger("State", int.Parse(actor.extraData[0]));
                        DC_OAT_UiManager.instance.txt_Teacher.transform.GetChild(0).GetComponent<TMP_Text>().text = actor.comments[0];
                        StartCoroutine(waitAnim(Anim_Teacher));

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
    
}
