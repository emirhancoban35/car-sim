using UnityEngine;

public class CarDial
{
    private readonly CarData _carData;
<<<<<<< Updated upstream
    private readonly float _idleRPM = 800f; // Rölanti devri
    private readonly float _maxRPM = 7000f; // Kesici devre noktası
    private readonly float _minRPMForStop = 500f; // Stop etme eşiği
    private readonly float _gearRatioFactor = 0.8f; // Vites arttıkça RPM düşüş oranı
    private readonly float _accelerationRate = 3000f; // Boştayken RPM artış hızı
    private readonly float _decelerationRate = 2000f; // Boştayken RPM düşüş hızı
    private readonly float _brakeDeceleration = 1500f; // Frenleme RPM düşüşü
=======
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
    private float _stallTimer = 0f;  // Stop etme için zamanlayıcı
    private float _stallDelay = 3f;  // Kaç saniye sonra stop etsin?
>>>>>>> Stashed changes

    public CarDial(CarData carData)
    {
        _carData = carData;
        ResetCarState(); // Oyun başladığında sıfırla
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

    private void ResetCarState()
    {
        _carData.tachometer = 0;
        _carData.currentSpeed = 0;
        _carData.isMotorRunning = false; // Oyun başında motor kapalı
    }

    private void UpdateTachometer()
    {
        if (_carData.isGasPressed)
        {
<<<<<<< Updated upstream
            if (_carData.gearStatus == 0)
=======
            _stallTimer = 0f; // Gaz verildiğinde stop sayacını sıfırla

            if (_carData.gearStatus == 0) // Boş viteste gaz veriliyorsa
>>>>>>> Stashed changes
            {
                _carData.tachometer += _accelerationRate * Time.deltaTime;
            }
            else
            {
                float targetRPM = _carData.currentSpeed * _carData.gearStatus * _gearRatioFactor;
                _carData.tachometer = Mathf.Lerp(_carData.tachometer, targetRPM, Time.deltaTime * 3);
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
                _carData.tachometer -= 700f * Time.deltaTime;
            }
        }

        if (_carData.tachometer < _idleRPM && _carData.gearStatus == 0)
        {
            _carData.tachometer = _idleRPM;
        }

<<<<<<< Updated upstream
        if (_carData.tachometer < _minRPMForStop && _carData.gearStatus > 0 && !_carData.isClutchPressed)
        {
            StopEngine();
        }

        _carData.tachometer = Mathf.Clamp(_carData.tachometer, 0, _maxRPM);
    }

=======
>>>>>>> Stashed changes
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

    private void CheckForEngineStop()
    {
        if (_carData.isClutchPressed) return;

        if (_carData.gearStatus > 0)
        {
            if (_carData.tachometer < _minRPMForStop && !_carData.isGasPressed && !_carData.isClutchPressed)
            {
                // StopEngine();
            }
        }
        else
        {
<<<<<<< Updated upstream
            if (_carData.fuelLevel <= 0)
            {
                // StopEngine();
            }
=======
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

        if (_carData.temperatureIndicator >= _overheatThreshold)
        {
            StopEngine("Motor hararet yaptı!");
            return;
>>>>>>> Stashed changes
        }

        // *Gerçekçi manuel vites stop etme*
        if (_carData.isManual && _carData.gearStatus > 0 && !_carData.isClutchPressed)
        {
            if (_carData.tachometer < _minRPMForStop) // Düşük devirdeyse
            {
                _stallTimer += Time.deltaTime;

                if (_stallTimer >= _stallDelay) // 3 saniye bekle, stop et
                {
                    StopEngine("Düşük devirde motor stop etti!");
                    return;
                }
            }
        }
        else
        {
            _stallTimer = 0f; // Debriyaja basılınca sayacı sıfırla
        }

        // *Hareketsiz halde stop etme*
        if (_carData.isManual && _carData.gearStatus > 0 && !_carData.isGasPressed && _carData.currentSpeed < 0.1f)
        {
            _stallTimer += Time.deltaTime;

            if (_stallTimer >= _stallDelay)
            {
                StopEngine("Hareketsiz kaldığı için motor stop etti!");
                return;
            }
        }
        else
        {
            _stallTimer = 0f; // Araç hareket ederse veya gaz verilirse sayaç sıfırla
        }
    }

    private void StopEngine()
    {
        _carData.isMotorRunning = false;
        _carData.tachometer = 0;
<<<<<<< Updated upstream
        Debug.Log("Motor stop etti!");
=======
        _carData.currentSpeed = 0; // Stop ettiğinde hız da sıfırlansın
        Debug.Log($"Motor stop etti: {reason}");
>>>>>>> Stashed changes
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
