using UnityEngine;
 
public class ChangeColor : MonoBehaviour
{
    [SerializeField] PencilSound pencilSound;
    AudioSource audioSourceRef;

    #region TexturesRef
    [Header("Icon Refrences")]
    [SerializeField] Texture2D YellowTex;
    [SerializeField] Texture2D GreenTex;
    [SerializeField] Texture2D OrangeTex;
    [SerializeField] Texture2D RedTex;
    [SerializeField] Texture2D BlackTex;
    [SerializeField] Texture2D PinkTex;
    [SerializeField] Texture2D PurpleTex;
    [SerializeField] Texture2D BrownTex;
    [SerializeField] Texture2D GrayTex;
    [SerializeField] Texture2D BlueTex;
    [SerializeField] Texture2D DarkRedTex;
    [SerializeField] Texture2D LightBlueTex;
    [SerializeField] Texture2D EraserTex;
    [SerializeField] Texture2D DarkBlueTex;

    //if its eraser
    public bool isEraser = false;

    #endregion

    public Texture2D GetGrayTex()
    {
        return GrayTex;
    }
    private void Awake()
    {
        audioSourceRef = GetComponent<AudioSource>();
    }

    public void CheckAndSetCursor(string PickedColorName)
    {
        switch(PickedColorName)
        {
            case "Yellow":
                isEraser = false;
                ChangeCursor(YellowTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));
                break;
            case "Green":
                isEraser = false;
                ChangeCursor(GreenTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Orange":
                isEraser = false;
                ChangeCursor(OrangeTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Red":
                isEraser = false;
                ChangeCursor(RedTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Black":
                isEraser = false;
                ChangeCursor(BlackTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Pink":
                isEraser = false;
                ChangeCursor(PinkTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Purple":
                isEraser = false;
                ChangeCursor(PurpleTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Brown":
                isEraser = false;
                ChangeCursor(BrownTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Gray":
                isEraser = false;
                ChangeCursor(GrayTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Blue":
                isEraser = false;
                ChangeCursor(BlueTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "LightBlue":
                isEraser = false;
                ChangeCursor(LightBlueTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "DarkRed":
                isEraser = false;
                ChangeCursor(DarkRedTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "DarkBlue":
                isEraser = false;
                ChangeCursor(DarkBlueTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));

                break;
            case "Eraser":
                isEraser = true;
                ChangeCursor(EraserTex);
                audioSourceRef.PlayOneShot(pencilSound.Sound(PickedColorName));
                break;
        }
    }

    void ChangeCursor(Texture2D TextureToSet)
    {
        if (isEraser)
        {
            //TODO : make eraser bigger to understand its selected
            //Vector2 eraserHotspot = new Vector2(TextureToSet.width / 7, 0);
            //Cursor.SetCursor(TextureToSet, eraserHotspot, CursorMode.ForceSoftware);
        }
        //else
        //{
        //    //Cursor.SetCursor(TextureToSet, Vector2.zero, CursorMode.ForceSoftware);
        //}      
    }
}
