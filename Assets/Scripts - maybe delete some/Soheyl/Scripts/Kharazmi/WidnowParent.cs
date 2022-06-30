using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidnowParent : MonoBehaviour
{
    //Cache
    Animator ParentAnimatorRef;
    KharazmiAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;
    OliverFacial oliverFacialAnimation;

    //Ray Var's
    RaycastHit2D HitedObject;
    [SerializeField] Camera MainCamera;

    //this code Var's
    string CurrentTag;

    //Bool for Fininsh
   static bool Panjere1Colored = false;
   static bool Panjere2Colored = false;
   static bool Panjere3Colored = false;
   static bool Panjere4Colored = false;
   static bool Panjere5Colored = false;
   static bool Panjere6Colored = false;
   static bool Panjere7Colored = false;

    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManagerRef = GetComponentInParent<KharazmiAnimManager>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }

    private void Start()
    {
        Panjere1Colored = false;
        Panjere2Colored = false;
        Panjere3Colored = false;
        Panjere4Colored = false;
        Panjere5Colored = false;
        Panjere6Colored = false;
        Panjere7Colored = false;
    }

    private void OnMouseDown()
    {
        if(AnimManagerRef.GetCurrentState() == EKharazamiAnimatorState.AirPlanePlayed && ColorManagerRef.GetCurrentColorName() == "Blue")
        {
            RecognizeObjects();
            PlayAnimation();
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

    private void RecognizeObjects()
    {

        if (HitedObject = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.forward)))
        {
            CurrentTag = HitedObject.transform.gameObject.tag;
        }
    }
    private void PlayAnimation()
    {
        switch (CurrentTag)
        {
            case "Panjere1":
                ParentAnimatorRef.SetTrigger("Window1_GTC");
                Panjere1Colored = true;
                CheckCurrentStatus();
                break;
            case "Panjere2":
                ParentAnimatorRef.SetTrigger("Window2_GTC");
                Panjere2Colored = true;
                CheckCurrentStatus();
                break; 
            case "Panjere3":
                ParentAnimatorRef.SetTrigger("Window3_GTC");
                Panjere3Colored = true;
                CheckCurrentStatus();
                break;
            case "Panjere4":
                ParentAnimatorRef.SetTrigger("Window4_GTC");
                Panjere4Colored = true;
                CheckCurrentStatus();
                break;
            case "Panjere5":
                ParentAnimatorRef.SetTrigger("Window5_GTC");
                Panjere5Colored = true;
                CheckCurrentStatus();
                break;
            case "Panjere6":
                ParentAnimatorRef.SetTrigger("Window6_GTC");
                Panjere6Colored = true;
                CheckCurrentStatus();
                break;
            case "Panjere7":
                ParentAnimatorRef.SetTrigger("Window7_GTC");
                Panjere7Colored = true;
                CheckCurrentStatus();
                break;
        }
    }

    private void CheckCurrentStatus()
    {
        if(Panjere1Colored && Panjere2Colored && Panjere3Colored && Panjere4Colored && Panjere5Colored && Panjere6Colored && Panjere7Colored)
        {
            AnimManagerRef.SetCurrentState(EKharazamiAnimatorState.KharazmiCompleted);
            UIManagement uiManage = FindObjectOfType<UIManagement>();
            uiManage.TellFinalStory();
            uiManage.isEndOfColoring = true;
        }
    }
}
