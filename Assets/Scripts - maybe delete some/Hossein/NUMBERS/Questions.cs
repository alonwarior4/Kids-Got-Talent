  using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Random = System.Random;
using TMPro;



public class Questions : MonoBehaviour
{
    [SerializeField] OtherUIManagement uiManager;
    [SerializeField] GameObject SnailParent;
	public static Questions instance;
	public DataQuestion DataQuestion;
	public GameObject []  birdsPrefabs = new GameObject[7];
    public GameObject[] ReptilePrefabs = new GameObject [4];

    private List<int> ListTypeBirdsinEveryquest = new List<int>();
	private List<int> ListTypeSheepinEveryquest = new List<int>();

    [HideInInspector]
    public int CounterBirds;
    [HideInInspector]
    public int CounterSheep;
    #region List type random 
    List<int> RanListBirdsTrue =  new List<int>();
    List<int> RanListBirdsFalse = new List<int>();
    List<int> RanListSheepTrue =  new List<int>();
    List<int> RanListSheepFalse = new List<int>();
    #endregion

    //[HideInInspector]
    public int Answers;



    public int CounterQuestion;
    private bool isfinish;

    private void Awake()
	{
		if (!instance)
		{
			instance = this;
		}
        CounterQuestion = 0;
	}

    // Use this for initialization
    IEnumerator Start()
    {
        isfinish = false;
        yield return new WaitUntil(() => uiManager.isEndOfTyping);
        SnailParent.SetActive(true);
        StartCoroutine(D_nextQuestion());
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject SpawnObject(GameObject g , Vector3 pos )   // change with Object pooling
	{
		///  forinstantiate  2face object true and false  filipx
		
		GameObject Spwned = Instantiate(g, pos, Quaternion.identity)as GameObject;
		return Spwned;
	}

	IEnumerator SetTragetSpawnAnimal(int Pre  , int count ,bool ISanswer , string animal)
	{
		Vector3 vec;
		GameObject g;
		for (int i = 0; i < count; i++)
		{
			yield return new WaitForSeconds(.3f);
			if (animal == "Bird")
			{
				vec = N_ManagmentTargetsBirds.Instance.GetPosStart();
				 g = SpawnObject(birdsPrefabs[Pre], vec);

			}
			else
			{
				vec = N_managmentTargetsReptile.Instance.GetPosStart();
				 g = SpawnObject(ReptilePrefabs[Pre], vec);

			}

			if (ISanswer)
			{
				g.GetComponent<Animals>().IsAnswer = true;
				Answers += 1;
			}
			else {
				g.GetComponent<Animals>().IsAnswer = false;
			}
		}
	}

	 IEnumerator PlanQuest()
	{
        #region  get random type birds  -- and animal true(0) or No(1)   

        List<int> Numbers_BT = new List<int>();
        List<int> Numbers_BF = new List<int>();
        List<int> Numbers_ST = new List<int>();
        List<int> Numbers_SF = new List<int>();

        Numbers_BT = GetRandomNumberFromList(0 , 0);  //true(0)  birds(0) 
	    GetRandomTypeFromListBirds(DataQuestion.DataSpawn[0].Data_numbers[0].NumberTypeObject , RanListBirdsTrue);

		yield return  new WaitForSeconds(.1f);
		
		///////////////////////////////
		Numbers_BF = GetRandomNumberFromList(1,0);  // false (1)   birds(0) 
	    GetRandomTypeFromListBirds(DataQuestion.DataSpawn[1].Data_numbers[0].NumberTypeObject , RanListBirdsFalse);

		yield return  new WaitForSeconds(.1f);

		/////////////////////////////////////		
		Numbers_ST = GetRandomNumberFromList(0,1);  // true(0)  Sheep(1) 
	    GetRandomTypeFromListSheeps(DataQuestion.DataSpawn[0].Data_numbers[1].NumberTypeObject , RanListSheepTrue);
		/////////////////////////////////

		yield return  new WaitForSeconds(.1f);
		
		Numbers_SF = GetRandomNumberFromList(1,1);  // False(1) Sheep(1) 
        GetRandomTypeFromListSheeps(DataQuestion.DataSpawn[1].Data_numbers[1].NumberTypeObject , RanListSheepFalse);
		///////////////////
		yield return  new WaitForSeconds(.1f);

		 GiveTarget(RanListBirdsTrue , Numbers_BT , true ,  "Bird");
	 	 GiveTarget(RanListBirdsFalse, Numbers_BF , false ,  "Bird");
         GiveTarget(RanListSheepTrue, Numbers_ST , true ,  "Sheep");
		 GiveTarget(RanListSheepFalse, Numbers_SF , false ,  "Sheep");


         SendTypesAndCountBirdsForShowInUi(RanListBirdsTrue , Numbers_BT);
         SendTypesAndCountSheepsForShowInUi(RanListSheepTrue , Numbers_ST);
   		//N_UiManager.Instance.SetActiveCountTypeAnimals(false);
        #endregion
    }

    private void SendTypesAndCountBirdsForShowInUi(List<int> Types , List<int> Count)
     {
         for (int i = 0; i < Types.Count; i++)
         {
             N_UiManager.Instance.ShowInUIBirds(Types[i] , Count[i]);   
         }
     }
	
    private void SendTypesAndCountSheepsForShowInUi(List<int> Types , List<int> Count)
     {
         for (int i = 0; i < Types.Count; i++)
         {
             N_UiManager.Instance.ShowInUISheeps(Types[i] , Count[i]);
         }
     }

	private void GiveTarget(List<int> typesBt, List<int> numbersBt, bool answer, string Animal)
	{
		for (int i = 0; i < typesBt.Count; i++)
		{
			StartCoroutine(SetTragetSpawnAnimal(typesBt[i], numbersBt[i], answer, Animal));
		}		
	}

	private List<int> GetRandomNumberFromList( int TrueOrNo , int BirdOrSheep)
	{
        if (DataQuestion.DataSpawn[TrueOrNo].Data_numbers[BirdOrSheep].NumberTypeObject !=0)
        {
            List<int> PartNumberType = new List<int>();

            int RanFromRange = UnityEngine.Random.Range(DataQuestion.DataSpawn[TrueOrNo].Data_numbers[BirdOrSheep].MinCount,
                DataQuestion.DataSpawn[TrueOrNo].Data_numbers[BirdOrSheep].MaxCount + 1);
            ////////////////////////////////////////////////
            int part = RanFromRange / DataQuestion.DataSpawn[TrueOrNo].Data_numbers[BirdOrSheep].NumberTypeObject;
            int rest = RanFromRange % DataQuestion.DataSpawn[TrueOrNo].Data_numbers[BirdOrSheep].NumberTypeObject;

            for (int i = 0; i < DataQuestion.DataSpawn[TrueOrNo].Data_numbers[BirdOrSheep].NumberTypeObject; i++)
            {
                PartNumberType.Add(part);
            }

            int increaseRes = UnityEngine.Random.Range(0, DataQuestion.DataSpawn[TrueOrNo].Data_numbers[BirdOrSheep].NumberTypeObject);

            PartNumberType[increaseRes] += rest;

            return PartNumberType;

        }
        else
        {
            return null;
        }


	}

	private List<int> GetRandomTypeFromListSheeps( int NumberType  ,List<int> RanType)
	{
        int R_temp;
		

		for (int i = 0; i < NumberType; i++)
		{
            while (true)
            {
	            R_temp = UnityEngine.Random.Range(0 ,4);
	            if (!RanListSheepTrue.Contains(R_temp)&& !RanListSheepFalse.Contains(R_temp))
	            {
					break;
	            }
            }
            ListTypeSheepinEveryquest.Remove(R_temp);
            RanType.Add(R_temp);

		}

		return RanType;
	}

	private List<int> GetRandomTypeFromListBirds(int NumberType , List<int> RanType)
	{
		int R_temp;

		for (int i = 0; i < NumberType; i++)
		{
			while (true)
			{
				R_temp = UnityEngine.Random.Range(0, 7);
				if (!RanListBirdsTrue.Contains(R_temp) && !RanListBirdsFalse.Contains(R_temp))
				{
					break;
				}
			}

			ListTypeBirdsinEveryquest.Remove(R_temp);
            RanType.Add(R_temp);
		}

		return RanType;

        
	}
    
	public void CheckingEnd()
	{

        Animals[] AnimalsINScene;
		AnimalsINScene = GameObject.FindObjectsOfType<Animals>();
		AnimalsINScene = GameObject.FindObjectsOfType<Animals>();
        bool isScene = false;
        for (int i = 0; i < AnimalsINScene.Length; i++)
        {
            if (AnimalsINScene[i].IsAnswer)
            {
                isScene = true;
                break;
            }
        }

        if (isScene == false)   //// end every question
        {

            AnimalsINScene = GameObject.FindObjectsOfType<Animals>();
            for (int i = 0; i < AnimalsINScene.Length; i++)
            {
                AnimalsINScene[i].GetComponent<Animals>().isfinish = true;
            }


            SnailWalking.instance.ChangeTarget();
            CounterQuestion += 1;

            StartCoroutine(N_SoundManager.Instance.PlaySoundCounterNaraitor(RanListBirdsTrue, RanListSheepTrue));

            }
    }

    public void NextQuest()
    {
        Animals[] AnimalsINScene = GameObject.FindObjectsOfType<Animals>();

        for (int i = 0; i < AnimalsINScene.Length; i++)
        {
            AnimalsINScene[i].EndQuestion();
        }
        ////////////// next question
        
            StartCoroutine(D_nextQuestion());
    }

    IEnumerator D_nextQuestion()
    {
        yield return new WaitForSeconds(1);
        Set_All();
        yield return new WaitForSeconds(4);
        if (isfinish == false)
        {
            StartCoroutine(PlanQuest());
        }
    }

	private void Set_All()
	{
        for (int i = 0; i < N_ManagmentTargetsBirds.Instance.forCome.Length; i++)
        {
            N_ManagmentTargetsBirds.Instance.forCome[i].InUse = false;
        }
        for (int i = 0; i < N_managmentTargetsReptile.Instance.forCome.Length; i++)
        {
            N_managmentTargetsReptile.Instance.forCome[i].InUse = false;
        }

        for (int i = 0; i <N_ManagmentTargetsBirds.Instance.forGo.Length; i++)
		{
			N_ManagmentTargetsBirds.Instance.forGo[i].InUse = false;
		}
		for (int i = 0; i <N_managmentTargetsReptile.Instance.forGo.Length; i++)
		{
			N_ManagmentTargetsBirds.Instance.forGo[i].InUse = false;
		}

        CounterBirds = 0;
        CounterSheep = 0;
        ListTypeBirdsinEveryquest.Clear();
        ListTypeSheepinEveryquest.Clear();

        RanListBirdsFalse.Clear();
        RanListBirdsTrue.Clear();
        RanListSheepFalse.Clear();
        RanListSheepTrue.Clear();


        for (int i = 0; i <birdsPrefabs.Length; i++)
		{
			ListTypeBirdsinEveryquest.Add(i);
		}
		
		for (int i = 0; i <ReptilePrefabs.Length; i++)
		{
			ListTypeSheepinEveryquest.Add(i);
		}

        N_UiManager.Instance.SetShowUI();
		Answers = 0;


        if (CounterQuestion >= DataQuestion.RepeatLevel)    /// end of level
        {
            isfinish = true;
            OtherUIManagement uiManager = FindObjectOfType<OtherUIManagement>();
            uiManager.TellFinalStory();
            uiManager.isEndOfScene = true;
           
        }
    }



    //TODO for test delete
    private void OnDestroy()
    {
        instance = null;
    }

}
