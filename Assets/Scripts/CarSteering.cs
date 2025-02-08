using UnityEngine;

public class CarSteering
{
    private readonly CarData _carData;
    private readonly Transform _steeringWheel;
    private readonly WheelCollider[] _frontWheels;

    private float _currentSteeringAngle;
    private Quaternion _initialSteeringWheelRotation;
    private float _steeringVelocity = 0f; // SmoothDamp için

    private const float MaxSteeringAngle = 35f; // Maksimum tekerlek dönüş açısı
    private const float SteeringSpeed = 2f; // Direksiyon dönüş hızını düşürdük
    private const float ReturnToCenterSpeed = 1.5f; // Direksiyonun ortalanma hızı daha yavaş
    private const float WheelSteeringRatio = 1.8f; // Direksiyon açısını tekerleklere oranla düşürme
    private const float SteeringSmoothTime = 0.2f; // Direksiyonun akıcı hareket etmesi için

    public CarSteering(CarData carData, Transform steeringWheel, WheelCollider[] frontWheels)
    {
        _carData = carData;
        _steeringWheel = steeringWheel;
        _frontWheels = frontWheels;
        
        _initialSteeringWheelRotation = _steeringWheel.localRotation;
    }

    public void UpdateSteering(float steeringInput)
    {
        // Direksiyonun hedef açısını hesapla
        float targetSteeringAngle = steeringInput * MaxSteeringAngle;

        // Direksiyonun dönüşünü yavaşlat ve akıcı hale getir
        _currentSteeringAngle = Mathf.SmoothDamp(_currentSteeringAngle, targetSteeringAngle, ref _steeringVelocity, SteeringSmoothTime);

        // Eğer direksiyon bırakılmışsa (çok küçük bir input varsa), yavaşça sıfıra dönmeli
        if (Mathf.Abs(steeringInput) < 0.05f)
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
            _steeringWheel.localRotation = _initialSteeringWheelRotation * Quaternion.Euler(0, 0, _currentSteeringAngle * 10);
        }
    }
}
