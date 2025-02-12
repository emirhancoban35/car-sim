using System;
using UnityEngine;

public class CarDataProvider : MonoBehaviour, ICarDataProvider
{
    [SerializeField] private CarData carData;

    public event Action<float> OnRpmChanged;
    public event Action<float> OnSpeedChanged;
    public event Action<float> OnTemperatureChanged;
    public event Action<float> OnFuelChanged;

    private void Update()
    {
        OnRpmChanged?.Invoke(carData.carDial.tachometer);
        OnSpeedChanged?.Invoke(carData.carDial.currentSpeed);
        OnTemperatureChanged?.Invoke(carData.carDial.temperatureIndicator);
        OnFuelChanged?.Invoke(carData.carDial.fuelLevel);
    }
}