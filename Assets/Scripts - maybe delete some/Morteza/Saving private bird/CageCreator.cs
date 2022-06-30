using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageCreator : MonoBehaviour
{
    [SerializeField] GameObject[] Cages;
    [SerializeField] GameObject parent;
    private void Awake()
    {
        foreach(Transform child in this.transform)
        {
            GameObject randomCage =  Instantiate(Cages[UnityEngine.Random.Range(0, Cages.Length)], child.position, transform.rotation) as GameObject;
            randomCage.transform.parent = parent.transform;           
        }
    }
}
