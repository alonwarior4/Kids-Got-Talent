using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckinNextObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Obstacle")
		{
			//other.GetComponent<BoxCollider2D>().enabled = false;
			S_ScriptManager.Instance.NextSpawn();
			S_ScriptManager.Instance.IncreaseSpeed();
			Destroy(other.gameObject , 2);
			
		}
	}
}
