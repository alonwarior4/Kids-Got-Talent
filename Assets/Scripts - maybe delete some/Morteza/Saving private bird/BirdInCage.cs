using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInCage : MonoBehaviour
{
    AudioSource audioSourceRef;

    [SerializeField] float moveSpeed;
    [SerializeField] Transform destination;
    [SerializeField] Rigidbody2D tahesh;
    [SerializeField] GameObject Bird;
    [SerializeField] AudioClip cageBreak_AC;

    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    private void Awake()
    {
        audioSourceRef = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D otheCollider)
    {
        if (otheCollider.CompareTag("Player"))
        {
            audioSourceRef.PlayOneShot(cageBreak_AC);
            StartCoroutine(FleeBird());
            AddBird.Instance.AddNumber();
        }      
    }

    IEnumerator FleeBird()
    {
        GetComponent<Animator>().SetTrigger("Bezade");
        tahesh.bodyType = RigidbodyType2D.Dynamic;
        tahesh.gravityScale = 2;
        while(Vector2.Distance(Bird.transform.position , destination.position) >= Mathf.Epsilon)
        {
            Bird. transform.position = Vector2.MoveTowards(Bird.transform.position, destination.position, moveSpeed * Time.deltaTime);
            yield return waitForEndOfFrame;
        }
    }

}
