using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTester : MonoBehaviour
{
    Conductor conductor;
    float lastbeat = 0;
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
    }
    void LateUpdate()
    {

        if (conductor.songPosition > lastbeat + conductor.secPerBeat)
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
            lastbeat += conductor.secPerBeat;

        }

    }
}
