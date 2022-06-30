using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI tmProRef;
    [SerializeField] Story_data storyData;
    [SerializeField] UIManagement uiManagement;
    AudioClip currentTextAudio;


    /// <summary>
    /// use this function just for calling Coroutine from another class.
    /// </summary>
    /// <param name="Text"></param>
 

    public void ShowOliverBullshits(int dataIndex)
    {
        AudioClip textAudio = storyData.oliverTalk[dataIndex].Audio;
        TextAudio.instance.SetCurrentAudio(textAudio);
        TextAudio.instance.ReadText();
        string oliverText = uiManagement.ConstructOliverText(storyData.oliverTalk[dataIndex].text);
        tmProRef.text = oliverText;
    }


}
