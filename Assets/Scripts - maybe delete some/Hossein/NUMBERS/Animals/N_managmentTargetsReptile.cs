using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class N_managmentTargetsReptile : MonoBehaviour
{
    public static N_managmentTargetsReptile Instance;
   // public Vector3 R_VecStart1;
    
        public void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
        }
         
        public R_Targets[] forCome = new R_Targets[10];
        public R_Targets[] forGo = new R_Targets[10] ;
        public Transform[] PosStart = new Transform[5];
    
    
        public int GetTargetCome()
        {
            int ret = -1;
            for (int i = 0; i <forCome.Length; i++)
            {
                if (forCome[i].InUse ==false)
                {
                    ret = i;
                    forCome[i].InUse = true;
                    break;
                }           
            }
            return ret;
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


    //TODO for test delete
    private void OnDestroy()
    {
        Instance = null;
    }

}


    [System.Serializable]
    public class R_Targets
    {

        [HideInInspector] public bool InUse;
        public Transform Vec3;
    }

