using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckingCat : MonoBehaviour
{
    [SerializeField] GameObject tire1, tire2;
    public float force;
    Rigidbody2D rigid;

    Animator catAnim;

    private void Awake()
    {
        catAnim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckForPush();
        RotateTires();
    }


    private void RotateTires()
    {             
        tire1.transform.Rotate(0, 0, -Mathf.Abs( rigid.velocity.x ) * 600 * Time.deltaTime);
        tire2.transform.Rotate(0, 0, -Mathf.Abs( rigid.velocity.x ) * 600 * Time.deltaTime);            
    }

    public void AddForce()
    {
        rigid.AddRelativeForce(transform.right * force  , ForceMode2D.Impulse);
    }

    private void CheckForPush()
    {
        if (Mathf.Abs(rigid.velocity.x) < 2)
        {
            catAnim.SetBool("Pushing", true);
        }
        else
        {
            catAnim.SetBool("Pushing", false);
        }
    }

    public void PlaySkateSound(AudioClip smellycat)   
    {
        if (GetComponent<AudioSource>().enabled)
        {
            GetComponent<AudioSource>().PlayOneShot(smellycat, 0.65f);
        }
    }
}
