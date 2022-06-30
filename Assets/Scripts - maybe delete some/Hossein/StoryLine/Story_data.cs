using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName = "StoryLine", menuName = "CYRUS/StoryLine" )]
public class Story_data : ScriptableObject
{

	public Storydata[] StoryData;
    public oliverTalk[] oliverTalk;


	
}

[System.Serializable]
public class Storydata
{
    [TextArea]
	public String text;
	public AudioClip Audio;
    public bool isFirst;
    public bool isLast;
}

[System.Serializable]
public class oliverTalk
{    
    public string text;
    public AudioClip Audio;
}