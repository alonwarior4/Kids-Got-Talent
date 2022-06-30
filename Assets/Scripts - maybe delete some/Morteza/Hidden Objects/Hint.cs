using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hint : MonoBehaviour
{

    Animator hintAnimator;


    List<string> hintsNames = new List<string>();
    List<int> usedHints = new List<int>();


    int randomHint;
    int currentHintAnimationIndex;
    bool needtoSelectNewHint;
    bool cooldownHasFinished = true;

    [SerializeField] float HintCooldown;



    private void Awake()
    {
        setList();
        hintAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHintAnimationIndex = UnityEngine.Random.Range(0, hintsNames.Count);
    }

    private void setList()
    {
        hintsNames.Add("Watch");
        hintsNames.Add("Picture");
        hintsNames.Add("Reading Lamp");
        hintsNames.Add("Pants");
        hintsNames.Add("Shirt");
        hintsNames.Add("Blanket");
        hintsNames.Add("Trash");
        hintsNames.Add("Blocks");
        hintsNames.Add("Chair");
        hintsNames.Add("Flower Pot");
        hintsNames.Add("Teddy Bear");
        hintsNames.Add("Pillow");
        hintsNames.Add("Book");
    }

    public void CheckAndSelectHint(int deletedHint)
    {
        usedHints.Add(deletedHint);
        if (usedHints.Count == 13) { return; }
        if (deletedHint == currentHintAnimationIndex)
        {
            needtoSelectNewHint = true;
            while (needtoSelectNewHint)
            {
                randomHint = UnityEngine.Random.Range(0, hintsNames.Count);
                if (usedHints.Contains(randomHint))
                {
                    continue;
                }
                else
                {
                    currentHintAnimationIndex = randomHint;
                    needtoSelectNewHint = false;
                }
            }
        }
    }


    public void playCurrentHint()
    {        
        if(cooldownHasFinished)
        {
            StartCoroutine(CoolDown());
        }       
    }

    IEnumerator CoolDown()
    {
        hintAnimator.SetTrigger(hintsNames[currentHintAnimationIndex]);
        cooldownHasFinished = false;
        yield return new WaitForSecondsRealtime(HintCooldown);
        cooldownHasFinished = true;
    }
   
}
