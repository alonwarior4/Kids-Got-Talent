using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ru_SpawnerTrees : MonoBehaviour
{

    public static Ru_SpawnerTrees instance;
    public List<Ru_TreesObject> listSpawn = new List<Ru_TreesObject>();
    public GameObject[] PrefabsObject = new GameObject[10];
    public Vector3 PointSpawn;
    public float Delay;

    private void Awake()
    {

        if (!instance)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
    }


    public void Starting()
    {

        Spwaner();
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void AdderToList(Ru_TreesObject Tree)
    {
        bool find = false;
        for (int i = 0; i < listSpawn.Count; i++)
        {
            if (listSpawn[i] == null)
            {
                listSpawn[i] = Tree;
                find = true;
                break;
            }
        }

        if (find)
        {
            listSpawn.Add(Tree);
        }
    }

    public void Spwaner()
    {

        int RandomIndex = Random.Range(0, PrefabsObject.Length);

        StartCoroutine(GetTrapsObject(RandomIndex));

    }

    private IEnumerator GetTrapsObject(int r)
    {
        yield return new WaitForSeconds(Delay);
        Ru_TreesObject G = Instantiate(PrefabsObject[r], PointSpawn, Quaternion.identity).GetComponent<Ru_TreesObject>();

        AdderToList(G);
    }


    public void StopBObjects()
    {
        for (int i = 0; i < listSpawn.Count; i++)
        {
            if (listSpawn[i])
            {
                listSpawn[i].Speed = 0;
            }
        }

    }
}
