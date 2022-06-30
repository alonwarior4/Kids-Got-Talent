using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minirunner_BG : MonoBehaviour
{
    public static Minirunner_BG instance;
    
    //array of spriterenderers in this layer 
    [SerializeField] SpriteRenderer[] sprites;
    [SerializeField] SpriteRenderer kharazmi;

    //move speed and target position
    public float bgMoveSpeed;
    [SerializeField] float targetPosX;

    //first and last sprite index
    int lastOneIndex;
    int firstOnIndex;

    //lengh of the sprite
    float spriteWidth;

    //for running
    public bool HasToRun = false;

    //arrived to kharazmi
    public bool IsArrived;

    bool KharazmiInPosition = false;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        lastOneIndex = sprites.Length - 1;       
        firstOnIndex = 0;
    }

    void Update()
    {
        if (HasToRun)
        {
            MoveALLObjects();
            if (!IsArrived)
            {
                CheckForRepeat();
            }
            else
            {
                BringUpKharazmi();
            }
        }
    }

    private void MoveALLObjects()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].gameObject.transform.Translate(bgMoveSpeed * Time.deltaTime , 0 , 0 );
        }
    }

    private void CheckForRepeat()
    {
        var targetPos = new Vector3(targetPosX, sprites[firstOnIndex].transform.position.y, sprites[firstOnIndex].transform.position.z);

        if (Vector2.Distance(sprites[firstOnIndex].gameObject.transform.position, targetPos) < 0.5f)
        {
            spriteWidth = sprites[lastOneIndex].bounds.size.x;

            sprites[firstOnIndex].gameObject.transform.position = new Vector3((sprites[lastOneIndex].gameObject.transform.position.x - spriteWidth),
                sprites[firstOnIndex].gameObject.transform.position.y,
                sprites[firstOnIndex].gameObject.transform.position.z);

            ChangeFrontObjectIndex();
            ChangeLastObjectIndex();
        }
    }

    private void BringUpKharazmi()
    {
        
        if (!KharazmiInPosition)
        {
            float Kh_Width = kharazmi.bounds.size.x;

            kharazmi.gameObject.transform.position = new Vector3(sprites[lastOneIndex].gameObject.transform.position.x - Kh_Width,
                kharazmi.gameObject.transform.position.y,
                kharazmi.gameObject.transform.position.z);

            KharazmiInPosition = true;
        }
        else
        {
            kharazmi.gameObject.transform.Translate(bgMoveSpeed * Time.deltaTime, 0, 0);
        }       
    }



    private void ChangeLastObjectIndex()
    {
        if (lastOneIndex < sprites.Length - 1)
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

        if (firstOnIndex < sprites.Length - 1)
        {
            firstOnIndex++;
        }
        else
        {
            firstOnIndex = 0;
        }
    }

  
}

