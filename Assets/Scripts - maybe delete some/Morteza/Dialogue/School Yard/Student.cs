using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    [Header("move speed")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    //random time beteween animation change
    [Tooltip("Random time between every animation change")][SerializeField] Vector2 waitTimeRange;

    //ypos array for random choose
    float[] YPos = { 2, 2.35f, 2.7f };

    //configs
    float moveSpeed;
    float defualtXScaleSign;
    float newScale;
    int defualtYPosIndex = 0;
    int defualtSortingOrder;

    //cash ref
    Animator studentAnimator;
    SpriteRenderer studentSpriteRenderer;
       



    private void Awake()
    {
        studentAnimator = GetComponent<Animator>();
        studentSpriteRenderer = GetComponent<SpriteRenderer>();     
    }

    private void Start()
    {
        moveSpeed = -Mathf.Sign(transform.localScale.x) * walkSpeed;
        defualtSortingOrder = studentSpriteRenderer.sortingOrder;       
    }

    private void Update()
    {       
        transform.Translate( moveSpeed * Time.deltaTime, 0, 0);
    }    

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Environment"))
        {
            defualtXScaleSign = Mathf.Sign(transform.localScale.x);
            SelectRandomYPosition();
            transform.localScale = new Vector3(-defualtXScaleSign * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            StartCoroutine(MovementCycle());
        }     
    }

    IEnumerator MovementCycle()
    {
        moveSpeed = 0;
        float waitTime = UnityEngine.Random.Range(waitTimeRange.x, waitTimeRange.y);
        yield return new WaitForSeconds(waitTime);       
        SelectRandomAnimationState();
    }

    private void SelectRandomYPosition()
    {       
        int randomYPosIndex = UnityEngine.Random.Range(0, YPos.Length);
        while(randomYPosIndex == defualtYPosIndex)
        {
            randomYPosIndex = UnityEngine.Random.Range(0, YPos.Length);
        }
        defualtYPosIndex = randomYPosIndex;
        transform.localPosition = new Vector3(transform.localPosition.x, -YPos[randomYPosIndex], transform.localPosition.z);
        switch (randomYPosIndex)
        {
            case 0:
                studentSpriteRenderer.sortingOrder = defualtSortingOrder;
                newScale = 0.75f;
                break;
            case 1:
                studentSpriteRenderer.sortingOrder = defualtSortingOrder + 2;
                newScale = 0.81f;
                break;
            case 2:
                studentSpriteRenderer.sortingOrder = defualtSortingOrder + 4;
                newScale = 0.87f;
                break;
        }
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void SelectRandomAnimationState()
    {
        bool[] triggers = { true, false };
        int randomIndex = UnityEngine.Random.Range(0, triggers.Length);
        SwitchAnimation(triggers[randomIndex]);
    }

    private void SwitchAnimation(bool trigger)
    {       
        studentAnimator.SetBool("Run" , trigger);
        if(trigger == false)
        {
            moveSpeed = walkSpeed * -Mathf.Sign(transform.localScale.x) ;
        }
        else if(trigger == true)
        {
            moveSpeed = runSpeed * -Mathf.Sign(transform.localScale.x) ;
        }
    }



}
