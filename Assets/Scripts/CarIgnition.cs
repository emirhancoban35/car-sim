using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarIgnition
{
    public bool IsCarRunning { get; private set; }

    public void ToggleIgnition()
    {
        if (Input.GetKeyDown(KeyCode.E))
            IsCarRunning = !IsCarRunning;
    }
}