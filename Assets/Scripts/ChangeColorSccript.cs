using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorSccript : MonoBehaviour
{
    Conductor conductor;
    Renderer tileRenderer;
    bool toggler = false;
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        tileRenderer = GetComponent<Renderer>();
        conductor.onBeat += ChangeColor;
    }

    void ChangeColor()
    {
        if (toggler == true)
        {
            // i'd rather set a bool, but it seems like you can't
            tileRenderer.material.SetFloat("UVOffset", 0f);
        }
        else
        {
            tileRenderer.material.SetFloat("UVOffset", -0.5f);
        }

        toggler = !toggler;
    }
}
