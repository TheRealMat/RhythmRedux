using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBeat : MonoBehaviour
{
    Conductor conductor;
    public Image[] beats;
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();
    }
    private void LateUpdate()
    {
        foreach (Image beat in beats)
        {
            beat.material.SetFloat("_Offset", conductor.songPositionInBeats);
        }
    }
}
