using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    public float GetThrottleInput() => Input.GetAxis("Vertical");
    public float GetBrakeInput() => Input.GetKey(KeyCode.Space) ? 1f : 0f;
    public float GetSteeringInput() => Input.GetAxis("Horizontal");
    public bool GetClutchInput() => Input.GetKey(KeyCode.LeftShift);
    public bool GetHandbrakeInput() => Input.GetKey(KeyCode.B);
    
    public bool GetIgnitionInput() => Input.GetKeyDown(KeyCode.E);
}