using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursorHiddenObjects : MonoBehaviour
{
    [SerializeField] Texture2D grabTexture;
    [SerializeField] Texture2D releaseTexture;

    private void Start()
    {
        Vector2 cursorHotspot = new Vector2(releaseTexture.width / 2, releaseTexture.height / 2);
        Cursor.SetCursor(releaseTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    public void SetCursorGrabTexture()
    {
        Vector2 hotSpot = new Vector2(grabTexture.width / 2, grabTexture.height / 2);
        Cursor.SetCursor(grabTexture, hotSpot, CursorMode.ForceSoftware);
    }

    public void SetCursorReleaseTexture()
    {
        Vector2 hotSpot = new Vector2(releaseTexture.width / 2, releaseTexture.height / 2);
        Cursor.SetCursor(releaseTexture, hotSpot, CursorMode.ForceSoftware);
    }


}
