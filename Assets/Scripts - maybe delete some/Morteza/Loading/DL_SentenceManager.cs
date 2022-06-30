using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DL_SentenceManager : MonoBehaviour
{
    //sentences and places
    [SerializeField] Sentences[] sentences;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;

    //cloud animtors
    [SerializeField] Animator leftCloud;
    [SerializeField] Animator rightCloud;

    //writing animtion speed
    [SerializeField] float writingSpeed;

    //if sentences animation is finished
    bool isSentence1Finished = false;
    bool isSentence2Finished = false;
    bool isFadeOutFinished = false;

    //if loading has to be finished

    //cash for end of frame
    private WaitForEndOfFrame waitforendofframe = new WaitForEndOfFrame();


    private void Start()
    {       
        StartCoroutine(StartDialogue());
    }
  
    IEnumerator StartDialogue()
    {
        Sentences choosedOne = sentences[UnityEngine.Random.Range(0, sentences.Length)];
        yield return new WaitForSeconds(0.5f);

        leftCloud.SetTrigger("LeftBiomade");
        yield return waitforendofframe;
        yield return new WaitForSeconds(leftCloud.GetCurrentAnimatorStateInfo(0).length);
        rightCloud.SetTrigger("RightBiomade");
        StartCoroutine(WritingSentence1(choosedOne.sentence1, text1));
        yield return waitforendofframe;
        yield return new WaitForSeconds(rightCloud.GetCurrentAnimatorStateInfo(0).length);
        StartCoroutine(WritingSentence2(choosedOne.sentence2, text2));
        yield return new WaitForSeconds(2f);


        while (true)
        {
            if(isSentence1Finished && isSentence2Finished)
            {                
                leftCloud.SetTrigger("LeftGo");              
                yield return new WaitForSeconds(0.25f);
                StartCoroutine(FadeOutText());
                rightCloud.SetTrigger("RightGo");               
                yield return waitforendofframe;
                yield return new WaitForSeconds(rightCloud.GetCurrentAnimatorStateInfo(0).length + 0.2f);
                while (true)
                {
                    if(isSentence1Finished && isSentence2Finished && isFadeOutFinished)
                    {
                        LoadingManager.instance.LoadingIsFinished = true;
                        break;
                    }
                    yield return waitforendofframe;
                }
            }
            yield return waitforendofframe;
        }
    }

    IEnumerator WritingSentence1(string sentence , TextMeshProUGUI textPlace)
    {
        for(int i=0; i<= sentence.Length; i++)
        {
            textPlace.text = sentence.Substring(0, i);
            yield return new WaitForSeconds(writingSpeed);
        }
        isSentence1Finished = true;
    }

    IEnumerator WritingSentence2(string sentence , TextMeshProUGUI textPlace)
    {
        for(int i=0; i<= sentence.Length; i++)
        {
            textPlace.text = sentence.Substring(0, i);
            yield return new WaitForSeconds(writingSpeed);
        }
        yield return new WaitForSeconds(0.5f);
        isSentence2Finished = true;
    }

    IEnumerator FadeOutText()
    {
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
        foreach(TextMeshProUGUI text in texts)
        {
            Color originalColor = text.color;
            for (float t = 0.01f; t < 0.5f; t += Time.deltaTime)
            {
                text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / 0.5f));
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.2f);
        isFadeOutFinished = true;
    }

}

[System.Serializable]
public class Sentences
{
    public string sentence1;
    public string sentence2;
}
