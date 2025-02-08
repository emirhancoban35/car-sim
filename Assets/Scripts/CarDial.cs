using UnityEngine;

public class CarDial
{
    private readonly CarData _carData;
    private readonly float _idleRPM = 800f;
    private readonly float _maxRPM = 7000f;
    private readonly float _minRPMForStop = 500f;
    private readonly float _gearRatioFactor = 0.8f;
    private readonly float _accelerationRate = 3000f;
    private readonly float _decelerationRate = 2000f;
    private readonly float _brakeDeceleration = 1500f;
    private readonly float _overheatThreshold = 120f;
    private readonly float _heatIncreaseRate = 0.5f;
    private readonly float _heatDecreaseRate = 0.2f;

    public CarDial(CarData carData)
    {
        _carData = carData;
    }

    public void UpdateDial()
    {
        if (_carData.isMotorRunning)
        {
            UpdateTachometer();
            UpdateSpeedometer();
            UpdateTemperature();
            CheckForEngineStop();
        }
    }

    private void UpdateTachometer()
    {
        if (!_carData.isMotorRunning)
        {
            _carData.tachometer = 0;
            return;
        }

        float targetRPM;

        if (_carData.isGasPressed)
        {
            if (_carData.gearStatus == 0) // Boş viteste gaz veriliyorsa
            {
                targetRPM = Mathf.Min(_carData.tachometer + (_accelerationRate * Time.deltaTime), _maxRPM);
            }
            else // Vitesteyken
            {
                targetRPM = Mathf.Lerp(_carData.tachometer, _carData.currentSpeed * _carData.gearStatus * _gearRatioFactor, Time.deltaTime * 3);
            }
        }
        else // Gaz verilmezse
        {
            if (_carData.gearStatus == 0) // Boş viteste rölantiye düşmeli
            {
                targetRPM = Mathf.Max(_carData.tachometer - (_decelerationRate * Time.deltaTime), _idleRPM);
            }
            else // Vitesteyken gaz kesilirse motor yavaşlamalı ama rölanti altına düşmemeli
            {
                targetRPM = Mathf.Lerp(_carData.tachometer, _idleRPM + (_carData.currentSpeed * _gearRatioFactor), Time.deltaTime * 2);
            }
        }

        // Devir sınırlandırma
        _carData.tachometer = Mathf.Clamp(targetRPM, _idleRPM, _maxRPM);
    }


    private void UpdateSpeedometer()
    {
        if (_carData.isGasPressed && _carData.isMotorRunning)
        {
            _carData.currentSpeed += (_carData.tachometer / 1000f) * Time.deltaTime * 2;
        }

        if (_carData.isBrakePressed)
        {
            _carData.currentSpeed -= 40f * Time.deltaTime;
        }

        _carData.currentSpeed = Mathf.Clamp(_carData.currentSpeed, 0f, GetMaxSpeedForGear(_carData.gearStatus));
    }

    private void UpdateTemperature()
    {
        if (_carData.isGasPressed)
        {
            _carData.temperatureIndicator += _heatIncreaseRate * Time.deltaTime;
        }
        else
        {
            _carData.temperatureIndicator -= _heatDecreaseRate * Time.deltaTime;
        }

        _carData.temperatureIndicator = Mathf.Clamp(_carData.temperatureIndicator, 20f, _overheatThreshold);
    }

    private void CheckForEngineStop()
    {
        if (_carData.isTestMode) return;

        if (_carData.fuelLevel <= 0)
        {
            StopEngine("Yakıt bitti!");
            return;
        }

        if (_carData.isManual && _carData.gearStatus > 0 && _carData.tachometer < _minRPMForStop && !_carData.isClutchPressed)
        {
            StopEngine("Düşük devirde motor stop etti!");
            return;
        }

        if (_carData.temperatureIndicator >= _overheatThreshold)
        {
            StopEngine("Motor hararet yaptı!");
            return;
        }
    }

    private void StopEngine(string reason)
    {
        _carData.isMotorRunning = false;
        _carData.tachometer = 0;
        Debug.Log($"Motor stop etti: {reason}");
    }

    private float GetMaxSpeedForGear(int gear)
    {
        switch (gear)
        {
            case 1: return 40f;
            case 2: return 70f;
            case 3: return 110f;
            case 4: return 150f;
            case 5: return 200f;
            case 6: return 250f;
            default: return 0f;
        }
    }
}