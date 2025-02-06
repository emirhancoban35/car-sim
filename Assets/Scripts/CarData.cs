using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "ScriptableObjects/Create New", order = 0)]
public class CarData : ScriptableObject
{
    [Header("Car Info")]
    public string carName;
    public uint carKm;
    public string carYear;
    
    [Header("Car Dial")]
    public float currentSpeed;
    public float tachometer;
    public float fuelLevel;
    public float temperatureIndicator;
    
    [Header("Car Pedals")]
    public bool isClutchPressed;
    public bool isBrakePressed;
    public bool isGasPressed;
    
    [Header("Car Status")]
    public bool isHandbrakeApplied;
    public bool isCarRunning;
    public bool isMotorRunning;
    public byte gearStatus;
    public bool isManual;
}