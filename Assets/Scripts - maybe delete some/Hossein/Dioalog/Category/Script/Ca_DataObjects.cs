using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InfoObjects",menuName = "CYRUS/Create Object Category ")]
public class Ca_DataObjects : ScriptableObject
{
    public Ca_TypesObjectsEnum ObjectsType;
    public Objects[] Objects = new Objects[10] ;
  
}

[System.Serializable]
public class Objects
{
    public string Name;
    public Sprite Sp;
    public AudioClip nameSound;
    public bool Choosed;
}