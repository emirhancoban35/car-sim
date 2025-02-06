using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPedal : MonoBehaviour
{
    private CarData carData;
    
    public void Initialize(CarData data)
    {
        carData = data;
    }
    
    void Update()
    {
        carData.isClutchPressed = CarInputHandler.PressClutch();
        carData.isBrakePressed = CarInputHandler.PressBrake();
        carData.isGasPressed = CarInputHandler.PressGas();
    }
}
