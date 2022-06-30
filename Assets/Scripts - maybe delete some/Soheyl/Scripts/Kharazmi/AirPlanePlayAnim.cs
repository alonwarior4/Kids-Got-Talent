using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlanePlayAnim : MonoBehaviour
{

    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    KharazmiAnimManager AnimMangerRef;
    Coroutine AnimPlayCache;
    ColorManager ColorManagerRef;
    [SerializeField] GameObject[] Postions;
    [SerializeField] float Speed = 1;


    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimMangerRef = GetComponentInParent<KharazmiAnimManager>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }

    private void Start()
    {
        PlayAirPlaneCoroutine();
    }

    private void OnMouseDown()
    {
        if (AnimMangerRef.GetCurrentState() == EKharazamiAnimatorState.FlagCompeleted && ColorManagerRef.GetCurrentColorName() == "Orange")
        {
            StopCoroutine(AnimPlayCache);
            ParentAnimatorRef.SetTrigger("AirPlane_GTC");
            AnimMangerRef.SetCurrentState(EKharazamiAnimatorState.AirPlanePlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }


    public void PlayAirPlaneCoroutine()
    {
        AnimPlayCache = StartCoroutine(PlayAnimation());
    }


    private IEnumerator PlayAnimation()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Postions[1].transform.position, Time.deltaTime * Speed);
            if (Mathf.Abs(transform.localPosition.x - Postions[1].transform.localPosition.x) <= 0.01f)
            {
                yield return new WaitForSeconds(1.5f);
                transform.position = Postions[0].transform.position;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
