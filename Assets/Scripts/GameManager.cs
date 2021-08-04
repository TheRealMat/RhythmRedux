using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 0.5 and above should mean that it is impossible to fail
    float acceptableDeviation = 0.2f;


    Conductor conductor;

    private void Start()
    {
        conductor = FindObjectOfType<Conductor>();
    }

    public bool ValidateMove()
    {
        float exactBeatTime = conductor.GetPerfectBeatTime(conductor.songPositionInBeats);

        // calculated with secPerBeat so it scales with speed of the song
        // 1 would be the entire secPerBeat and 0.5 would be half. Half is the cutoff point to where you can actually miss a beat
        float acceptableDeviationSeconds = conductor.secPerBeat * acceptableDeviation;

        float upperAcceptableBound = exactBeatTime + acceptableDeviationSeconds;
        float lowerAcceptableBound = exactBeatTime - acceptableDeviationSeconds;

        if (Math.Abs(conductor.songPosition - exactBeatTime) <= acceptableDeviationSeconds)
        {
            return true;
        }
        return false;

    }
}
