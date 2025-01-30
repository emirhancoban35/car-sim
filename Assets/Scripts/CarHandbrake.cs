using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarHandbrake
{
    public bool IsHandbrakeApplied { get; private set; }

    public void HandleHandbrake(bool isHandbrakePressed)
    {
        IsHandbrakeApplied = isHandbrakePressed;
    }
}