using UnityEngine;

public class CarSteering
{
    private readonly CarData _carData;
    private readonly WheelCollider[] _wheelColliders;
    private readonly Transform _steeringWheel; // Direksiyon modeli
    private float _currentSteeringAngle; // Anlık direksiyon açısı
    private Quaternion _initialSteeringWheelRotation; // Direksiyonun başlangıç rotasyonu
    private const float MaxSteeringAngle = 900f; // 2.5 tur (900 derece)
    private const float SteeringSpeed = 150f; // Direksiyon dönüş hızı (derece/saniye)
    private const float WheelSteeringRatio = 30f; // Tekerlek dönüş oranı
    private const float ReturnToCenterSpeed = 100f; // Direksiyonun eski haline dönme hızı (derece/saniye)

    public CarSteering(CarData carData, WheelCollider[] wheelColliders, Transform steeringWheel)
    {
        _carData = carData;
        _wheelColliders = wheelColliders;
        _steeringWheel = steeringWheel;
        _initialSteeringWheelRotation = _steeringWheel.localRotation; // Başlangıç rotasyonunu kaydet
    }

    public void UpdateSteering(float steeringInput)
    {
        if (!_carData.isMotorRunning)
        {
            // Motor çalışmıyorsa direksiyon ve tekerlekler dönmesin
            _currentSteeringAngle = 0f;
            UpdateWheelSteering();
            UpdateSteeringWheelRotation();
            return;
        }

        if (_carData.currentSpeed > 0.1f) // Araç hareket ediyorsa
        {
            // Direksiyonu yavaşça eski haline getir
            _currentSteeringAngle = Mathf.MoveTowards(_currentSteeringAngle, 0f, ReturnToCenterSpeed * Time.deltaTime);
        }
        else // Araç duruyorsa
        {
            // Direksiyon açısını güncelle
            float targetSteeringAngle = steeringInput * MaxSteeringAngle;
            _currentSteeringAngle = Mathf.MoveTowards(_currentSteeringAngle, targetSteeringAngle, SteeringSpeed * Time.deltaTime);
        }

        // Tekerlekleri ve direksiyonu güncelle
        UpdateWheelSteering();
        UpdateSteeringWheelRotation();
    }

    private void UpdateWheelSteering()
    {
        foreach (var wheelCollider in _wheelColliders)
        {
            if (wheelCollider.transform.localPosition.z > 0) // Sadece ön tekerlekler
            {
                wheelCollider.steerAngle = _currentSteeringAngle / WheelSteeringRatio;
            }
        }
    }

    private void UpdateSteeringWheelRotation()
    {
        if (_steeringWheel != null)
        {
            _steeringWheel.localRotation = _initialSteeringWheelRotation * Quaternion.Euler(0, 0, -_currentSteeringAngle);
        }
    }
}