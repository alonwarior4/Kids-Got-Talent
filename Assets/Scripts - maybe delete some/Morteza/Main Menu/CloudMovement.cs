using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{    
    [SerializeField] Vector2 moveSpeedRange;
    [SerializeField] float xDeathPos;
    float moveSpeed;

    private void Start()
    {
        moveSpeed = UnityEngine.Random.Range(moveSpeedRange.x, moveSpeedRange.y);
    }

    private void Update()
    {   
        if(xDeathPos - Mathf.Abs(transform.localPosition.x) <= Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
        transform.Translate(moveSpeed, 0, 0);      
    }

}
