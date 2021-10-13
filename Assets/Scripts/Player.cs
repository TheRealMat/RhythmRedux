using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    Vector2 targetPosition;
    public float speedModifier = 15;

    private void Update()
    {
        float step = gameManager.conductor.secPerBeat * speedModifier * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }

    private void Start()
    {
        targetPosition = transform.position;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.events.onMovePlayer += MovePlayer;
    }

    public void MovePlayer(Vector2 desiredDirection)
    {
        targetPosition += desiredDirection;
    }
}
