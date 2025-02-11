using UnityEngine;

[System.Serializable]
public class CarInterior
{
    [Header("Car Status")] 
    public bool isHandbrakeApplied;
    public bool isCarRunning;
    public bool isMotorRunning;
    
    [Header("Doors And Interior")]
    public bool isDoorOpen; // Kapılar açık mı?
    public bool isInteriorLightOn; // İç ışık açık mı?
    public bool isACOn; // Klima açık mı?
    public float acTemperature; // Klima sıcaklık ayarı (°C)
}