using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Load Data")]
public class LoadData : ScriptableObject
{
 
    public AsyncOperation LoadMainMenu;
    public AsyncOperation LoadLevelSelecing;
    public string TargetMission;

}
