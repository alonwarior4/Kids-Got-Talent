using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "numbersData")]
public class NumbersData : ScriptableObject
{
    public SpriteRenderer[] numbers;
    public AudioClip[] numberSounds;
    public AudioClip[] sheepSounds;

}
