using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EKharazamiAnimatorState
{
    NO_Anim,
    TanePlayed,
    BargPlayed,
    TreeCompeleted,
    BallPlayed,
    TaxiPlayed,
    KyteCompeleted,
    FlagCompeleted,
    AirPlanePlayed,
    KharazmiCompleted
}

public class KharazmiAnimManager : MonoBehaviour
{
    EKharazamiAnimatorState CurrentState = EKharazamiAnimatorState.NO_Anim;
    public void SetCurrentState(EKharazamiAnimatorState AnimatorToSet)
    {
        CurrentState = AnimatorToSet;
    }
    public EKharazamiAnimatorState GetCurrentState()
    {
        return CurrentState;
    }
}
