using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Security.Policy;
using JetBrains.Annotations;
using UnityEngine;

public class Ru_SpwanerTrapsObject : MonoBehaviour
{

	public static Ru_SpwanerTrapsObject instance;
	//public  List<Ru_TrapsObject> listSpawn = new List<Ru_TrapsObject>();
	//public GameObject[] PrefabsObject = new GameObject[10];

    public GameObject[] Sheeps;
    public GameObject[] ZirGozar;
    public GameObject[] Other;
    

	public Vector3 PointSpawn;
	public float Delay;
	


	private void Awake()
	{

		if (!instance)
		{
			instance = this;
		}
	}


	void Start () {
		
	}
	

	void Update () {
		
	}

    public void Starting()
    {
        Spwaner();
    }


	//public void AdderToList(Ru_TrapsObject traps)
	//{
	//	bool find = false;
	//	for (int i = 0; i <listSpawn.Count ; i++)
	//	{
	//		if (listSpawn[i] == null)
	//		{
	//			listSpawn[i] = traps;
	//			find = true;
	//			break;
	//		}
	//	}

	//	if (find)
	//	{
	//		listSpawn.Add(traps);
	//	}
	//}

	public void Spwaner()
	{

        //int RandomIndex = Random.Range(0, PrefabsObject.Length);
        

		StartCoroutine(GetTrapsObject());
		
	}

	private IEnumerator GetTrapsObject()
	{
        float f = Random.value;
///        Ru_TrapsObject G;
        if (f < 0.2f)
        {
            Instantiate(Sheeps[UnityEngine.Random.Range(0 , Sheeps.Length)], PointSpawn, Quaternion.identity).GetComponent<Ru_TrapsObject>();
        }
        else if(f >= 0.2f && f < 0.6f)
        {
             Instantiate(ZirGozar[UnityEngine.Random.Range(0 , ZirGozar.Length)], PointSpawn, Quaternion.identity).GetComponent<Ru_TrapsObject>();
        }
        else if(f >= 0.6f)
        {
            Instantiate(Other[UnityEngine.Random.Range(0 , Other.Length)], PointSpawn, Quaternion.identity).GetComponent<Ru_TrapsObject>();
        }
		yield return  new WaitForSeconds(Delay);
         //G = Instantiate(PrefabsObject[r], PointSpawn, Quaternion.identity).GetComponent<Ru_TrapsObject>();
        //AdderToList(G);
	}


    //public void StopBObjects()
    //{
    //    for (int i = 0; i < listSpawn.Count; i++)
    //    {
    //        if (listSpawn[i])
    //        {
    //            listSpawn[i].Speed = 0;
    //        }
    //    }

    //}


}
