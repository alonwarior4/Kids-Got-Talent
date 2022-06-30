using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Layers
{
    Layer1,
    Fog
}
public class RepeatImage : MonoBehaviour
{
    //asset images
    [SerializeField] SavingBirdData data;

    //state of layer
    [SerializeField] Layers layerSprites;

    //array of spriterenderers in this layer 
    SpriteRenderer[] sprites;

    //move speed and target position
    [SerializeField] float moveSpeedPercent;
    [SerializeField] Vector3 targetPos;

    //first and last sprite index
    int lastOneIndex;
    int firstOnIndex;

    //lengh of the sprite
    float spriteWidth;    


    private void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();       
        lastOneIndex = sprites.Length - 1;
        firstOnIndex = 0;
    }

    void Update()
    {
        MoveALLObjects();
        CheckForRepeat();
    }

    private void MoveALLObjects()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].gameObject.transform.Translate(-moveSpeedPercent * S_ScriptManager.Instance.BaseSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void CheckForRepeat()
    {
        if (Vector2.Distance(sprites[firstOnIndex].gameObject.transform.position, targetPos) < 0.5f)
        {
            spriteWidth = sprites[lastOneIndex].bounds.size.x;

            sprites[firstOnIndex].gameObject.transform.position = new Vector3((sprites[lastOneIndex].gameObject.transform.position.x + spriteWidth), 
                sprites[firstOnIndex].gameObject.transform.position.y,
                sprites[firstOnIndex].gameObject.transform.position.z);
            ChangeSprite();
            ChangeFrontObjectIndex();
            ChangeLastObjectIndex();
        }
    }

    private void ChangeSprite()
    {
        List<Sprite> choosedSprites = SelectLayerImages();
        sprites[firstOnIndex].sprite = choosedSprites[UnityEngine.Random.Range(0, choosedSprites.Count)];
    }
   
    private void ChangeLastObjectIndex()
    {
        if(lastOneIndex < sprites.Length -1)
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

        if(firstOnIndex < sprites.Length - 1)
        {
            firstOnIndex++;
        }
        else
        {
            firstOnIndex = 0;
        }
    }

    private List<Sprite> SelectLayerImages()
    {
        switch (layerSprites)
        {
            case Layers.Layer1:
                return data.Layer1;                               
            case Layers.Fog:
                return data.Fog;                
            default:
                return null;                
        }
    }
}
