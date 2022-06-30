using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questions", menuName = "CYRUS/Questions" )]
public class DataQuestion : ScriptableObject
{
	public int RepeatLevel;
    public int Level;
    public CorrectOrNo[] DataSpawn;
        
}


[System.Serializable]
public class Animal_data {
    public string TypeAnimal;
    public int NumberTypeObject;
    public int MinCount;
    public int MaxCount;
}

[System.Serializable]
public class CorrectOrNo {
    [Header("True Or No")]
    public string TypeSpawn;
    [Header("Animals Data")]
    public Animal_data[] Data_numbers;
}