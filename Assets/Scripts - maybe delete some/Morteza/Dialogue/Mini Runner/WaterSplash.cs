using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<Ru_CharcterController>())
        {
            transform.GetComponentInChildren<Animator>().SetTrigger("WaterSplash");
        }       
    }

}
