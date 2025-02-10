using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "ScriptableObjects/Create New", order = 0)]
public class CarData : ScriptableObject
{
    public bool isTestMode;
    
    [Header("Car Info")]
    public string carName;
    public uint carKm;
    public string carYear;
    
    [Header("Car Pedals")]
    public bool isClutchPressed;
    public bool isBrakePressed;
    public bool isGasPressed;
    
    [Header("Transmission System")] 
    public bool isManual;
    
    [Header("For Manual Cars")] 
    public byte gearStatus;
    public byte gearRatios;
    [Header("For Automatic Cars")] 
    public TransmissionMode transmissionMode; 


    [Header("Performance and Status")] 
    public CarEngine CarEngine;
    public CarDial carDial; // Motor ve performans bilgileri
    public CarInterior carInterior; // İç mekan ve dış donanım bilgileri
    
}