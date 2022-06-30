using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    //Change 1
    public bool OnMoving;
    public float Speed;
    public Vector3 StartPo;
    public Vector3 TargetPo;

    // Use this for initialization
    void Start()
    {
       // OnMoving = true;
        StartPo = new Vector3(StartPo.x, this.transform.position.y, 2);
        TargetPo = new Vector3(TargetPo.x, this.transform.position.y, 2);
        
        Destroy(this.gameObject , 100);

    }

    // Update is called once per frame
    void Update()
    {
        if (OnMoving)
        {
            if (this.transform.position.x > TargetPo.x)
            {
                transform.Translate(-Speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position = StartPo;
            }
        }
        
    }
    
    
}