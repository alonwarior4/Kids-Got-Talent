using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EJungleAnimatorState
{
    No_State,

    LionColoringPlayed,
    

    CatColoringPlayed,
    

    DogColoringPlayed,
    

    SheepColoringPlayed,
    

    RabbitColoringPlayed,
    

    CorocodileColoringPlayed,
}

public class JungleAnimManager: MonoBehaviour
{
    EJungleAnimatorState CurrentAnim;
    private void Awake()
    {
        CurrentAnim = EJungleAnimatorState.No_State;
    }

    public EJungleAnimatorState GetCurrentAnim() { return CurrentAnim; }
    public void SetCurrentAnim(EJungleAnimatorState EAnimToSet) { CurrentAnim = EAnimToSet; }

}
