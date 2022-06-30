using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoringHajiFiroz : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteForChangeColor;
    Animator AnimatorRef;
    GetHajiFiroozColors GetHajiFiroozColorsRef;
    ChangeColor changeColorRef;
    Collider2D partCollider;

    bool objectHasErased = false;
    bool firstTimeColoring = false;


    private void Awake()
    {
        
    }

    private void Start()
    {
        AnimatorRef = GetComponent<Animator>();
        GetHajiFiroozColorsRef = FindObjectOfType<GetHajiFiroozColors>();
        changeColorRef = FindObjectOfType<ChangeColor>();
        partCollider = GetComponent<Collider2D>();
    }

    public void OnMouseDown()
    {
        if(firstTimeColoring == false)
        {
            if (changeColorRef.isEraser == false)
                StartCoroutine(WaitForColoring());
        }
        else
        {
           if (changeColorRef.isEraser == true)
           {
               if (objectHasErased == false)
                   StartCoroutine(WaitForErasing());
           }
           else
           {
               if (objectHasErased == true)
                   StartCoroutine(WaitForRecoloring());
           }
        }

        //hasWhiteSpriteBehind = SpriteForChangeColor.color.Equals(new Color(1, 1, 1, 1));
        //if (hasWhiteSpriteBehind)
        //{
        //    if (changeColorRef.isEraser == false)
        //        StartCoroutine(WaitForColoring());
        //}
        //else
        //{
        //    if (changeColorRef.isEraser == true)
        //    {
        //        if (objectHasErased == false)
        //            StartCoroutine(WaitForErasing());
        //    }
        //    else
        //    {
        //        if (objectHasErased == true)
        //            StartCoroutine(WaitForRecoloring());
        //    }
        //}
    }

    IEnumerator WaitForColoring()
    {       
        partCollider.enabled = false;
        SpriteForChangeColor.color = GetHajiFiroozColorsRef.GetColors();
        AnimatorRef.SetTrigger("GoToColoring");
        firstTimeColoring = true;
        //hasWhiteSpriteBehind = false;
        yield return new WaitForEndOfFrame();       
        yield return new WaitUntil(()=> AnimatorRef.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f);
        partCollider.enabled = true;
    }

    IEnumerator WaitForRecoloring()
    {
        partCollider.enabled = false;
        SpriteForChangeColor.color = GetHajiFiroozColorsRef.GetColors();
        objectHasErased = false;
        AnimatorRef.SetTrigger("ReColor");       
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(()=> AnimatorRef.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f);
        partCollider.enabled = true;
    }

    IEnumerator WaitForErasing()
    {
        partCollider.enabled = false;
        objectHasErased = true;
        AnimatorRef.SetTrigger("Erasing");       
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(()=> AnimatorRef.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f);
        partCollider.enabled = true;
    }


}
