using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YardCollider : MonoBehaviour
{
    private int rotate = 0;
    [SerializeField] Transform childPos;
    [SerializeField] GameObject ball;

    [SerializeField] Vector2 delay;

    IEnumerator OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject other = otherCollider.gameObject;
        if (other.GetComponent<FuckingCat>())
        {
            if (other.transform.eulerAngles.y == 180)
                rotate = 0;
            else
                rotate = 180;
           
            other.transform.eulerAngles = new Vector3(other.transform.eulerAngles.x , rotate , other.transform.eulerAngles.z);
            FuckingCat cat = other.GetComponent<FuckingCat>();
            float defaultForce = cat.force; 
            cat.force = 0;
            other.GetComponent<AudioSource>().enabled = false;
            yield return new WaitForSeconds(UnityEngine.Random.Range(delay.x , delay.y));
            other.GetComponent<AudioSource>().enabled = true;
            cat.force = defaultForce;

        }
        else if(other.GetComponent<CatBall>())
        {
            ball.transform.position = childPos.transform.position;
        }

        
    }



}
