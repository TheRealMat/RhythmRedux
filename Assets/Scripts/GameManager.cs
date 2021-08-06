using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEvents))]
public class GameManager : MonoBehaviour
{
    // 0.5 and above should mean that it is impossible to fail
    public float acceptableDeviation = 0.2f;

    public GameEvents events;

    Conductor conductor;

    bool playerMoved;

    public float acceptableDeviationSeconds;

    private void Start()
    {
        events = FindObjectOfType<GameEvents>();
        conductor = FindObjectOfType<Conductor>();
        // calculated with secPerBeat so it scales with speed of the song
        // 1 would be the entire secPerBeat and 0.5 would be half. Half is the cutoff point to where you can actually miss a beat
        acceptableDeviationSeconds = conductor.secPerBeat * acceptableDeviation;

        events.onMoveAttempt += ManagePlayerMove;
        events.onTooLate += PlayerTooLate;
        events.onNextTurn += NewTurn;
    }

    // checks if player's input was on time
    public bool ValidateMove()
    {
        float exactBeatTime = conductor.GetPerfectBeatTime(conductor.songPositionInBeats);

        float upperAcceptableBound = exactBeatTime + acceptableDeviationSeconds;
        float lowerAcceptableBound = exactBeatTime - acceptableDeviationSeconds;

        if (Math.Abs(conductor.songPosition - exactBeatTime) <= acceptableDeviationSeconds)
        {
            return true;
        }
        return false;

    }

    void ManagePlayerMove()
    {
        playerMoved = true;

        if (ValidateMove())
        {
            events.BeatHit();
            events.NextTurn();
            return;
        }
        events.BeatMissed();
        events.NextTurn();
    }
    // player did not move in time
    void PlayerTooLate()
    {
        // player should now be able to move again
        // this should be here to ensure player can't
        // move twicer per beat
        playerMoved = false;

        // go to next turn if player didn't move at all
        if (playerMoved == false)
        {
            events.BeatMissed();
            events.NextTurn();
        }
    }

    void NewTurn()
    {
        // run enemy logic
    }
}
