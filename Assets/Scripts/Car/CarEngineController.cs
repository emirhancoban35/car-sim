using UnityEngine;

public class CarEngineController
{
    private readonly CarData _carData;
    private float _stallTimer = 0f;
    
    private const float StallDelay = 3f;
    private const float MinRPMForStop = 500f;
    private const float AirResistance = 0.02f;
    private const float RollingResistance = 0.015f;
    
    public CarEngineController(CarData carData)
    {
        _carData = carData;
    }

    public void UpdateEngine()
    {
        if (!_carData.carInterior.isMotorRunning) return;

        UpdateTachometer();
        UpdateSpeed();
        CheckForEngineStop();
    }

    private void UpdateTachometer()
    {
        float targetRPM;
        
        if (_carData.isGasPressed)
        {
            _stallTimer = 0f;
            float accelerationFactor = _carData.CarEngine.horsepower * 50f;
            targetRPM = Mathf.Min(_carData.carDial.tachometer + accelerationFactor * Time.deltaTime, _carData.CarEngine.maxRPM);
        }
        else
        {
            targetRPM = Mathf.Lerp(_carData.carDial.tachometer, _carData.CarEngine.idleRPM, Time.deltaTime * 2);
        }

        _carData.carDial.tachometer = Mathf.Clamp(targetRPM, _carData.CarEngine.idleRPM, _carData.CarEngine.maxRPM);
    }

    private void UpdateSpeed()
    {
        if (_carData.transmissionMode == TransmissionMode.Neutral || _carData.transmissionMode == TransmissionMode.Park)
        {
            return; // N ve P modunda hız değişmez.
        }

        float engineTorque = _carData.CarEngine.maxTorque * (_carData.carDial.tachometer / _carData.CarEngine.maxRPM);
        float wheelForce = engineTorque * GetCurrentGearRatio() * 0.1f;
        float resistance = _carData.carDial.currentSpeed * (_carData.carDial.currentSpeed * AirResistance + RollingResistance);

        if (_carData.isGasPressed)
        {
            _carData.carDial.currentSpeed += wheelForce * Time.deltaTime;
        }
        else
        {
            _carData.carDial.currentSpeed -= resistance * Time.deltaTime;
        }

        _carData.carDial.currentSpeed = Mathf.Max(0, _carData.carDial.currentSpeed);
    }

    private void CheckForEngineStop()
    {
        if (_carData.carDial.tachometer < MinRPMForStop && !_carData.isClutchPressed)
        {
            _stallTimer += Time.deltaTime;
            if (_stallTimer >= StallDelay)
            {
                StopEngine("Düşük devirde stop etti!");
            }
        }
        else
        {
            _stallTimer = 0f;
        }
    }

    private void StopEngine(string reason)
    {
        _carData.carInterior.isMotorRunning = false;
        _carData.carDial.tachometer = 0;
        _carData.carDial.currentSpeed = 0;
        Debug.Log($"Motor stop etti: {reason}");
    }

    private float GetCurrentGearRatio()
    {
        if (_carData.isManual)
        {
            return _carData.gearStatus > 0 ? 3.0f / _carData.gearStatus : 0.0f;
        }
        else
        {
            return _carData.transmissionMode switch
            {
                TransmissionMode.Park => 0.0f,
                TransmissionMode.Reverse => -2.0f,
                TransmissionMode.Neutral => 0.0f,
                TransmissionMode.Drive => 2.5f,
                _ => 0.0f
            };
        }
    }
}
