using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

    [SerializeField] OtherUIManagement uiManager;
    [SerializeField] Animator teacherAnimator;

	public List<SpriteRenderer> ListSprite = new List<SpriteRenderer>();

	public Color C_in;
	public Color C_out;
    public float smooth;
    private float transitiontime = 1.0f;
    // Use this for initialization
	IEnumerator Start()
	{
        yield return new WaitUntil(() => MovingTeacher.instance.IsReccive);
        uiManager.StartStory();
        yield return new WaitForSeconds(1.75f);
        teacherAnimator.SetInteger("State", 8);
        yield return new WaitForEndOfFrame();
        teacherAnimator.SetInteger("State", -1);
        yield return new WaitWhile(()=> uiManager.isEndOfTyping == false);
        yield return new WaitForSeconds(1.75f);
        Camera.main.GetComponent<Animator>().SetTrigger("Zoom");
        yield return new WaitForSeconds(1f);
        Setinit();
	}

	// Update is called once per frame
	void Update () {

      
    }


	private void Setinit()
	{
		for (int i = 0; i < this.transform.GetChild(2).childCount ; i++)
		{	
			ListSprite.Add(this.transform.GetChild(2).GetChild(i).GetComponent<SpriteRenderer>());
		}
		ListSprite.Add(this.transform.GetChild(0).GetComponent<SpriteRenderer>());
		
        //isset = true;
        StartCoroutine(LerpColor(C_in));

    }


    public void FadeOut()
    {
        StartCoroutine(LerpColor(C_out));
    }

    private IEnumerator LerpColor(Color source)
    {

        float temptime = 0.0f;

        while (temptime<=transitiontime)
        {
            temptime += Time.deltaTime;

            foreach (SpriteRenderer sp in ListSprite)
                sp.color = Color.Lerp(sp.color, source, Mathf.Clamp(temptime / transitiontime, 0.0f, 1f));
            yield return new WaitForSeconds(smooth);
        }
    }
    public void EndOfstory()
    {
        StartCoroutine(EndOfBodyParts());
    }

    public IEnumerator EndOfBodyParts()
    {
        OtherUIManagement uiManager = FindObjectOfType<OtherUIManagement>();       
        uiManager.TellFinalStory();
        uiManager.isEndOfScene = true;
        yield return new WaitForSeconds(3.75f);
        teacherAnimator.SetInteger("State", 9);
        yield return new WaitForEndOfFrame();
        teacherAnimator.SetInteger("State", -1);
    }

}
