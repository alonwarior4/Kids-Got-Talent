using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTeacher : MonoBehaviour {

    public Vector2 PosRight;
    public Vector2 PosLeft;
    public float Speed;
    public GameObject Teacher;


    [Tooltip("for programmer Not relevant to design")]
    public bool IsReccive;
    private bool StartMove;
    private Vector2 target;
    private Vector2 angle;
    private Animator anim_teacher;
    public static MovingTeacher instance;

    public bool ToRight;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {

        IsReccive = false;
        anim_teacher = Teacher.GetComponent<Animator>();
        if (ToRight)
        {
            MovingToleftOrRight(true);
        }
        else
            MovingToleftOrRight(false);

    }


    // Update is called once per frame
    void Update()
    {

        if (StartMove)
        {

            if (Vector2.Distance(Teacher.transform.position, target) > Mathf.Epsilon)
            {
                Teacher.transform.position = Vector2.MoveTowards(Teacher.transform.position, target, Speed * Time.deltaTime);
                anim_teacher.SetTrigger("Walk");
                Teacher.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                StartMove = false;
                IsReccive = true;
                anim_teacher.ResetTrigger("Walk");
                anim_teacher.SetTrigger("SpeakPos");
                Teacher.transform.eulerAngles = angle;
                Teacher.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
       
    }



    public void MovingToleftOrRight(bool ROL)
    {
        if (ROL)        /// move to right
        {
            StartMove = true;
            target = PosRight;
            angle = new Vector3(0, 0, 0); 
        }
       else         /// move to right
        {
            StartMove = true;
            target = PosLeft;
            angle = new Vector3(0, 180, 0);
        }
    }
}
