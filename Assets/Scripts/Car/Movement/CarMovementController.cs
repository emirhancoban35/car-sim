using UnityEngine;

public class CarMovementController
{
    private readonly CarData _carData;
    private readonly WheelCollider[] _wheelColliders;

    private const float MaxTorque = 500f;
    private const float BrakeForce = 6000f;
    private const float ReverseTorqueMultiplier = 0.5f;
    private const float ClutchEngagementThreshold = 0.1f;

    public CarMovementController(CarData carData, WheelCollider[] wheelColliders)
    {
        _carData = carData;
        _wheelColliders = wheelColliders;
    }
    
    public void UpdateMovement()
    {
        if (!_carData.carInterior.isMotorRunning)
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

        if (_carData.isClutchPressed)
        {
            return 0f; // Debriyaja basılıysa motor tekerleklere güç vermez
        }

        if (_carData.isManual)
        {
            if (_carData.gearStatus == 255) // Geri vites
                return -MaxTorque * ReverseTorqueMultiplier;
            if (_carData.gearStatus > 0) // İleri vitesler
                return MaxTorque * (1f - (_carData.gearStatus * 0.1f));
        }
        else
        {
            if (_carData.transmissionMode == TransmissionMode.Reverse)
                return -MaxTorque * ReverseTorqueMultiplier;
            if (_carData.transmissionMode == TransmissionMode.Drive)
                return MaxTorque * (1f - (_carData.carDial.currentSpeed / 200f));
        }

        return 0f;
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