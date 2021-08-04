using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTimeTester : MonoBehaviour
{
    Conductor conductor;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    Color[] colors = new Color[] { Color.white, Color.blue };
    AudioSource audioPlayer;
    public AudioClip soundEffect;
    bool toggle1 = false;
    bool toggle2 = false;

    private void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioSource>();
    }
    void LateUpdate()
    {
        if (gameManager.ValidateMove())
        {
            spriteRenderer.color = colors[1];
            toggle1 = true;
        }
        else
        {
            spriteRenderer.color = colors[0];
            toggle1 = false;
        }
        if (toggle1 != toggle2) {
            toggle2 = toggle1;
            audioPlayer.PlayOneShot(soundEffect);
        }
    }
}
