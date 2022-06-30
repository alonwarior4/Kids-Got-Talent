using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    ChangeColor ChangeColorRef;
    [SerializeField] Texture2D grayPencilTexture;
    [SerializeField] GameObject penParent;
    [SerializeField] AudioClip penPickup;
    [SerializeField] Button eraser;

    public bool isHajiFiruzScene;

    List<Button> colorButtons = new List<Button>();
    

    private void Awake()
    {
        ChangeColorRef = GetComponent<ChangeColor>();
        if (isHajiFiruzScene)
        {
            //Cursor.SetCursor(ChangeColorRef.GetGrayTex(), Vector2.zero, CursorMode.ForceSoftware);
            CurrentColorName = "Gray";
        }
        //else
        //{
        //    Cursor.SetCursor(grayPencilTexture, Vector2.zero, CursorMode.ForceSoftware);
        //}
    }

    private void Start()
    {
        Button[] uiButtons = penParent.GetComponentsInChildren<Button>();
        for(int i=0; i< uiButtons.Length; i++)
        {
            colorButtons.Add(uiButtons[i]);
        }
        if (eraser)
        {
            colorButtons.Add(eraser);
        }
    }

    string CurrentColorName;
    public string GetCurrentColorName() { return CurrentColorName; }
    public void SetCurrentColorName(string ColorNameToSet)
    {
        Button thisPen = null;
        CurrentColorName = ColorNameToSet;     
        ChangeColorRef.CheckAndSetCursor(CurrentColorName);

        //Button[] uiPens = penParent.GetComponentsInChildren<Button>();
        foreach(Button pen in colorButtons)
        {
            pen.gameObject.GetComponent<Image>().color = Color.white;
            pen.interactable = true;
            if(pen.name == ColorNameToSet)
            {
                thisPen = pen;
            }
        }

        if (thisPen)
        {
            StartCoroutine(FadeOut(thisPen.gameObject.GetComponent<Image>()));
            AudioSource.PlayClipAtPoint(penPickup, Camera.main.transform.position, 1f);
            thisPen.interactable = false;
        }       
        //thisPen.gameObject.GetComponent<Image>().color = Color.clear;
        
    }

    IEnumerator FadeOut(Image penImage)
    {
        for (float i = 0.01f; i < 0.3f; i += Time.deltaTime)
        {
            penImage.color = Color.Lerp(Color.white, Color.clear, Mathf.Min(1, i / 0.3f));
            yield return null;
        }
    }
}
