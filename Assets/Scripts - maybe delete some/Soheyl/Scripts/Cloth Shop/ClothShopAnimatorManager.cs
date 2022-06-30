using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// PlayX = XPlayed => Playing mean Played. two state doesn't add just for Goshadism purposes.
/// </summary>
public enum EClothShopState
{

    //TODO Add Idle Played States and Check them in AnimPlayer Switch.
    No_Anim,
    PlayBag,
    PlayDress,
    PlayShirt,
    PlayGlass,
    PlayHat,
    PlayCap,
    PlayCoat,
    PlayPants,
    PlayShoe
}
public class ClothShopAnimatorManager : MonoBehaviour
{
    EClothShopState CurrentState = EClothShopState.No_Anim;
    public EClothShopState GetCurrentState() { return CurrentState; }
    public void SetCurrentState(EClothShopState StateToSet) { CurrentState = StateToSet; }
}
