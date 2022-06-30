using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
   
    [SerializeField] Texture2D cursorTexture;

    private void Start()
    {       
        Cursor.SetCursor(cursorTexture, Vector2.zero , CursorMode.ForceSoftware);
    }
    

}
