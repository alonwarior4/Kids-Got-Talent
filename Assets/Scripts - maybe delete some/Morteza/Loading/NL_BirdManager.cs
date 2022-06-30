using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NL_BirdManager : MonoBehaviour
{
    // different kind of birds
    [SerializeField] GameObject[] birds;

    // positions start(where to come from) , sit(where to sit and wait) , end(where to go)
    [SerializeField] Transform[] startPos;
    [SerializeField] Transform[] sitPos;
    [SerializeField] Transform[] endPos;

    // configs for speed of birds
    [SerializeField] float comeSpeed;
    [SerializeField] Vector2 sitDurationOffset;
    [SerializeField] float goSpeed;

    [SerializeField] Animator numberSikhAnimator;

    List<GameObject> birdsChoosed = new List<GameObject>();

    //text for numbers
    [SerializeField] TextMeshProUGUI numberText;

    //refrence for colors
    [SerializeField] Color brownColor;
    [SerializeField] Color whiteColor;

    //bools for check to end
    bool isBirdsGone = false;
    bool isSikhGone = false;
    bool isNumberTextFadesOut = false;

    //check for end of loading

    //cashed wait for end of frame
    private WaitForEndOfFrame waitUntilEndOfFrame = new WaitForEndOfFrame();

    // mission select manager
    public int missionNumber = 1;


    private void Start()
    {
        missionNumber = LoadingManager.instance.MissionNumber;
        StartCoroutine(BirdComingManager());
    }

    private void Update()
    {
        if(isBirdsGone && isSikhGone && isNumberTextFadesOut)
        {
           LoadingManager.instance.LoadingIsFinished = true;
            isBirdsGone = false;
        }
    }


    IEnumerator BirdComingManager()
    {             
        int birdIndex = UnityEngine.Random.Range(0, birds.Length);
        numberSikhAnimator.SetTrigger("Biomade");
        for(int i = 1 ; i<= missionNumber; i++)
        {           
            GameObject randomBird = Instantiate(birds[birdIndex],
                startPos[UnityEngine.Random.Range(0, startPos.Length)].position , 
                birds[birdIndex].transform.rotation) as GameObject;
            birdsChoosed.Add(randomBird);
            randomBird.name = randomBird.name.Replace("(Clone)", "");
        }
        for(int i = 0; i < birdsChoosed.Count; i++)
        {
            StartCoroutine(birdToCome(i));
        }
        numberText.color = SelectColor(birds[birdIndex].name);
        numberText.text = missionNumber.ToString();
        yield return null;
    }


    IEnumerator birdToCome(int index)
    {
        while (Vector2.Distance(birdsChoosed[index].transform.position, sitPos[index].transform.position) >= Mathf.Epsilon)
        {
            birdsChoosed[index].transform.position = Vector2.MoveTowards(birdsChoosed[index].transform.position,
                sitPos[index].transform.position, comeSpeed * Time.deltaTime);
            yield return waitUntilEndOfFrame;
        }
        birdsChoosed[index].GetComponentInChildren<Animator>().SetTrigger("IsWireHitted");        
        yield return new WaitForSeconds(UnityEngine.Random.Range(sitDurationOffset.x , sitDurationOffset.y));
        birdsChoosed[index].GetComponentInChildren<Animator>().SetTrigger("FunPlay");
        yield return waitUntilEndOfFrame;
        yield return new WaitForSeconds(birdsChoosed[index].GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.1f);
        birdsChoosed[index].GetComponentInChildren<Animator>().SetTrigger("Fly Up");
        birdsChoosed[index].GetComponentInChildren<FadeInBirdsLoading>().SetDistanceGo();
        if (birdsChoosed[index].name != "black")
        {
            Transform goPosition = endPos[UnityEngine.Random.Range(0, endPos.Length)];
            while (Vector2.Distance(birdsChoosed[index].transform.position, goPosition.position) >= Mathf.Epsilon)
            {
                birdsChoosed[index].transform.position = Vector2.MoveTowards(birdsChoosed[index].transform.position,
                    goPosition.position, goSpeed * Time.deltaTime);
                yield return waitUntilEndOfFrame;
            }
        }
        else
        {
            yield return waitUntilEndOfFrame;
            yield return new WaitForSeconds(birdsChoosed[index].GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
        Destroy(birdsChoosed[index]);
        isBirdsGone = true;
        numberSikhAnimator.SetTrigger("NumberSikhGo");
        yield return waitUntilEndOfFrame;
        yield return new WaitForSeconds(numberSikhAnimator.GetCurrentAnimatorStateInfo(0).length + 0.25f);
        isSikhGone = true;
        StartCoroutine(FadeOutText(numberText));
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

    IEnumerator FadeOutText(TextMeshProUGUI colorTxt)
    {
        Color originalColor = colorTxt.color;
        for (float t = 0.01f; t < 0.5f; t += Time.deltaTime)
        {
            colorTxt.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / 0.5f));
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        isNumberTextFadesOut = true;
    }


}
