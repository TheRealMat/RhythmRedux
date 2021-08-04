using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTester : MonoBehaviour
{
    Conductor conductor;
    SpriteRenderer spriteRenderer;
    AudioSource audioPlayer;
    public AudioClip soundEffect;

    byte toggler = 0;
    Color[] colors = new Color[] { Color.white, Color.red };
    private void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioPlayer = FindObjectOfType<AudioSource>();

        conductor.onBeat += doThing;
    }

    private void OnDestroy()
    {
        conductor.onBeat -= doThing;
    }

    void doThing()
    {
        audioPlayer.PlayOneShot(soundEffect);
        if (toggler == 0)
        {
            toggler = 1;
        }
        else
        {
            toggler = 0;
        }

        spriteRenderer.color = colors[toggler];
    }
}
