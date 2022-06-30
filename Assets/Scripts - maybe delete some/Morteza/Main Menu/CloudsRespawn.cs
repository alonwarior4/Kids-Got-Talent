using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsRespawn : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    const string PARENT_NAME = "Clouds";
    List<float> YPos = new List<float>();
    float cloudYPos = 2.2f;
    GameObject parent;
    [SerializeField] float spawnDelay;
    [SerializeField] float firstDelay;


    private void Awake()
    {
        parent = GameObject.Find(PARENT_NAME);
        SetYPosList();
    }

    private void Start()
    {
        StartCoroutine(CloudSpawning());
    }

    IEnumerator CloudSpawning()
    {
        yield return new WaitForSeconds(firstDelay);
        while (true)
        {
            int cloudIndex = UnityEngine.Random.Range(0, clouds.Length);
            GameObject cloud = Instantiate(clouds[cloudIndex], ChoosePos(), transform.rotation) as GameObject;
            cloud.transform.parent = parent.transform;
            float newScale = UnityEngine.Random.Range(0.75f, 1f);
            cloud.transform.localScale = new Vector3(newScale , newScale, 0.5f);            
            yield return new WaitForSeconds(spawnDelay);
        }     
    }

    private Vector3 ChoosePos()
    {       
        int randomYPosIndex = UnityEngine.Random.Range(0, YPos.Count);
        while (YPos[randomYPosIndex] == cloudYPos)
        {
            randomYPosIndex = UnityEngine.Random.Range(0, YPos.Count);
        }
        cloudYPos = YPos[ randomYPosIndex];
        return new Vector3(-6.5f , cloudYPos , 0);                 
    }

    private void SetYPosList()
    {
        YPos.Add(1.3f);
        YPos.Add(1.8f);
        YPos.Add(2.2f);
    }
}
