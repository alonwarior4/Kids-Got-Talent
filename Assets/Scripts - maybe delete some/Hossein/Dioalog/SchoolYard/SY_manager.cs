using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using TMPro;
using System;

public class SY_manager : MonoBehaviour {
    public AudioClip clip_bell;
    private VD.NodeData actor;
    public Animator Anim_Asghar;
    public Animator Anim_Oliver;
    
    // will be change
    public float Delay;

    private bool F_StartDialogue;
    private int CommentIndex;
    private bool isclicked;
    private bool EndedEverydialogue;
    // Use this for initialization
    void Start () {

        isclicked = false;
        F_StartDialogue = false;
        EndedEverydialogue = true;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
        StartCoroutine(DelayFirst());
        SY_UiManager.instance.DisableUIAsghar();
        SY_UiManager.instance.DisableUIOliver();
    }

    // Update is called once per frame
    public void Update()
    {
        if (F_StartDialogue )
        {
            StartDialogue();
        }

    }

    public void StartDialogue()
    {
        if (VD.isActive)
        {
            actor = VD.nodeData;
            if (actor.isEnd)
            {
                VD.EndDialogue();
                F_StartDialogue = false;
                /// 
            }
          
            if (actor.isPlayer)
            {
 
                if (EndedEverydialogue)
                {
                    SY_UiManager.instance.enableUIOliver(actor.comments.Length);
                    for (int i = 0; i < actor.comments.Length; i++)
                    {
                        SY_UiManager.instance.Btn_oliver[i].transform.GetChild(0).GetComponent<TMP_Text>().text = actor.comments[i];
                    }

                    if (isclicked)
                    {

                        StartCoroutine(waitAnim(Anim_Oliver));
                        actor.commentIndex = CommentIndex;
                        Anim_Oliver.SetInteger("FY_DialogueParameter", int.Parse(actor.extraData[CommentIndex]));
                        if (actor.extraData[CommentIndex] == "10")
                        {
                            StartCoroutine(ShowOffFatherOliver());
                        }
                    }
                }
            }
            else
            {
                if (EndedEverydialogue)
                {
                    SY_UiManager.instance.enableUIAsghar();
                    SY_UiManager.instance.DisableUIOliver();
                    Anim_Asghar.SetInteger("FY_DialogueParameter", int.Parse(actor.extraData[0]));
                    SY_UiManager.instance.txt_asghar.GetComponent<TMP_Text>().text = actor.comments[0];
                    StartCoroutine(waitAnim(Anim_Asghar));
                }
            }

        }
        else
        { /// ended 

      //      print("Dialog is ended");
        }
    }

    public void ClickOptionOliver(int option)
    {
        CommentIndex = option;
        isclicked = true;
    }

    IEnumerator waitAnim(Animator a)
    {
        EndedEverydialogue = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        a.SetInteger("FY_DialogueParameter", -1);
        yield return new WaitForSeconds(a.GetCurrentAnimatorStateInfo(1).length);
      
        if (actor.isPlayer)
        {
            isclicked = false;
        }
        if (actor.tag == "end")
        {
            VD.EndDialogue();
            F_StartDialogue = false;
            StartCoroutine(finishscene());
        }
        else
        {
            EndedEverydialogue = true;
            VD.Next();
        }
    }

    IEnumerator finishscene()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(_SceneManager.Instance.NextAutoScene());

    }

    IEnumerator DelayFirst()
    {
        yield return new WaitForSeconds(Delay);
        F_StartDialogue = true;
    }

    public void ShowFatherOliver()
    {
        SY_UiManager.instance.g_oliverfather.SetActive(true);
        SY_UiManager.instance.g_oliverfather.GetComponent<Animator>().SetTrigger("shake");
    }

    IEnumerator ShowOffFatherOliver()
    {
        SY_UiManager.instance.g_oliverfather.GetComponent<Animator>().SetTrigger("off");
        yield return new WaitUntil(()=>SY_UiManager.instance.g_oliverfather.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <.98f);
        SY_UiManager.instance.g_oliverfather.SetActive(false);

    }

    public void playBell()
    {
        AudioSource.PlayClipAtPoint(clip_bell , Camera.main.transform.position);
        StartCoroutine(waitbell());
       
    }

    IEnumerator waitbell()
    {
        yield return new WaitForSeconds(4);
        var b = VD.nodeData;
        b.pausedAction = false;
        VD.Next();
    }
}
