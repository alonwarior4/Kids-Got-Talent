using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ru_TrapsObject : MonoBehaviour
{

	[SerializeField]float Speed;
	
	// Use this for initialization
	void Start ()
    {
        Speed = Minirunner_BG.instance.bgMoveSpeed;
		Destroy(this.gameObject,20);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Minirunner_BG.instance.HasToRun)
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        }		
	}


}
