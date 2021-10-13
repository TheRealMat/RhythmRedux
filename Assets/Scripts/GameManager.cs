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

    public Conductor conductor;

    public FileHandler fileHandler;

    bool playerMoved;

    public float acceptableDeviationSeconds;

    public GameObject pauseMenu;

    bool gameStarted;

    private async void Awake()
    {
        events = FindObjectOfType<GameEvents>();
        conductor = FindObjectOfType<Conductor>();
        fileHandler = FindObjectOfType<FileHandler>();

        events.onMoveAttempt += ManagePlayerMove;
        events.onTooLate += PlayerTooLate;
        events.onNextTurn += NewTurn;
        events.onBeatMissed += PlayerMissedBeat;

        await fileHandler.Setup();
    }

    public void StartGame()
    {
        if (gameStarted == true)
        {
            UnpauseGame();
            return;
        }
        gameStarted = true;

        conductor.musicMetaPair = fileHandler.musicMetaPairs[0];
        conductor.StartMusic();
    }
    public void PauseGame()
    {

        conductor.PauseMusic();
        pauseMenu.SetActive(true);
    }
    public void UnpauseGame()
    {
        conductor.ContinueMusic();
    }

    // checks if player's input was on time
    public bool ValidateMoveTiming()
    {

        // calculated with secPerBeat so it scales with speed of the song
        // 1 would be the entire secPerBeat and 0.5 would be half. Half is the cutoff point to where you can actually miss a beat
        acceptableDeviationSeconds = conductor.secPerBeat * acceptableDeviation;

        float exactBeatTime = conductor.GetPerfectBeatTime(conductor.songPositionInBeats);

        float upperAcceptableBound = exactBeatTime + acceptableDeviationSeconds;
        float lowerAcceptableBound = exactBeatTime - acceptableDeviationSeconds;

        if (Math.Abs(conductor.songPosition - exactBeatTime) <= acceptableDeviationSeconds)
        {
            return true;
        }
        return false;

    }

    void ManagePlayerMove(Vector2 desiredPosition)
    {
        // check if hitting a wall or something


        // do not allow to move twice
        if (playerMoved == true)
        {
            return;
        }
        playerMoved = true;

        if (ValidateMoveTiming())
        {
            events.MovePlayer(desiredPosition);
            events.BeatHit();
            events.NextTurn();
        }
        else
        {
            events.BeatMissed();
        }

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
        }
    }

    void PlayerMissedBeat()
    {
        events.NextTurn();
    }

    void NewTurn()
    {
        // run enemy logic
    }
}
