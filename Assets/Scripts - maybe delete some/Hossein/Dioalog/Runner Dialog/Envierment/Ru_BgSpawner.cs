using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Ru_BgSpawner : MonoBehaviour
{

    public static Ru_BgSpawner instance;
    [SerializeField] private List<Ru_BgObject> listSpawn = new List<Ru_BgObject>();
    [SerializeField] private GameObject[] PrefabsObject = new GameObject[10];
    [SerializeField] private Vector3 PointSpawn;
    [SerializeField] private float Delay;
    [SerializeField] private GameObject BG_Kharazmi;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    
    void Start()
    {

    }


    public void Starting()
    {
        Spwaner();
    }
    

    void Update()
    {

    }


    public void AdderToList(Ru_BgObject Bg)
    {
        bool find = false;
        for (int i = 0; i < listSpawn.Count; i++)
        {
            if (listSpawn[i] == null)
            {
                listSpawn[i] = Bg;
                find = true;
                break;
            }
        }

        if (find)
        {
            listSpawn.Add(Bg);
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
        Ru_BgObject G = Instantiate(PrefabsObject[r], PointSpawn, Quaternion.identity).GetComponent<Ru_BgObject>();

        AdderToList(G);
    }


    public void SpawnKharazmi()
    {
        GameObject g = Instantiate(BG_Kharazmi, PointSpawn, Quaternion.identity) as GameObject;
        Ru_BgObject r_kh = g.GetComponent<Ru_BgObject>();
        AdderToList(r_kh);
        g.transform.name = "Bg_Kharazmi";
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
