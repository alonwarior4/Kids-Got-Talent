using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiPlayAnim : MonoBehaviour
{

    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    KharazmiAnimManager AnimMangerRef;
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

    private void OnMouseDown()
    {
        if(AnimMangerRef.GetCurrentState() == EKharazamiAnimatorState.BallPlayed && ColorManagerRef.GetCurrentColorName() == "Yellow")
        {
            ParentAnimatorRef.SetTrigger("Car_GTC");
            AnimMangerRef.SetCurrentState(EKharazamiAnimatorState.TaxiPlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

    public void TaxiMoveCoroutine()
    {
        StartCoroutine(PlayAnimation());
    }
    private IEnumerator PlayAnimation()
    {
        while(true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Postions[1].transform.position, Time.deltaTime * Speed);
            if (Mathf.Abs(transform.localPosition.x - Postions[1].transform.localPosition.x) <= 0.01f)
            {
                yield return new WaitForSeconds(3f);
                transform.position = Postions[0].transform.position;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
