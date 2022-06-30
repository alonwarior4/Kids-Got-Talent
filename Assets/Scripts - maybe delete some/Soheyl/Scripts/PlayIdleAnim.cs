using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIdleAnim : MonoBehaviour
{
    Animator AnimatorRef;
    SheepPlayAnim sheep;
    MousePlayAnim mouse;
    AirPlanePlayAnim airPlane;
    TaxiPlayAnim Taxi;

    private void Awake()
    {
        AnimatorRef = GetComponent<Animator>();
        sheep = GetComponentInChildren<SheepPlayAnim>();
        mouse = GetComponentInChildren<MousePlayAnim>();
        airPlane = GetComponentInChildren<AirPlanePlayAnim>();
        Taxi = GetComponentInChildren<TaxiPlayAnim>();
    }

    public void SheepEndAnimation()
    {
        sheep.CallCoroutine();
    }

    public void MouseEndAnimation()
    {
        mouse.MoveToSmells();
    }

    public void AirPlaneMovement()
    {
        airPlane.PlayAirPlaneCoroutine();
    }

    public void TaxiMovement()
    {
        Taxi.TaxiMoveCoroutine();
    }

    private void StartIdleAnim(string TriggerName)
    {
        AnimatorRef.SetTrigger(TriggerName);
    }
}
