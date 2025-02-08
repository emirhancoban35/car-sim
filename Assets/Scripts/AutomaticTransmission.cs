using UnityEngine;

public class AutomaticTransmission : ITransmission
{
    private int _currentGear = 1;
    private readonly CarData _carData;

    // Vites değişimi için gerekli RPM değerleri
    private readonly float[] _gearUpRPM = { 0, 2500, 3000, 3500, 4000, 4500 };
    private readonly float[] _gearDownRPM = { 0, 1500, 2000, 2500, 3000, 3500 };
    private readonly float[] _maxSpeedPerGear = { 0, 30f, 60f, 100f, 140f, 180f, 220f };

    private const float KickdownThreshold = 0.8f; // %80 gaz seviyesinde kickdown aktif olur

    public AutomaticTransmission(CarData carData)
    {
        _carData = carData;
        _carData.gearStatus = (byte)_currentGear;
    }

    public void UpdateTransmission()
    {
        if (!_carData.isMotorRunning) return;

        float rpm = _carData.tachometer;
        float speed = _carData.currentSpeed;
        bool isGasPressed = _carData.isGasPressed;

        // Kickdown kontrolü (gaz pedalına tam basıldığında)
        if (isGasPressed && rpm > _gearUpRPM[_currentGear] * KickdownThreshold)
        {
            KickdownShift();
            return;
        }

        // Vites yükseltme (devir yüksekse)
        if (_currentGear < 6 && rpm >= _gearUpRPM[_currentGear] && speed >= _maxSpeedPerGear[_currentGear] * 0.8f)
        {
            ShiftUp();
        }
        
        // Vites düşürme (devir düşükse ve hız uygunsa)
        if (_currentGear > 1 && rpm <= _gearDownRPM[_currentGear] && speed <= _maxSpeedPerGear[_currentGear] * 0.7f)
        {
            ShiftDown();
        }
    }

    public void ShiftUp()
    {
        _currentGear++;
        _carData.gearStatus = (byte)_currentGear;
        Debug.Log($"Otomatik Vites Yükseltildi: {_currentGear}");
    }

    public void ShiftDown()
    {
        _currentGear--;
        _carData.gearStatus = (byte)_currentGear;
        Debug.Log($"Otomatik Vites Düşürüldü: {_currentGear}");
    }

    private void KickdownShift()
    {
        if (_currentGear > 1)
        {
            _currentGear--;
            _carData.gearStatus = (byte)_currentGear;
            Debug.Log($"Kickdown: Vites Düşürüldü -> {_currentGear}");
        }
    }

    public int GetCurrentGear() => _currentGear;
}
