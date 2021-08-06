using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // subscribe to this
    public event Action onBeat;
    // fires trigger
    public void BeatHappened()
    {
        onBeat();
    }
}
