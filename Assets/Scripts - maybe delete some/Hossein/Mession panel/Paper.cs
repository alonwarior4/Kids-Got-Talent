using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    [HideInInspector]
    public Animator PaperAnim;
    public bool IsPaperEnd;
    public static Paper instance;
    [SerializeField] Sprite fullPaper;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        PaperAnim = this.GetComponent<Animator>();
        GetComponent<SpriteRenderer>().sprite = fullPaper;
    }
	// Use this for initialization
	IEnumerator Start ()
    {       
        IsPaperEnd = false;       
        yield return new WaitForEndOfFrame();
        StartOfLevel(); 
    }

    public void EndOfLevel()
    {
        PaperAnim.SetTrigger("End");
    }

    public void StartOfLevel()
    {
        PaperAnim.SetTrigger("Start");
    }

    public void EndPaper()
    {
        IsPaperEnd = true;
    }


}
