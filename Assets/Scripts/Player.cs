using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    KeyCode up = new KeyCode();
    KeyCode down = new KeyCode();
    KeyCode left = new KeyCode();
    KeyCode right = new KeyCode();

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.events.onMovePlayer += MovePlayer;

        // get from config
        up = KeyCode.W;
        down = KeyCode.S;
        left = KeyCode.A;
        right = KeyCode.D;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(up)){
            gameManager.events.MoveAttempted(new Vector2(transform.position.x, transform.position.y + 1));
        }
        else if (Input.GetKeyDown(down)){
            gameManager.events.MoveAttempted(new Vector2(transform.position.x, transform.position.y - 1));
        }
        else if (Input.GetKeyDown(left)){
            gameManager.events.MoveAttempted(new Vector2(transform.position.x - 1, transform.position.y));
        }
        else if (Input.GetKeyDown(right)){
            gameManager.events.MoveAttempted(new Vector2(transform.position.x + 1, transform.position.y));
        }
    }

    public void MovePlayer(Vector2 desiredPosition)
    {
        transform.position = desiredPosition;
    }
}
