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
        // crash if nothing subsribed
        if (onBeat != null)
        {
            onBeat();
        }
    }

    //  fires acceptableDeviationSeconds after each beat
    public event Action onTooLate;
    public void TooLate()
    {
        if (onTooLate != null)
        {
            onTooLate();
        }

    }



    // player attempted to move
    public event Action<Vector2> onMoveAttempt;
    public void MoveAttempted(Vector2 desiredDirection)
    {
        if (onMoveAttempt != null)
        {
            onMoveAttempt(desiredDirection);
        }

    }

    // move player
    public event Action<Vector2> onMovePlayer;
    public void MovePlayer(Vector2 desiredPosition)
    {
        if (onMovePlayer != null)
        {
            onMovePlayer(desiredPosition);
        }

    }

    // player either hit too early or not at all
    public event Action onBeatMissed;
    public void BeatMissed()
    {
        if (onBeatMissed != null)
        {
                onBeatMissed();
        }

    }

    // player successfully hit on the beat
    public event Action onBeatHit;
    public void BeatHit()
    {
        if (onBeatHit != null)
        {
            onBeatHit();
        }

    }



    public event Action onNextTurn;
    public void NextTurn()
    {
        if (onNextTurn != null)
        {
            onNextTurn();
        }

    }

}
