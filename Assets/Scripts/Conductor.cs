using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is used to keep the beat
public class Conductor : MonoBehaviour
{
    public MusicMetaPair musicMetaPair;

    GameManager gameManager;

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    private float songBpm;

    //The time duration of a beat in seconds
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public float firstBeatOffset;

    float lastbeat = 0;
    bool tooLateFired = false;

    // Calculates songPosition from a songPositionInBeats
    // Useful for checking future and past beats
    // example: at a bpm of 120, sec per beat will be 0.5, and beat number 10 will happen 5 seconds in
    public float GetSongPositionFromBeat(float beatNumber)
    {
        return beatNumber * secPerBeat;
    }

    // Takes a songPositionInBeats and returns the songPosition at the time of the closest perfect beat
    public float GetPerfectBeatTime(float beatPosition)
    {
        float beatTime = GetSongPositionFromBeat(beatPosition);

        float beatPositionFlattened = (Convert.ToInt32(beatPosition));

        // closest exact beat should be one of these two
        float beat1Time = GetSongPositionFromBeat(beatPositionFlattened); // get time of exact beat
        float beat2Time = GetSongPositionFromBeat(beatPositionFlattened + 1); // future beat

        float diff1 = Math.Abs(beat1Time - beatTime);
        float diff2 = Math.Abs(beat2Time - beatTime);

        if (diff1 < diff2)
        {
            return beat1Time;
        }
        return beat2Time;
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();


        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        songBpm = musicMetaPair.meta.BPM;

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        musicSource.clip = musicMetaPair.music;

        //Start the music
        musicSource.Play();

    }

    public void ContinueMusic()
    {
        AudioListener.pause = false;
        musicSource.UnPause();
    }

    public void PauseMusic()
    {
        // pauses timer
        AudioListener.pause = true;
        // pauses audio
        musicSource.Pause();
    }


    void Update()
    {
        if (!musicSource.isPlaying)
        {
            return;
        }

        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        // maybe use pitch so you can speed up and slow down songs
        // songPosition = (float)(AudioSettings.dspTime - dsptimesong) * song.pitch - offset;

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;


        if (songPosition > lastbeat + secPerBeat)
        {
            lastbeat += secPerBeat;

            tooLateFired = false;
            gameManager.events.BeatHappened();
        }

        if (songPosition > lastbeat + gameManager.acceptableDeviationSeconds)
        {
            if (tooLateFired == false)
            {
                tooLateFired = true;
                gameManager.events.TooLate();
            }

        }
    }


}
