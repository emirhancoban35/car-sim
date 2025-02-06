using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBase : MonoBehaviour
{
    public CarData carData;
    private CarIgnition _carIgnition;
    private CarPedal _carPedal;

    private void Start()
    {
        _carIgnition = gameObject.AddComponent<CarIgnition>();
        _carPedal = gameObject.AddComponent<CarPedal>();
        
        _carIgnition.Initialize(carData);
        _carPedal.Initialize(carData);
    }
}
