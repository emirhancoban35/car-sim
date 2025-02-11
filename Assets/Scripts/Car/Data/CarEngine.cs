using UnityEngine;

[System.Serializable]
public class CarEngine
{
    [Header("Engine Performance")]
    public float horsepower; // Beygir gücü
    public float maxTorque; // Tork
    public float engineCapacity; // Motor hacmi (litre)
    public float maxRPM; // Maksimum devir
    public float idleRPM = 800f; // Rölanti devri
    public float fuelConsumption; // Yakıt tüketimi
    
    [Header("Brake and Suspension")] 
    public float brakePower; // Fren gücü (kN)
    public float suspensionStiffness; // Süspansiyon sertliği (0-1)
  
}