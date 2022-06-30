using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoadingObjects : MonoBehaviour
{
    [SerializeField] Category[] Category;
    [SerializeField] TextMeshProUGUI dataImageName;
    [SerializeField] float textShowSpeed;
    List<SpriteRenderer> dataImages = new List<SpriteRenderer>();
    List<int> showedSprites = new List<int>();
    Image objectImage;

    //max sprite sizes
    float maxXsize = 5.25f;
    float maxYsize = 4.70f;

    //new sprite sizes
    float newXSize;
    float newySize;
    


    private void Awake()
    {        
        objectImage = GetComponent<Image>();        
    }

    private void Start()
    {
        Cursor.visible = false;
        dataImages.Clear();
        showedSprites.Clear();
        dataImages = Category[UnityEngine.Random.Range(0, Category.Length)].GetData();
    }

    public void SelectNewObject()
    {
        StartCoroutine(SelectCategoryImage());
    }

    IEnumerator SelectCategoryImage()
    {       
        int randomImageIndex;
        if (showedSprites.Count == dataImages.Count) { showedSprites.Clear(); }
        randomImageIndex = UnityEngine.Random.Range(0, dataImages.Count);

        while (showedSprites.Contains(randomImageIndex))
        {
            randomImageIndex = UnityEngine.Random.Range(0, dataImages.Count);
        }

        showedSprites.Add(randomImageIndex);
        objectImage.sprite = dataImages[randomImageIndex].sprite;
        SetImageSize();
        StartCoroutine(FadeInImage());
        StartCoroutine(ShowObjectName(dataImages[randomImageIndex].name));
        yield return null;
    }

    IEnumerator FadeInImage()
    {        
        for (float i = 0.01f; i < 0.5f; i+= Time.deltaTime)
        {
            objectImage.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), Mathf.Min(1, i / 0.5f));
            yield return null;
        }
    }

    private void SetImageSize()
    {
        objectImage.SetNativeSize();
        float xSize = objectImage.sprite.bounds.size.x;
        float ySize = objectImage.sprite.bounds.size.y;
        float imageAspect = xSize / ySize;
        newXSize = xSize;
        newySize = ySize;
        if (xSize > ySize)
        {
            while (maxXsize - newXSize >= Mathf.Epsilon)
            {
                newXSize = newXSize * 1.01f;
                newySize = newXSize / imageAspect;
                if(maxYsize - newySize <= Mathf.Epsilon)
                {
                    break;
                }
            }
            objectImage.GetComponent<RectTransform>().sizeDelta = new Vector2(newXSize * 100 * 0.75f, newySize * 100 * 0.75f);
        }
        else
        {
            while (maxYsize - newySize >= Mathf.Epsilon)
            {
                newySize = newySize * 1.01f;
                newXSize = newySize * imageAspect;
                if(maxXsize - newXSize <= Mathf.Epsilon)
                {
                    break;
                }
            }
        }
        objectImage.GetComponent<RectTransform>().sizeDelta = new Vector2(newXSize * 100 *  0.75f, newySize * 100 * 0.75f);
        objectImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    IEnumerator ShowObjectName(string objectName)
    {
        for(int index = 0 ; index <= objectName.Length ; index ++)
        {
            dataImageName.text = objectName.Substring(0, index);
            yield return new WaitForSeconds(textShowSpeed);
        }
    }
}
