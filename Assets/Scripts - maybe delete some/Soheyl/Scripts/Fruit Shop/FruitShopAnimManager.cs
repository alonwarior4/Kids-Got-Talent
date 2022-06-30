using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayX = XPlayed , using one state just for GOSHADISM purposes.
/// </summary>
public enum EFruitShopState
{
    NO_Anim,
    PlayMango,
    PlayCarrot,
    PlayPear,
    PlayWatermelon,
    PlayBanana,
    PlayApple,
    PlayOrange,
    PlayCherry,
    PlayGrapes
}

public class FruitShopAnimManager : MonoBehaviour
{
    EFruitShopState CurrentState = EFruitShopState.NO_Anim;
    public EFruitShopState GetCurrentState() { return CurrentState; }
    public void SetCurrentState(EFruitShopState StateToSet) { CurrentState = StateToSet; }
}
