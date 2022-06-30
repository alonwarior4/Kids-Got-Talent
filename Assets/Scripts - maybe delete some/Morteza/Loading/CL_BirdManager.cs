using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CL_BirdManager : MonoBehaviour
{


    [SerializeField] GameObject[] birds;
    [SerializeField] TextMeshProUGUI colorText;
    [SerializeField] Animator colorSikhAnimator;

    // bird speed config
    [SerializeField] float birdComeSpeed;
    [SerializeField] float birdGoSpeed;


    // bird wait time on sit position
    //[SerializeField] float birdWaitTime;


    //Birds Positions
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] Transform sitPos;


    // text color
    [SerializeField] Color brownColor;
    [SerializeField] Color whiteColor;

    //check if the loading finished
    bool isBirdGone = false;
    bool isTextFadesOut = false;
    bool isSikhGone = false;


    //cash for end of frame
    private WaitForEndOfFrame WaitToEndOfFrame = new WaitForEndOfFrame();


    private void Start()
    {
        StartCoroutine(ColoringBirdsManager());
    }

    private void Update()
    {
        if(isBirdGone && isSikhGone && isTextFadesOut)
        {
            LoadingManager.instance.LoadingIsFinished = true;
            isBirdGone = false;
        }
    }

    IEnumerator ColoringBirdsManager()
    {
        int birdIndex;      

        colorSikhAnimator.SetTrigger("Biomade");
        yield return WaitToEndOfFrame;
        yield return new WaitForSeconds(colorSikhAnimator.GetCurrentAnimatorStateInfo(0).length);

        birdIndex = UnityEngine.Random.Range(0, birds.Length);      
        GameObject choosedBird = Instantiate(birds[birdIndex], startPos.position, birds[birdIndex].transform.rotation) as GameObject;
        choosedBird.name = choosedBird.name.Replace("(Clone)", "");
        while (Vector2.Distance(choosedBird.transform.position, sitPos.position) >= Mathf.Epsilon)
        {
            choosedBird.transform.position = Vector2.MoveTowards(choosedBird.transform.position, sitPos.position, birdComeSpeed * Time.deltaTime);
            yield return WaitToEndOfFrame;
        }
        choosedBird.GetComponentInChildren<Animator>().SetTrigger("IsWireHitted");
        StartCoroutine(TypingBirdName(choosedBird.name));
        yield return WaitToEndOfFrame;
        yield return new WaitForSeconds(choosedBird.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length + 1f);
        choosedBird.GetComponentInChildren<Animator>().SetTrigger("FunPlay");
        yield return WaitToEndOfFrame;
        yield return new WaitForSeconds(choosedBird.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.1f);
        yield return WaitToEndOfFrame;
        choosedBird.GetComponentInChildren<Animator>().SetTrigger("Fly Up");
        choosedBird.GetComponentInChildren<FadeInBirdsLoading>().SetDistanceGo();
        if (choosedBird.name != "black")
        {
            while (Vector2.Distance(choosedBird.transform.position, endPos.position) >= Mathf.Epsilon)
            {
                choosedBird.transform.position = Vector2.MoveTowards(choosedBird.transform.position, endPos.position, birdGoSpeed * Time.deltaTime);
                yield return WaitToEndOfFrame;
            }
        }
        else
        {
            yield return WaitToEndOfFrame;
            yield return new WaitForSeconds(choosedBird.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
        Destroy(choosedBird);
        isBirdGone = true;
        colorSikhAnimator.SetTrigger("ColorSikhGo");
        yield return WaitToEndOfFrame;
        yield return new WaitForSeconds(colorSikhAnimator.GetCurrentAnimatorStateInfo(0).length);
        isSikhGone = true;
        StartCoroutine(FadeOutText(colorText));
    }

    IEnumerator TypingBirdName(string name)
    {
        colorText.color = SelectColor(name);       
        for (int i =0; i<= name.Length; i++)
        {
            colorText.text = name.Substring(0, i);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private Color32 SelectColor(string birdName)
    {       
        switch (birdName)
        {
            case "black":
                return Color.black;
            case "brown":
                return brownColor;
            case "blue":
                return Color.blue;
            case "white":
                return whiteColor;
            case "yellow":
                return Color.yellow;
            case "red":
                return Color.red;
            case "green":
                return Color.green;
            default:
                return Color.black;
        }
    }

    IEnumerator FadeOutText(TextMeshProUGUI colorTxt )
    {
        Color originalColor = colorTxt.color;
        for (float t = 0.01f; t < 0.5f; t += Time.deltaTime)
        {
            colorTxt.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / 0.5f));
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        isTextFadesOut = true;
    }



  




}
