using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailWalking : MonoBehaviour {

    public float speed;
    [SerializeField] Transform flagPos;
    [SerializeField] Vector2 thispoint;
    [SerializeField] int progressNumber;
    [SerializeField] float partSize;
    [SerializeField] Vector2 TargetTemp;
    public static SnailWalking instance;

    Animator snailAnim;


	// Use this for initialization
	void Awake ()
    {
        if (!instance)
        {
            instance = this;
        }
        snailAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        thispoint = this.transform.localPosition;
        partSize = Vector2.Distance(thispoint, new Vector2(flagPos.localPosition.x + 0.164f , flagPos.localPosition.y) ) / progressNumber;
        TargetTemp = thispoint;
    }

    // Update is called once per frame
    void Update ()
    {
        thispoint = this.transform.localPosition;
        CheckPoint();
	}


    private void CheckPoint()
    {
        if (Vector2.Distance(thispoint ,TargetTemp) > 0.05f)
        {
            Move();
        }
        else
        {
            Stop();
        }
    }

    private void Move()
    {
        snailAnim.SetBool("Movement", true);
        transform.localPosition = Vector2.MoveTowards(thispoint, flagPos.localPosition , speed * Time.deltaTime);
    }

    private void Stop()
    {
        snailAnim.SetBool("Movement", false);
    }

    //public void SetMoveSpeed(float Speed)
    //{
    //    this.speed = Speed;
    //}

    //public void StopMoveSpeed()
    //{
    //    this.speed = 0;
    //}

    public void ChangeTarget()
    {
        TargetTemp -= new Vector2(partSize, 0); 
    }


}
