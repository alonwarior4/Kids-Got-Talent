using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public enum AnimalType {Reptile , Bird }
public class Animals : MonoBehaviour
{
	
	public Vector3 MyTarget;
    public AnimalType MyType;
    public string Name;
    public Animator ThisAnim; 
    [SerializeField]
    public float speed;
    [SerializeField] 
    //protected int Direction;
    public bool IsAnswer;
    public int DelayDisable ;
    public bool isfinish;
    public bool IsSceneSaving;
    protected bool SetBeforeTarget;
    // Use this for initializatio

    public virtual void OnMouseDown()
    { 
	    N_SoundManager.Instance.PlaySound("S_Click");
		if(IsAnswer)
		{
		    N_SoundManager.Instance.PlaySound("S_True");
		}
		else
		{
			N_SoundManager.Instance.PlaySound("S_Wrong");
		}	
    }

    public void DisableColliders()
	{
		this.GetComponent<Collider2D>().enabled = false;
	}

	public void EnableColliders()
	{
		this.GetComponent<Collider2D>().enabled = true;
	}

	public void Remove()
	{
		 Destroy(this.gameObject , DelayDisable);
	}

	public virtual void Come()
	{
	//	print("in master");
	}

	public virtual void Go()
	{
	//	print("father function");
	}

	public virtual void EndQuestion()
	{
		Go();
       	IsAnswer = false;
		ThisAnim.SetTrigger("Fly Up");
		Remove(); 
       	DisableColliders();

     	}
	
	public virtual void Movement()
	{
		this.transform.position =	Vector3.MoveTowards(this.transform.position, MyTarget, Time.deltaTime*speed );
	}

	public virtual void CheckingDirection(Vector3 v)
	{
	
    }

	public void SetSpeed(float speedin)
	{
		speed = speedin;		
	}


}
