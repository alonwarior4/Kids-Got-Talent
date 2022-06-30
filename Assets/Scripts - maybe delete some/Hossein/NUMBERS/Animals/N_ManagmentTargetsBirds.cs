using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class N_ManagmentTargetsBirds : MonoBehaviour
{
    public static N_ManagmentTargetsBirds Instance;
   // public Vector3 B_VecStart1;
    
    public void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public B_Targets[] forCome = new B_Targets[10];
    public B_Targets[] forGo = new B_Targets[10] ;
    public Transform[] PosStart = new Transform[5];
    
    public Vector3 GetTargetCome()
    {
        int ret = -1;
        int ran = UnityEngine.Random.Range(0,forCome.Length);
        while (forCome[ran].InUse)
        {
            ran = UnityEngine.Random.Range(0, forCome.Length);
        }
        ret = ran;
        forCome[ran].InUse = true;
        //for (int i = 0; i <forCome.Length; i++)
        //{
        //    if (forCome[i].InUse ==false)
        //    {
        //        ret = i;
        //        forCome[i].InUse = true;
        //        break;
        //    }           
        //}
        return forCome[ret].Vec3.position;
    }
    public Vector3 GetTargetGo()
    {
        int ret = UnityEngine.Random.Range(0, forGo.Length);
        return forGo[ret].Vec3.position;
    }

    public Vector3 GetPosStart()
    {
        return PosStart[UnityEngine.Random.Range(0, PosStart.Length)].position;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}

[System.Serializable]
public class B_Targets
{
    public bool InUse;
    public Transform  Vec3;
}

