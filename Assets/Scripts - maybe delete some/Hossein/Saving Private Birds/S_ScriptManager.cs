using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

public class S_ScriptManager : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI ScoreText;
    //string scoreString = "Score : ";
    //float timePassed;

    public static S_ScriptManager Instance;
	public S_Spawning SC_Spawn;
	public S_hemletController Player;
	public float SlopeIncreaseExp;
	public float Speed_Static;
	public float FlyAnimSpeedRate;

    public float BaseSpeed;
    public bool ISdeath;
    private float increase;
    private void Awake()
    {
        if (!Instance )
        {
            Instance = this;
        }
    }
 
	void Start ()
    {
        BaseSpeed = Speed_Static;
        ISdeath = false;
        //ScoreText.text = scoreString + "0";
	}

    private void Update()
    {
        //ScoreManager();
        if (ISdeath == false)
        {
            increase = SlopeIncreaseExp * Time.timeSinceLevelLoad;
            BaseSpeed = increase + Speed_Static;
        }

    }

    public void NextSpawn()
	{
		SC_Spawn.Spawner();
	}

	public void IncreaseSpeed( )
	{
		GameObject[] Obj = GameObject.FindGameObjectsWithTag("Obstacle");


		
		for (int i = 0; i <Obj.Length; i++)
		{
			Obj[i].GetComponent<Moving>().Speed = increase +Speed_Static;
		}

		float speed_anim = Player.anim.GetFloat("Speed") + FlyAnimSpeedRate;
		Player.Increase_velocity(speed_anim);


    }
	
	public void Stop_()				////	 little by little
	{
		GameObject[] Obj = GameObject.FindGameObjectsWithTag("Obstacle");

		for (int i = 0; i <Obj.Length; i++)
		{
			Obj[i].GetComponent<Moving>().Speed = 0;
		}
        BaseSpeed = 0;
	}

    //private void ScoreManager()
    //{
    //    timePassed = Time.timeSinceLevelLoad;
    //    int ScoreValue = Mathf.RoundToInt( timePassed * BaseSpeed * 5);
    //    ScoreText.text = scoreString + ScoreValue.ToString();
    //}
	
}

