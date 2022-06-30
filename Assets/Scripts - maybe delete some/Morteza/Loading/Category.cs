using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Category")]
public class Category : ScriptableObject
{

    [SerializeField] GameObject dataPrefab;   
    SpriteRenderer[] dataSprites;
    



    public List<SpriteRenderer> GetData()
    {
        List<SpriteRenderer> dataList = new List<SpriteRenderer>();
        dataList.Clear();
        dataSprites = dataPrefab.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer dataSprite in dataSprites)
        {
            dataList.Add(dataSprite);            
        }
        return dataList;     
    }
	
}
