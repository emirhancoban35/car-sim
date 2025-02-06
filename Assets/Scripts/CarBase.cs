using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarBase : MonoBehaviour
{
    public CarData carData;
    private CarIgnition _carIgnition;
    private CarPedal _carPedal;
    private CarTransmissionManager _carTransmissionManager;
    private CarDial _carDial;

    private void Awake()
    {
        _carIgnition = new CarIgnition(carData);
        _carPedal = new CarPedal(carData);
        _carTransmissionManager = new CarTransmissionManager(carData);
        _carDial = new CarDial(carData);
        
        _carTransmissionManager.GenerateTransmission();
    }

    private void Update()
    {
        _carIgnition.HandleIgnition();
        _carPedal.HandlePedals();
        _carTransmissionManager.HandleTransmission();
        _carDial.UpdateDial();
    }
}

