using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LayerEnum
{
    Middle,
    Back
}

public class RepeatLayer : MonoBehaviour
{
    //enum obj
    [SerializeField] LayerEnum layers;

    [SerializeField] Vector3 target;

    [SerializeField] float moveSpeedPercent;

    //scene layer objects
    GameObject[] SceneGameObjects = new GameObject[3];

    //gameobjects that are not selected
    List<GameObject> Remindes = new List<GameObject>();

    //objects to use for repeat
    GameObject[] objects;

    //objects that choosed to move
    List<GameObject> choosedObjects = new List<GameObject>();

    //pos they stay till their turn
    Vector3 StaticPos = new Vector3(10, 10, 0);

    float objectSpriteWidth;

    int lastOneIndex;
    int firstOnIndex;


    private void Awake()
    {
        lastOneIndex = SceneGameObjects.Length - 1;
        firstOnIndex = 0;
        SetObjects();        
    }

    private void SetObjects()
    {
        GameObject[] middleObjects = GameObject.FindGameObjectsWithTag("Middle");
        GameObject[] backObjects = GameObject.FindGameObjectsWithTag("Back");

        switch (layers)
        {
            case LayerEnum.Middle:
                objects = middleObjects;
                break;
            case LayerEnum.Back:
                objects = backObjects;
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        PlaceObjects();
    }

    private void PlaceObjects()
    {
        for (int i = 0; i < SceneGameObjects.Length; i++)
        {
            SceneGameObjects[i] = objects[UnityEngine.Random.Range(0, objects.Length)];
            while (choosedObjects.Contains(SceneGameObjects[i]))
            {
                SceneGameObjects[i] = objects[UnityEngine.Random.Range(0, objects.Length)];
            }
            choosedObjects.Add(SceneGameObjects[i]);
            if (layers == LayerEnum.Back)
            {
                switch (i)
                {
                    case 0:
                        SceneGameObjects[i].transform.position = new Vector3(-4.8f, -2.7f, 0);
                        break;
                    case 1:
                        SceneGameObjects[i].transform.position = new Vector3(4.8f, -2.7f, 0);
                        break;
                    case 2:
                        SceneGameObjects[i].transform.position = new Vector3(14.4f, -2.7f, 0);
                        break;
                    //case 3:
                    //    SceneGameObjects[i].transform.position = new Vector3(24f, -2.7f, 0);
                    //    break;
                    //case 4:
                    //    SceneGameObjects[i].transform.position = new Vector3(33.6f, -2.7f, 0);
                    //    break;

                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        SceneGameObjects[i].transform.position = new Vector3(-3.6f, -2.7f, 0);
                        break;
                    case 1:
                        SceneGameObjects[i].transform.position = new Vector3(3.6f, -2.7f, 0);
                        break;
                    case 2:
                        SceneGameObjects[i].transform.position = new Vector3(10.8f, -2.7f, 0);
                        break;
                    //case 3:
                    //    SceneGameObjects[i].transform.position = new Vector3(18f, -2.7f, 0);
                    //    break;
                    //case 4:
                    //    SceneGameObjects[i].transform.position = new Vector3(25.2f, -2.7f, 0);
                    //    break;
                }
            }
        }
        Remindes.Clear();
        foreach (GameObject obj in objects)
        {
            if (choosedObjects.Contains(obj))
            {
                continue;
            }
            else
            {
                Remindes.Add(obj);
            }
        }
    }

    private void Update()
    {
        CheckForReplace();
        MoveAllObjects();        
    }

    private void MoveAllObjects()
    {
        foreach (GameObject obj in SceneGameObjects)
        {
            obj.transform.Translate(-moveSpeedPercent * S_ScriptManager.Instance.BaseSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void CheckForReplace()
    {
        if (Vector2.Distance(SceneGameObjects[firstOnIndex].transform.position, target) < 0.5f)
        {
            objectSpriteWidth = SceneGameObjects[firstOnIndex].GetComponent<SpriteRenderer>().bounds.size.x;

            SceneGameObjects[firstOnIndex].gameObject.transform.position = new Vector3((SceneGameObjects[lastOneIndex].transform.position.x + objectSpriteWidth),
                SceneGameObjects[lastOneIndex].transform.position.y,
                SceneGameObjects[lastOneIndex].transform.position.z);

            ChangeLayerObject();
            ChangeFrontObjectIndex();
            ChangeLastObjectIndex();
        }
    }

    private void ChangeLastObjectIndex()
    {
        if (lastOneIndex < SceneGameObjects.Length - 1)
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

        if (firstOnIndex < SceneGameObjects.Length - 1)
        {
            firstOnIndex++;
        }
        else
        {
            firstOnIndex = 0;
        }
    }

    private void ChangeLayerObject()
    {
        GameObject oldOne = SceneGameObjects[firstOnIndex];
        oldOne.transform.position = StaticPos;

        GameObject newOne = Remindes[UnityEngine.Random.Range(0, Remindes.Count)];
        SceneGameObjects[firstOnIndex] = newOne;

        choosedObjects.Add(newOne);
        choosedObjects.Remove(oldOne);        
        Remindes.Add(oldOne);
        Remindes.Remove(newOne);
        
        
        SceneGameObjects[firstOnIndex].gameObject.transform.position = new Vector3((SceneGameObjects[lastOneIndex].transform.position.x + objectSpriteWidth),
                SceneGameObjects[lastOneIndex].transform.position.y,
                SceneGameObjects[lastOneIndex].transform.position.z);
    }




}
