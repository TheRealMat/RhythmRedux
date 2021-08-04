using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenNote : MonoBehaviour
{
    Coroutine move;

    public void StartMove(Vector3 startPos, Vector3 endPos, float duration)
    {
        move = StartCoroutine(this.Move(startPos, endPos, duration));
    }
    private IEnumerator Move(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0;
        float ratio = elapsedTime / duration;
        while (ratio < 1f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos, endPos, ratio);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(move);
    }
}
