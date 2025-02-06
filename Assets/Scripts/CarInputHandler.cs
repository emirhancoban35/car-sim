using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarInputHandler
{
    public static bool StartKeyDown() => Input.GetKeyDown(KeyCode.E);
    public static bool StartKeyUp() => Input.GetKeyUp(KeyCode.E);
    public static bool StopKeyDown() => Input.GetKeyDown(KeyCode.Q);
    public static bool PressClutch() => Input.GetKey(KeyCode.Z);
    public static bool PressBrake() => Input.GetKey(KeyCode.X);
    public static bool PressGas() => Input.GetKey(KeyCode.C);
    
}
