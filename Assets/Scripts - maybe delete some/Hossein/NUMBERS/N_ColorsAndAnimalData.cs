using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName  = "Colors and Animals data"  , menuName ="CYRUS/Colors and animals" )]

public class N_ColorsAndAnimalData : ScriptableObject{

    public Colorsdata[] colorsdataBirds;
    public Colorsdata[] colorsdataSheeps;
    [Space]
    public AudioClip and;
    public AudioClip Birds;
    public AudioClip Sheeps;
    public AudioClip bird;
    public AudioClip sheep;

    public AudioClip GetColorBirds(int index)
    {
//        Debug.Log(index);
        AudioClip c = null;
        for (int i = 0; i < colorsdataBirds.Length; i++)
        {
            if (colorsdataBirds[i].indexColor == index)
            {
                c = colorsdataBirds[i].Clip;
                break;
            }
        }
        return c;
    }

    public AudioClip GetColorSheeps(int index)
    {
        AudioClip c = null;
        for (int i = 0; i < colorsdataSheeps.Length; i++)
        {
            if (colorsdataSheeps[i].indexColor == index)
            {
                c = colorsdataSheeps[i].Clip;
                break;
            }
        }
        return c;
    }
}

[System.Serializable]
public class Colorsdata
{
    public int indexColor;
    public AudioClip Clip;
}