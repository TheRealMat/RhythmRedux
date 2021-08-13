using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.events.onMovePlayer += MovePlayer;
    }

    public void MovePlayer(Vector2 desiredPosition)
    {
        transform.position = desiredPosition;
    }
}
