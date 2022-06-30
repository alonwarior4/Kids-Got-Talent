using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGraph : MonoBehaviour
{
    public Info[] Info;
}

[System.Serializable]
public class Info {
    [TextArea]
    public string txt ;
    public Child[] childs;

}

[System.Serializable]
public class Child {

    public string str;
}