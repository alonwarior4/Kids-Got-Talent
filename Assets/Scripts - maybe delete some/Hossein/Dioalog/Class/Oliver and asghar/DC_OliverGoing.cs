using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_OliverGoing : MonoBehaviour {

    public static DC_OliverGoing instance; 
    public GameObject OliverStanding;
    public GameObject OliverSiting;
    public Animator Asghar;
    public Transform PosGo;
    public Animator Camera;
    public float Speed;
    public GameObject OliverAndTeacher;
    private bool F_going;
    private bool Reccived;

    void Awake()
    {
        if (!instance )
        {
            instance = this;
        }

    }
    // Use this for initialization
    void Start () {
        F_going = false;
        Reccived = false;
        OliverAndTeacher.SetActive(false);
        StartCoroutine(GoingToOutSide());
    }
	
	// Update is called once per frame
	void Update () {

        if (F_going)
        {
            Go();
        }
	}

    IEnumerator GoingToOutSide()
    {
        yield return new WaitForSeconds(1);
        OliverStanding.transform.eulerAngles = new Vector3(0, 180, 0);
     //   yield return new WaitForSeconds(1);
        F_going = true;
        OliverStanding.GetComponent<Animator>().SetTrigger("Walk");
        OliverStanding.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        OliverSiting.SetActive(true);
        yield return new WaitUntil(() => Reccived);
        OliverSiting.SetActive(true);
        OliverStanding.SetActive(false);
        Camera.SetTrigger("OliverAndAsghar");
        Asghar.SetTrigger("SpeakPos");  
        OliverSiting.GetComponent<Animator>().SetTrigger("SpeakPos");
        yield return new WaitForSeconds(2);
        DC_dialogueOliverAndAsghar.Instance.StartDialogue();
        //Destroy(this);
    }

    private void Go()
    {
        if (Vector2.Distance(OliverStanding.transform.position, PosGo.position) > Mathf.Epsilon)
        {
            OliverStanding.transform.position = Vector2.MoveTowards(OliverStanding.transform.position, PosGo.position, Speed * Time.deltaTime);
        }
        else
        {
            F_going = false;
            Reccived = true;
        }

    }
}
