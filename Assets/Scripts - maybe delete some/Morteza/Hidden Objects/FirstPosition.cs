using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPosition : MonoBehaviour
{

    [SerializeField] AudioClip nameSound;
    [SerializeField] AudioClip dragSound;
    [SerializeField] AudioClip dropSound;
    [SerializeField] AudioClip wrongDropSound;


    public static int counterObj;

    Vector3 firstPos;
    Vector3 screenToWorld;    
    bool isHittedRightObject = false;
    bool isDragging = false;
    SpriteRenderer objectSprite;
    int firstSpriteOrderLayer;
    int defaultSortingLayer;

    [Header("Configs")]
    [SerializeField] float padding;
    [SerializeField] Collider2D SecondPositionCollider;
    [SerializeField] GameObject SecondPosSprite;
    [SerializeField] int listIndex;

    [Header("cursor texture script ref")]
    [SerializeField] ChangeCursorHiddenObjects changeCursorRef;
    


    private void Awake()
    {
        

        objectSprite = GetComponent<SpriteRenderer>();
        defaultSortingLayer = objectSprite.sortingOrder;
        firstSpriteOrderLayer = objectSprite.sortingOrder;
        screenToWorld = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, 0));
        firstPos = transform.position;
    }

    private void Start()
    {
        counterObj = 0;
    }


    private void OnMouseDrag()
    {
        if(!isDragging)
        {
            AudioSource.PlayClipAtPoint(dragSound, Camera.main.transform.position,0.5f);
            isDragging = true;
        }   
        changeCursorRef.SetCursorGrabTexture();
        objectSprite.sortingOrder = defaultSortingLayer + 10;
        float newXPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        float newYPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
        Vector3 mousePos = Camera.main.ViewportToWorldPoint(new Vector3
            (Mathf.Clamp(newXPos, padding, screenToWorld.x - padding),
             Mathf.Clamp(newYPos, padding, screenToWorld.y - padding), 0));
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }


    private void OnMouseUp()
    {
        isDragging = false;
        changeCursorRef.SetCursorReleaseTexture();
        if (isHittedRightObject)
        {
            AudioSource.PlayClipAtPoint(dropSound,Camera.main.transform.position,0.3f);
            counterObj += 1;
            CheckForFinish();
            AudioSource.PlayClipAtPoint(nameSound, Camera.main.transform.position, 1f);
            if (SecondPosSprite == null) { return; }
            SecondPosSprite.GetComponent<SpriteRenderer>().enabled = true;           
            FindObjectOfType<Hint>().CheckAndSelectHint(listIndex);
            FindObjectOfType<ObjectName>().changeText(listIndex);
            gameObject.SetActive(false);         
        }
        else
        {
            AudioSource.PlayClipAtPoint(wrongDropSound,Camera.main.transform.position,0.2f);
            transform.position = firstPos;
            objectSprite.sortingOrder = firstSpriteOrderLayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {        
        if(otherCollider == SecondPositionCollider)    
        {           
            isHittedRightObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if(otherCollider == SecondPositionCollider)
        {
            isHittedRightObject = false;
        }
    }

    private void CheckForFinish()
    {
        if(counterObj >= 13)
        {
            StartEndStoryManager.instance.EndOfHiddenObj();
        }
    }

}
