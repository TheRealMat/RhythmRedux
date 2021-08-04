using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    Conductor conductor;
    GameManager gameManager;
    private void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float pressedTime = conductor.songPositionInBeats;

            

            gameManager.ValidateMove();

        }
    }
}
