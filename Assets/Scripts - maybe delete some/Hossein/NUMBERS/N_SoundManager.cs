using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N_SoundManager : MonoBehaviour
{
    public static N_SoundManager Instance;
    AudioSource audiosource;
    public AudioClip S_Click;
    public AudioClip S_True;
    public AudioClip S_Wrong;
    public AudioClip S_Sheep;
    public NumbersData numbersData;
    public N_ColorsAndAnimalData Colorsdata;


    private WaitForSeconds wait = new WaitForSeconds(.6f);
    private void Awake()
    {
        Instance = this;
        audiosource = GetComponent<AudioSource>();
        // until = new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(String S_rec)
    {
        switch (S_rec)
        {
            case "S_Click":
                AudioSource.PlayClipAtPoint(S_Click, Camera.main.transform.position);
                break;
            case "S_True":
                AudioSource.PlayClipAtPoint(S_True, Camera.main.transform.position);
                break;
            case "S_Wrong":
                AudioSource.PlayClipAtPoint(S_Wrong, Camera.main.transform.position);
                break;
        }
    }


    public IEnumerator PlaySoundCounterNaraitor(List<int> typeBirds, List<int> typesheep)
    {
        yield return new WaitForSeconds(1.2f);

        N_ResultInUi.instance.ShowResult();

        yield return new WaitForSeconds(1);

        N_ResultInUi.instance.Anim_PanelBirds.gameObject.SetActive(true);
        yield return new WaitForSeconds(.66f);
        audiosource.PlayOneShot(numbersData.numberSounds[Questions.instance.CounterBirds - 1]);
        // yield return  new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
        yield return wait;

        for (int i = 0; i < typeBirds.Count; i++)
        {
            if (i != 0)
            {
                audiosource.PlayOneShot(Colorsdata.and);
                //   yield return new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
                yield return wait;
            }
            audiosource.PlayOneShot(Colorsdata.GetColorBirds(typeBirds[i]));

            //  yield return new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
            yield return wait;
        }

        if (Questions.instance.CounterBirds == 1)
        {
            audiosource.PlayOneShot(Colorsdata.bird);
        }
        else
        {
            audiosource.PlayOneShot(Colorsdata.Birds);
        }

        //  yield return new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
        yield return wait;

        if (typesheep.Count > 0)
        {
            N_ResultInUi.instance.Anim_PanelSheeps.gameObject.SetActive(true);

            yield return new WaitForSeconds(.66f);
            audiosource.PlayOneShot(Colorsdata.and);
            //   yield return new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
            yield return wait;

            audiosource.PlayOneShot(numbersData.numberSounds[Questions.instance.CounterSheep - 1]);

            //   yield return new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
            yield return wait;

            audiosource.PlayOneShot(Colorsdata.GetColorSheeps(typesheep[0]));

            //  yield return new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
            yield return wait;

            if (Questions.instance.CounterSheep == 1)
            {
                audiosource.PlayOneShot(Colorsdata.sheep);
            }
            else
            {
                audiosource.PlayOneShot(Colorsdata.Sheeps);
            }


            // yield return new WaitUntil(() => (audiosource.clip.length - audiosource.time) <= 0);
            yield return wait;

        }


        N_UiManager.Instance.EmptyUi();


        yield return new WaitForSeconds(2.25f /*fuck*/);

        N_ResultInUi.instance.Anim_PanelBirds.SetTrigger("ShowOff");

        yield return new WaitForSeconds(1);

        if (N_ResultInUi.instance.Anim_PanelSheeps)
        {
            N_ResultInUi.instance.Anim_PanelSheeps.SetTrigger("ShowOff");
        }



        Questions.instance.NextQuest();
        yield return new WaitForSeconds(1);

    }



    //TODO For Test Delete
    private void OnDestroy()
    {
        Instance = null;
    }

}


