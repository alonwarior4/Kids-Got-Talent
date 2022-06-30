using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DC_OliverComing: MonoBehaviour {

    public float DelayStart;
    public Animator Door;
    public Transform TargetStanding;
    public GameObject Oliver;
    public float Speed;
    public Animator camera_com;
    public float DelaySpeekToTeacher;
    public float ShowPosOliver;
    private bool Isreccive;
    private bool IsStarting;
    private WaitForEndOfFrame w = new WaitForEndOfFrame();
	// Use this for initialization
	void Start () {

        Isreccive = false;
        IsStarting = false;
        StartCoroutine(_Start());
        Oliver.GetComponent<Animator>().SetTrigger("Walk");
        Oliver.GetComponent<SpriteRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (IsStarting)
        {
            Moving();
        }


    }

    private void Moving()
    {
        

        if (Isreccive == false)
        {
            if (Vector3.Distance(Oliver.transform.position, TargetStanding.position) >= .7f)
            {
                Oliver.transform.position = Vector2.MoveTowards(Oliver.transform.position, TargetStanding.position, Speed * Time.deltaTime);
            }
            else
            {
                Isreccive = true;
            }

        }
       

        if (Oliver.transform.position.x - ShowPosOliver <= Mathf.Epsilon)
        {
            Oliver.GetComponent<SpriteRenderer>().enabled = true;   
        }
    }

    IEnumerator _Start()
    {   
        yield return new WaitForSeconds(DelayStart);
        yield return new WaitUntil(() => MovingTeacher.instance.IsReccive);
        IsStarting = true;
        Door.SetTrigger("OpenDoor");                            /// dar baz mishe

        yield return new WaitForSeconds(4);                     
        Door.SetTrigger("CloseDoor");                       /// dar baz mishe
        yield return new WaitForSeconds(1);
        camera_com.SetTrigger("OliverComing");                 // dorbin zoom mishe

        while (true)
        {
            yield return w;
            if (Isreccive == true)
            {
                Oliver.GetComponent<Animator>().SetTrigger("SpeakPos");
                
                yield return new WaitForSeconds(DelaySpeekToTeacher);
                DC_DialogueTeacherAndOliver.Instance.StartDialogue();
                Destroy(this, 1);
                break;
            }
        }

        

    }

}
