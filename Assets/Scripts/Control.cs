using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    GameManager gameManager;
    GameObject player;
    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(gameManager.fileHandler.config.up))
        {
            gameManager.events.MoveAttempted(new Vector2(0, 1));
        }
        else if (Input.GetKeyDown(gameManager.fileHandler.config.down))
        {
            gameManager.events.MoveAttempted(new Vector2(0, -1));
        }
        else if (Input.GetKeyDown(gameManager.fileHandler.config.left))
        {
            gameManager.events.MoveAttempted(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(gameManager.fileHandler.config.right))
        {
            gameManager.events.MoveAttempted(new Vector2(+1, 0));
        }
        else if (Input.GetKeyDown(gameManager.fileHandler.config.pause))
        {
            gameManager.PauseGame();
        }
    }
}
