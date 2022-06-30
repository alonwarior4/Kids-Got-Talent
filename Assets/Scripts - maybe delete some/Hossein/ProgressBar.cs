using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public static ProgressBar instance;
    public Image bar; 
   [SerializeField]
    private float initValue;
   [SerializeField]
    private float AddedValue;
    private float Value;
    [SerializeField]
    private float speed;


    // Use this for initialization
    void Start () {
        if (!instance)
        {
            instance = this;
        }	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FullingProgress(bool isfirst)
    {
        if (isfirst == false)
        {
            Value = bar.fillAmount;
            while (bar.fillAmount < Value + AddedValue)
            {
                yield return null;
                bar.fillAmount += speed;
            }
        }
        else
        {

            while (bar.fillAmount < initValue)
            {
                yield return null;
                bar.fillAmount += speed;
            }

        }

    }

}
