using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBallIdle : MonoBehaviour
{
    [SerializeField] TaneAnimPlay TaneRef;
    [SerializeField] BargPlayAnim BargRef;
    
    public void CallTaneSetState()
    {
        TaneRef.SetTreeAnimatorState();
    }
    public void CallBargSetState()
    {
        BargRef.SetTreeAnimatorState();
    }
}
