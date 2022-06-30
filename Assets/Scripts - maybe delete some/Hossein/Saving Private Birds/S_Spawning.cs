using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class S_Spawning : MonoBehaviour {

    public GameObject[] ObstaclePrefabs =new GameObject[10];
	public Transform LastObject;
	public int NumberInitSpawn;
	public Vector3 vecInit;
	public Expression Exp;
	void Start () {
			
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spawner()
	{
		GameObject g = Instantiate(ObstaclePrefabs[Random.Range(0 , ObstaclePrefabs.Length)] , GetVectorPos() + LastObject.position, Quaternion.identity);
		LastObject = g.GetComponent<Transform>();
	}

	private Vector3 GetLengthLastObject()
	{
		float x1 = LastObject.GetComponent<BoxCollider2D>().bounds.size.x;
		
		Vector3 v = new Vector3(x1 , 0 ,0);
		return v;
	}

	private Vector3 GetVectorPos()
	{
        float time = Time.timeSinceLevelLoad;

        float X = Exp.TwoPoint * Mathf.Sin(Exp.Curve * time*(Mathf.PI/180)) - (Exp.Slope* time) + Exp.StartPO;

		if (X<=10)
		{
			X = GetLengthLastObject().x+Exp.DistanceAfterHighestDificulty;
		}
		
		Vector3 vec;


		vec = new Vector3(X , 0 , 0);
		return vec;
	}
	
	public void Init()
	{
		Vector3 TempVec = Vector3.zero;
		for (int i = 0; i <NumberInitSpawn; i++)
		{
			TempVec += new Vector3(Random.Range(14, 25), 0, 0);
			GameObject g = Instantiate(ObstaclePrefabs[Random.Range(0 , ObstaclePrefabs.Length)] ,vecInit+ TempVec, Quaternion.identity);
		
			LastObject = g.GetComponent<Transform>();
		}

	}
	
	
	[System.Serializable]
	public class Expression
	{
		public float StartPO;
		public float Slope;
		public float Curve;
		public float TwoPoint;
		public float DistanceAfterHighestDificulty;
	}
}
