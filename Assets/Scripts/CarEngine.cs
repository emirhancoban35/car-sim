using UnityEngine;

public class CarEngine
{
    private readonly CarData _carData;
    private readonly WheelCollider[] _wheelColliders;
    
    private const float MaxTorque = 500f; // Motor torku
    private const float BrakeForce = 6000f; // Frenleme gücü
    private const float IdleRPM = 900f; // Rölanti devri
    private const float MinRPMForStop = 500f; // Stop etme deviri
    
    public CarEngine(CarData carData, WheelCollider[] wheelColliders)
    {
        _carData = carData;
        _wheelColliders = wheelColliders;
    }
    
    public void UpdateMovement()
    {
        if (!_carData.isMotorRunning)
        {
            ApplyBrakes(BrakeForce * 0.5f);
            return;
        }
        
        float motorTorque = CalculateMotorTorque();
        float brakeForce = _carData.isBrakePressed ? BrakeForce : 0f;
        
        ApplyTorque(motorTorque);
        ApplyBrakes(brakeForce);
    }
    
    private float CalculateMotorTorque()
    {
        if (!_carData.isGasPressed) return 0f;
        
        float gearRatio = _carData.isManual ? GetGearRatioManual() : GetGearRatioAuto();
        float torque = (_carData.tachometer / 1000f) * MaxTorque * gearRatio;
        
        return Mathf.Clamp(torque, 0, MaxTorque);
    }
    
    private float GetGearRatioManual()
    {
        return 1f - (_carData.gearStatus * 0.1f);
    }
    
    private float GetGearRatioAuto()
    {
        return 1f - (_carData.currentSpeed / 200f);
    }
    
    private void ApplyTorque(float torque)
    {
        foreach (var wheel in _wheelColliders)
        {
            wheel.motorTorque = torque;
        }
    }
    
    private void ApplyBrakes(float force)
    {
        foreach (var wheel in _wheelColliders)
        {
            wheel.brakeTorque = force;
        }
    }
}