using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine
{
    private float _currentSpeed;
    private const float MAX_SPEED = 200f;
    private const float ACCELERATION = 10f;
    private const float BRAKING_FORCE = 20f;
    
    public float CurrentSpeed => _currentSpeed;

    public void ApplyThrottle(float throttle)
    {
        _currentSpeed += throttle * ACCELERATION * Time.deltaTime;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, MAX_SPEED);
    }

    public void ApplyBraking(float brake)
    {
        _currentSpeed -= brake * BRAKING_FORCE * Time.deltaTime;
        _currentSpeed = Mathf.Max(_currentSpeed, 0);
    }
}