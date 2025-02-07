using UnityEngine;

public class CarSteering
{
    private readonly CarData _carData;
    private readonly Transform _steeringWheel;
    private readonly WheelCollider[] _frontWheels;
    
    private float _currentSteeringAngle;
    private Quaternion _initialSteeringWheelRotation;
    
    private const float MaxSteeringAngle = 35f; // Maksimum tekerlek dönüş açısı
    private const float SteeringSpeed = 75f; // Direksiyon dönüş hızı
    private const float ReturnToCenterSpeed = 80f; // Direksiyonun ortalanma hızı
    private const float WheelSteeringRatio = 1.5f; // Direksiyon açısını tekerleklere oranla düşürme

    public CarSteering(CarData carData, Transform steeringWheel, WheelCollider[] frontWheels)
    {
        _carData = carData;
        _steeringWheel = steeringWheel;
        _frontWheels = frontWheels;
        
        _initialSteeringWheelRotation = _steeringWheel.localRotation;
    }

    public void UpdateSteering(float steeringInput)
    {
        // Direksiyon açısını hesapla
        float targetSteeringAngle = steeringInput * MaxSteeringAngle;
        _currentSteeringAngle = Mathf.Lerp(_currentSteeringAngle, targetSteeringAngle, SteeringSpeed * Time.deltaTime);

        // Direksiyonu ortalama mantığı
        if (Mathf.Abs(steeringInput) < 0.1f)
        {
            _currentSteeringAngle = Mathf.Lerp(_currentSteeringAngle, 0, ReturnToCenterSpeed * Time.deltaTime);
        }

        UpdateWheelSteering();
        UpdateSteeringWheelRotation();
    }

    private void UpdateWheelSteering()
    {
        foreach (var wheel in _frontWheels)
        {
            wheel.steerAngle = _currentSteeringAngle / WheelSteeringRatio;
        }
    }

    private void UpdateSteeringWheelRotation()
    {
        if (_steeringWheel != null)
        {
            _steeringWheel.localRotation = _initialSteeringWheelRotation * Quaternion.Euler(0, 0, -_currentSteeringAngle * 10);
        }
    }
}