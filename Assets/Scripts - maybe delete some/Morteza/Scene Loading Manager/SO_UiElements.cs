using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mobile Ui Elements")]
public class SO_UiElements : ScriptableObject
{
    public UiImages[] ColorUiImages;
    public UiImages[] NumberUiImages;
    public UiImages[] DialogueUiImages;

    public string lastColoringMission = "ColorKharazmi";
    public string lastDialogueMission = "Class";

}


[System.Serializable]
public class UiImages
{
    public Sprite activeSprite;
    public Sprite inActiveSprite;
}
