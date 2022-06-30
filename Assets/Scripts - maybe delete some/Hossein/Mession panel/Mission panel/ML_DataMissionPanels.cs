using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DataMissionScene", menuName = "CYRUS/DataMissions")]
public class ML_DataMissionPanels : ScriptableObject {

    public Data[] Data;
}

[System.Serializable]
public class Data
{
    public NamesScenes Scenename;
    public Sprite BeforeFinished;
    public Sprite AfterFinished;

}


