using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] FadeManager fadeManager;
    [SerializeField] AudioClip nameSound;
    [SerializeField] AudioClip dragSound;
    [SerializeField] AudioClip dropSound;

    Vector3 firstPos;
    Vector3 screenToWorld;
    bool isHittedRightObject = false;
    bool isDragging = false;
    SpriteRenderer objectSprite;
    int firstSpriteOrderLayer;
    int defaultSortingLayer;

    public static int countOfBodyparts;

    [Header("Configs")]
    [SerializeField] float padding;
    [SerializeField] Collider2D SecondPositionCollider;
    [SerializeField] SpriteRenderer SecondPosSprite;
    [Header("for changing cursor when body parts enabled")]
    [SerializeField] ChangeCursorHiddenObjects changeCursorTexture;


    private void Awake()
    {
        changeCursorTexture.SetCursorReleaseTexture();
        objectSprite = GetComponent<SpriteRenderer>();
        defaultSortingLayer = objectSprite.sortingOrder;
        firstSpriteOrderLayer = objectSprite.sortingOrder;
        screenToWorld = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, 0));
        firstPos = transform.position;
    }

    private void Start()
    {
        countOfBodyparts = 0;
    }


    private void OnMouseDrag()
    {
        if(!isDragging)
        {
            AudioSource.PlayClipAtPoint(dragSound,Camera.main.transform.position,0.3f);
            isDragging = true;
        }
        changeCursorTexture.SetCursorGrabTexture();
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
        changeCursorTexture.SetCursorReleaseTexture();
        AudioSource.PlayClipAtPoint(dropSound,Camera.main.transform.position,0.3f);
        if (isHittedRightObject)
        {
            AudioSource.PlayClipAtPoint(nameSound, Camera.main.transform.position, 1f);
            countOfBodyparts += 1;
            CheckForFinish();
            if (SecondPosSprite == null) { return; }
            SecondPosSprite.enabled = true;
            gameObject.SetActive(false);            
        }
        else
        {
            transform.position = firstPos;
            objectSprite.sortingOrder = firstSpriteOrderLayer;
        }
    }

    private void CheckForFinish()
    {
        if (countOfBodyparts >= 18)
        {
            fadeManager.EndOfstory();
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider == SecondPositionCollider)
        {
            isHittedRightObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider == SecondPositionCollider)
        {
            isHittedRightObject = false;
        }
    }



}
