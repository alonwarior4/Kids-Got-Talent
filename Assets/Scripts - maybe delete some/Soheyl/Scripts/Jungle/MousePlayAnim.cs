using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePlayAnim : MonoBehaviour
{

    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    JungleAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;
    [SerializeField] List<GameObject> SmellLocations;
    [SerializeField] float Speed = 3f;
    [SerializeField] GameObject RespawnPointRef;
    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManagerRef = GetComponentInParent<JungleAnimManager>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }
    private void OnMouseDown()
    {
        if(AnimManagerRef.GetCurrentAnim() == EJungleAnimatorState.CorocodileColoringPlayed && ColorManagerRef.GetCurrentColorName() == "Black")
        {
            ParentAnimatorRef.SetTrigger("Mouse_GTC");
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

    public void MoveToSmells() 
    {
        ParentAnimatorRef.SetBool("Mouse_Walk", true);
        StartCoroutine(MoveToListCOR(SmellLocations));

        /////TODO   HOSSEIN ADDED FOR FINISHED
        UIManagement uiManager = FindObjectOfType<UIManagement>();
        uiManager.TellFinalStory();
        uiManager.isEndOfColoring = true;
    }
    ///TODO HOSSEIN ADDED
   


    private IEnumerator MoveToListCOR(List<GameObject> LocationList) //TODO Random
    {
        for (int index = 0; index < LocationList.Count; index++)
        {
            while (true)
            {
                if ((transform.localPosition.x -LocationList[index].transform.localPosition.x) >= 1f) //Not Reach Location                                  
                {
                    transform.position = Vector2.MoveTowards(transform.position, LocationList[index].transform.position, Speed * Time.deltaTime);
                }
                else // Reach Location
                {
                    ParentAnimatorRef.SetBool("Mouse_Walk", false);
                    ParentAnimatorRef.SetBool("Mouse_Smell", true);

                    yield return new WaitForSeconds(2f);

                    if(index != LocationList.Count -1) // if not Reach Last Location yet.
                    {
                        ParentAnimatorRef.SetBool("Mouse_Smell", false);
                        ParentAnimatorRef.SetBool("Mouse_Walk", true);
                    }
                    else
                    {
                        ParentAnimatorRef.SetBool("Mouse_Smell", false);
                        ParentAnimatorRef.SetBool("Mouse_Walk", true);
                        transform.localPosition = RespawnPointRef.transform.localPosition;
                        Invoke("MoveToSmellAgain", 2f);
                    }
                    break;
                }
                yield return new WaitForEndOfFrame();
             }
        }
        yield return new WaitForEndOfFrame();
    }

    private void MoveToSmellAgain()
    {
        ParentAnimatorRef.SetBool("Mouse_Walk", true);
        StartCoroutine(MoveToListCOR(SmellLocations));
    }
}
