using UnityEngine;

public class CarDial
{
    private readonly CarData _carData;
    private readonly float _idleRPM = 800f; // Rölanti devri
    private readonly float _maxRPM = 7000f; // Kesici devre noktası
    private readonly float _minRPMForStop = 500f; // Stop etme eşiği
    private readonly float _gearRatioFactor = 0.8f; // Vites arttıkça RPM düşüş oranı
    private readonly float _accelerationRate = 2500f; // Boştayken RPM artış hızı
    private readonly float _decelerationRate = 1500f; // Boştayken RPM düşüş hızı
    private readonly float _brakeDeceleration = 1000f; // Frenleme RPM düşüşü

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
            CheckForEngineStop();
        }
    }

    private void UpdateTachometer()
    {
        if (_carData.isGasPressed)
        {
            if (_carData.gearStatus == 0)
            {
                _carData.tachometer += _accelerationRate * Time.deltaTime;
            }
            else 
            {
                float targetRPM = _carData.currentSpeed * _carData.gearStatus * _gearRatioFactor;
                _carData.tachometer = Mathf.Lerp(_carData.tachometer, targetRPM, Time.deltaTime * 2);
            }
        }
        else
        {
            if (_carData.gearStatus == 0) 
            {
                _carData.tachometer -= _decelerationRate * Time.deltaTime;
            }
            else if (!_carData.isClutchPressed) 
            {
                _carData.tachometer -= _brakeDeceleration * Time.deltaTime;
            }

            if (_carData.gearStatus > 0 && !_carData.isGasPressed && !_carData.isClutchPressed)
            {
                _carData.tachometer -= 500f * Time.deltaTime; 
            }
        
            if (_carData.tachometer < _idleRPM && _carData.gearStatus == 0)
            {
                _carData.tachometer = _idleRPM; 
            }
        }

        if (_carData.tachometer < _minRPMForStop && _carData.gearStatus > 0)
        {
            StopEngine();
        }

        _carData.tachometer = Mathf.Clamp(_carData.tachometer, 0, _maxRPM);
    }
    private void UpdateSpeedometer()
    {
        if (_carData.isGasPressed && _carData.isMotorRunning)
        {
            _carData.currentSpeed += (_carData.tachometer / 1000f) * Time.deltaTime;
        }

        if (_carData.isBrakePressed)
        {
            _carData.currentSpeed -= 30f * Time.deltaTime;
        }

        _carData.currentSpeed = Mathf.Clamp(_carData.currentSpeed, 0f, GetMaxSpeedForGear(_carData.gearStatus));
    }

    private void CheckForEngineStop()
    {
        if (_carData.isClutchPressed) return;
        
        if (_carData.gearStatus > 0) 
        {
            if (_carData.tachometer < _minRPMForStop && !_carData.isGasPressed && !_carData.isClutchPressed)
            {
                StopEngine();
            }
        }
        else 
        {
            if (_carData.fuelLevel <= 0)
            {
                StopEngine();
            }
        }
    }


    private void StopEngine()
    {
        _carData.isMotorRunning = false;
        _carData.tachometer = 0;
        Debug.Log("Motor stop etti!");
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
