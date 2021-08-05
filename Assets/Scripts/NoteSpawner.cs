using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    Conductor conductor;
    public GameObject notePrefab;
    public GameObject[] spawnPositions;

    private void Start()
    {
        conductor = FindObjectOfType<Conductor>();

        conductor.onBeat += SpawnNote;
    }

    private void OnDestroy()
    {
        conductor.onBeat -= SpawnNote;
    }

    void SpawnNote()
    {
        // using this there will be no visuals for the first note
        // i think that's OK. It's better than pausing the music for one beat, anyway

        // this is the exact next beat
        float nextBeat = (int)conductor.songPositionInBeats + 1;

        // put code here to check that the next beat actually exists (song not over)

        // this is the time the next beat will happen
        float nextBeatTime = conductor.GetSongPositionFromBeat(nextBeat);

        // how much time remains before next beat
        float timeTillBeat = nextBeatTime - conductor.songPosition;

        foreach (GameObject spawnPosition in spawnPositions)
        {
            GameObject tmp = Instantiate(notePrefab, spawnPosition.transform.position, Quaternion.identity, transform);


           
            // i don't really want to call getcomponent this often. see if you can't find another solution
            tmp.GetComponent<ScreenNote>().StartMove(spawnPosition.transform.position, this.transform.position, timeTillBeat);

            // may be better to check if it has reached destination
            Destroy(tmp, timeTillBeat);
        }

    }
}
