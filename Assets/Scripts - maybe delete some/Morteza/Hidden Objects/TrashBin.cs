using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    int trashCounter = 0;
    SpriteRenderer spriteRenderer;   

    [Header("paper trash sprites")]
    [SerializeField] Sprite oneTrash;
    [SerializeField] Sprite TwoTrash;
    [SerializeField] Sprite ThreeTrash;
    [SerializeField] int hintIndex;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void addTrash()
    {
        trashCounter++;
    }

    private void Update()
    {
        if (trashCounter == 0) { return; }
        else if (trashCounter == 1)
        {
            spriteRenderer.sprite = oneTrash;
        }
        else if (trashCounter == 2)
        {
            spriteRenderer.sprite = TwoTrash;
        }
        else if (trashCounter == 3)
        {
            FirstPosition.counterObj += 1;
            CheckForFinish();
            spriteRenderer.sprite = ThreeTrash;
            trashCounter = 0;
            FindObjectOfType<Hint>().CheckAndSelectHint(hintIndex);
            FindObjectOfType<ObjectName>().changeText(hintIndex);
        }
    }


    private void CheckForFinish()
    {
        if (FirstPosition.counterObj >= 13)
        {
            StartEndStoryManager.instance.EndOfHiddenObj();
        }
    }
    

}
