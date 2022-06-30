using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNumberPanel : MonoBehaviour
{
    bool isBirunPanel = false;



    public void GetOut_Ghalaf()
    {
        isBirunPanel = !isBirunPanel;
        GetComponent<Animator>().SetBool("Ch_PanelState", isBirunPanel);
    }

    public void LoadNumbersScene()
    {
        StartCoroutine(NextSecne());
    }


    IEnumerator NextSecne()
    {
        Paper.instance.EndOfLevel();
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => Paper.instance.PaperAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f);
        ObscuredPrefs.SetString("ClickMini", "LastNumbers");
        SceneManager.LoadScene("Mission Loading");
    }
}
