using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveParticle : MonoBehaviour
{

    ParticleSystem leaveParticle;

    float baseSpeed = 2;
    float baseForce = 2;
    float baseGravity = 2;
    float baseEmission = 2;
    float minRate = 0.8f;
    float maxRate = 2.1f;

    ParticleSystem.EmissionModule emissionMudule;

    float forceOffset = 0;

    private void Start()
    {
        leaveParticle = GetComponent<ParticleSystem>();
        emissionMudule = leaveParticle.emission;
    }

    private void Update()
    {
        SetParticelStartSpeed();
        SetGravityModifire();
        SetEmission();
        SetForce();           
    }

    private void SetParticelStartSpeed()
    {
        float currentSpeed;
        float diffrence;
        

        currentSpeed = S_ScriptManager.Instance.BaseSpeed;
        diffrence = currentSpeed - baseSpeed;
        baseSpeed = currentSpeed;

        float startSpeed = Mathf.Max(-7, leaveParticle.startSpeed - (diffrence / 2));
        leaveParticle.startSpeed = startSpeed ;
    }

    private void SetForce()
    {
        float currentForce;
        float difference;
        

        currentForce = S_ScriptManager.Instance.BaseSpeed;
        difference = currentForce - baseForce;
        baseForce = currentForce;

        forceOffset = Mathf.Max( -5 , forceOffset - (difference / (8 / 5f)));
        var particleForce = leaveParticle.forceOverLifetime;
        particleForce.x = forceOffset;
    }


    private void SetGravityModifire()
    {
        float currentSpeed;
        float diffrence;

        currentSpeed = S_ScriptManager.Instance.BaseSpeed;
        diffrence = currentSpeed - baseGravity;
        baseGravity = currentSpeed;

        float gravity =  Mathf.Min(leaveParticle.gravityModifier + (diffrence / (8 / 0.5f)), 0.5f);
        leaveParticle.gravityModifier  = gravity;
    }

    private void SetEmission()
    {
        float currentSpeed;
        float diffrence;


        currentSpeed = S_ScriptManager.Instance.BaseSpeed;
        diffrence = currentSpeed - baseEmission;
        baseEmission = currentSpeed;


        minRate = Mathf.Min (minRate + (diffrence / (8 / 1.7f)) , 2.5f);
        maxRate = Mathf.Min( maxRate + (diffrence / (8 / 2.4f)) , 4.5f);

        emissionMudule.rateOverTime = new ParticleSystem.MinMaxCurve(minRate , maxRate);
        
        

    }
}
