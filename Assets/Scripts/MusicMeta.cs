using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MusicMeta
{
    public float BPM = 120;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset = 0;
}
