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

    //  fires acceptableDeviationSeconds after each beat
    public event Action onTooLate;
    public void TooLate()
    {
        onTooLate();
    }



    // player attempted to move
    public event Action onMoveAttempt;
    public void MoveAttempted()
    {
        onMoveAttempt();
    }

    // player either hit too early or not at all
    public event Action onBeatMissed;
    public void BeatMissed()
    {
        onBeatMissed();
    }

    // player successfully hit on the beat
    public event Action onBeatHit;
    public void BeatHit()
    {
        onBeatHit();
    }



    public event Action onNextTurn;
    public void NextTurn()
    {
        onNextTurn();
    }

}
