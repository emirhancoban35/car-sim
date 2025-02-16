using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPedal
{
    private CarData _carData;
    
    public CarPedal(CarData carData) => _carData = carData;
    
    public void HandlePedals()
    {
        _carData.isClutchPressed = CarInputHandler.PressClutch();
        _carData.isBrakePressed = CarInputHandler.PressBrake();
        _carData.isGasPressed = CarInputHandler.PressGas();
    }
}
