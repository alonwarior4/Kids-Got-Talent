
using UnityEngine;
using System.Collections;

public class Fps : MonoBehaviour
{

    string label = "";
    public static float count;

    IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            if (Time.timeScale == 1)
            {
                yield return new WaitForSeconds(0.1f);
                count = (1 / Time.deltaTime);
                label = "FPS :" + (Mathf.Round(count));
            }
            else
            {
                label = "Pause";
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(50, 40, 200, 200), label);
    }
}