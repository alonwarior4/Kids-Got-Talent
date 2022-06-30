using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInBirdsLoading : MonoBehaviour {
    private float dis_Going;
    private BirdsSoundPlayer soundbird;
    private bool IsGoing;
    // Use this for initialization
    void Start () {

        soundbird = GetComponent<BirdsSoundPlayer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (IsGoing)
        {
            Fading();
        }
	}

    private void Fading()
    {
        float fadevolume = (dis_Going - Vector2.Distance(this.transform.position, new Vector2(-2.7f , 2))) / dis_Going;
        soundbird.fade_v = 1 - fadevolume;
    }

    public void SetDistanceGo()
    {
        dis_Going = Vector2.Distance(this.transform.position , new Vector2(-2.7f , 2));
        IsGoing = true;
    }
}
