using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ru_BgObject : MonoBehaviour
{

    public float Speed;
	
    // Use this for initialization
    void Start () {

        if (this.transform.name != "Bg_Kharazmi")
        {
            Destroy(this.gameObject,10);
        }

    }
	
    // Update is called once per frame
    void Update () {
        transform.Translate(Speed*Time.deltaTime ,0,0);
    }

}
