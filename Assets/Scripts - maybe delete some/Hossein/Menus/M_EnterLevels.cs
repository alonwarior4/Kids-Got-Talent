using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class M_EnterLevels : MonoBehaviour {

    public UiImage ui = new UiImage();
    // Use this for initialization
    public bool DebogeMode;
    private void Awake()
    {
        if (DebogeMode)
        {
            ObscuredPrefs.SetBool("Colors", true);
            ObscuredPrefs.SetBool("Numbers", true);
            ObscuredPrefs.SetBool("Dialogue", true);
            ObscuredPrefs.SetBool("SavingPrivateBird", true);
            ObscuredPrefs.SetBool(NamesScenes.Alighapoo.ToString() , true);
            ObscuredPrefs.SetBool(NamesScenes.BodyPart.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.Category.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.Class.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.ColorFruits.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.ColorHajiFiruz.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.ColorJungle.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.ColorKharazmi.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.ColorStoreCloth.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.CyrusTemple.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.Damavand.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.HiddenObject.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.KolahFarangi.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.MiniRunner.ToString(), true);
            ObscuredPrefs.SetBool(NamesScenes.SchoolYard.ToString(), true);
        }
        else
        {
           // ObscuredPrefs.DeleteAll();
        }
    }

    void Start() {

        ObscuredPrefs.SetBool("Colors", true);
        ChangeSpritelevels("Numbers", ui.Numbers );
        ChangeSpritelevels("Dialogue", ui.Dialogue);
        ChangeSpritelevels("SavingPrivateBird", ui.SavingPrivateBirds);
   }

    // Update is called once per frame
    void Update() {

    }

    private void ChangeSpritelevels(string namelevel , GameObject u )
    {
        if (CheckOpenLevels(namelevel))
        {
            u.transform.GetChild(0).gameObject.SetActive(true);
            u.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            u.transform.GetChild(0).gameObject.SetActive(false);
            u.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private bool CheckOpenLevels(string namelevel)
    {
        //// Colors 
        ///Dialogue         /// is names level in btn string parametr
        ///Numbers 
        ///SavingPrivateBird

        bool ret = false;

        if (ObscuredPrefs.HasKey(namelevel))
        {
            if (ObscuredPrefs.GetBool(namelevel))
            {
                ret = true;
            }
        }
        return ret;
    }

    public void ClickLevels(string name)
    {
        if (CheckOpenLevels(name))
        {
            ObscuredPrefs.SetString("ClickMini", "Last" + name);
            SceneManager.LoadScene("Mission Loading");
        }
    }

    public void NextScene(string n)
    {
        SceneManager.LoadScene(n);
    }

    [System.Serializable]
    public class UiImage {

        public GameObject Numbers;
        public GameObject Dialogue;
        public GameObject SavingPrivateBirds;
    }

}
