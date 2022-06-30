using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SheepPlayAnim : MonoBehaviour
{

    OliverFacial oliverFacialAnimation;
    Animator ParentAnimatorRef;
    JungleAnimManager AnimManagerRef;
    ColorManager ColorManagerRef;
    [SerializeField] List<GameObject> EatLocations;
    [SerializeField] [Range(0.5f, 5f)] float Speed = 1f;
    SpriteRenderer SpriteRef;

    private void Awake()
    {
        ParentAnimatorRef = GetComponentInParent<Animator>();
        AnimManagerRef = GetComponentInParent<JungleAnimManager>();
        SpriteRef = GetComponent<SpriteRenderer>();
        ColorManagerRef = GetComponentInParent<ColorManager>();
        oliverFacialAnimation = FindObjectOfType<OliverFacial>();
    }
    private void OnMouseDown()
    {
        if(AnimManagerRef.GetCurrentAnim() == EJungleAnimatorState.DogColoringPlayed  &&  ColorManagerRef.GetCurrentColorName() == "Gray")
        {
            ParentAnimatorRef.SetTrigger("Sheep_GTC");
            AnimManagerRef.SetCurrentAnim(EJungleAnimatorState.SheepColoringPlayed);
            oliverFacialAnimation.SetOliverAnimatorState(3);
        }
        else
        {
            oliverFacialAnimation.SetOliverAnimatorState(1);
        }
    }

    public void CallCoroutine()
    {
        ParentAnimatorRef.SetBool("Sheep_Walk", true);
        StartCoroutine(MoveSheep());
    }

    private IEnumerator MoveSheep()
    {
        for (int index = 0; index < EatLocations.Count; index++)
        {

            while (true)
            {
                if(Mathf.Abs(transform.localPosition.x - EatLocations[index].transform.localPosition.x) >= 0.01f) // Not Reach
                {
                    transform.position = Vector2.MoveTowards(transform.position, EatLocations[index].transform.position, Speed * Time.deltaTime);
                }
                else //Reach
                {
                    StopToEat();
                    yield return new WaitForSeconds(5.0f);

                    if (index != EatLocations.Count - 1) // Not Reach Last Location.
                    {
                        DontMichaelJackson();
                        WalkAndNotEat();
                    }
                    else
                    {
                        yield return new WaitForSeconds(3f);
                        SpriteRef.flipX = true;
                        WalkAndNotEat();
                        index = -1;
                    }

                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        yield return new WaitForEndOfFrame();
    }

    private void DontMichaelJackson()
    {
        if (SpriteRef.flipX)
        {
            SpriteRef.flipX = false;
        }
    }

    private void WalkAndNotEat()
    {
        ParentAnimatorRef.SetBool("Sheep_Walk", true);
        ParentAnimatorRef.SetBool("Sheep_Eat", false);
    }

    private void StopToEat()
    {
        ParentAnimatorRef.SetBool("Sheep_Walk", false);
        ParentAnimatorRef.SetBool("Sheep_Eat", true);
    }
}
