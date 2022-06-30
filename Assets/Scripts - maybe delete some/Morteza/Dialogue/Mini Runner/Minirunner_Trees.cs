using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minirunner_Trees : MonoBehaviour
{







    //array of spriterenderers in this layer 
    [SerializeField] SpriteRenderer[] trees;
    [SerializeField] Sprite[] treesSprites;

    //move speed and target position
    float moveSpeed;
    [SerializeField] Vector3 targetPos;

    //first and last sprite index
    int lastOneIndex;
    int firstOnIndex;

    //tree distances
    [SerializeField] float treeDistance;


    private void Start()
    {
        moveSpeed = Minirunner_BG.instance.bgMoveSpeed;
        lastOneIndex = trees.Length - 1;
        firstOnIndex = 0;
    }

    void Update()
    {
        if (Minirunner_BG.instance.HasToRun)
        {
            MoveALLObjects();
            CheckForRepeat();
        }
    }

    private void MoveALLObjects()
    {
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].gameObject.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void CheckForRepeat()
    {
        if (Vector2.Distance(trees[firstOnIndex].gameObject.transform.position, targetPos) < 0.5f)
        {
            trees[firstOnIndex].gameObject.transform.position = new Vector3((trees[lastOneIndex].gameObject.transform.position.x - treeDistance),
                trees[firstOnIndex].gameObject.transform.position.y,
                trees[firstOnIndex].gameObject.transform.position.z);
            ChangeSprite();
            ChangeFrontObjectIndex();
            ChangeLastObjectIndex();
        }
    }

    private void ChangeSprite()
    {
        trees[firstOnIndex].sprite = treesSprites[UnityEngine.Random.Range(0, treesSprites.Length)];
    }

    private void ChangeLastObjectIndex()
    {
        if (lastOneIndex < trees.Length - 1)
        {
            lastOneIndex++;
        }
        else
        {
            lastOneIndex = 0;
        }
    }

    private void ChangeFrontObjectIndex()
    {

        if (firstOnIndex < trees.Length - 1)
        {
            firstOnIndex++;
        }
        else
        {
            firstOnIndex = 0;
        }
    }

  
}

