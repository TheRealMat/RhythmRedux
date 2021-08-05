using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelMusic", order = 1)]
public class LevelMusic : ScriptableObject
{
    public AudioClip Song;

    public float BPM;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;
}
