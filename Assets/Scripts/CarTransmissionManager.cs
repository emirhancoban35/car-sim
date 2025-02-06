using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTransmissionManager
{
    private ITransmission _transmission;
    private CarData _carData;
    
    public CarTransmissionManager(CarData carData) => _carData = carData;


    public void GenerateTransmission()
    {
        _transmission = _carData.isManual ? new ManualTransmission(_carData) : new AutomaticTransmission(_carData);
    }

    public void HandleTransmission()
    {
        if (CarInputHandler.ShiftUpKey())
        {
            _transmission.ShiftUp();
        }
           
        
        if (CarInputHandler.ShiftDownKey())
            _transmission.ShiftDown();
    }
}
