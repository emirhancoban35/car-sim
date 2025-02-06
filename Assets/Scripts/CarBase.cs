using UnityEngine;

public class CarBase : MonoBehaviour
{
    public CarData carData;
    private CarIgnition _carIgnition;
    private CarPedal _carPedal;
    private CarTransmissionManager _carTransmissionManager;
    private CarDial _carDial;
    private CarSteering _carSteering;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider[] wheelColliders;

    [Header("Steering Wheel")]
    [SerializeField] private Transform steeringWheel; 

    private void Awake()
    {
        _carIgnition = new CarIgnition(carData);
        _carPedal = new CarPedal(carData);
        _carTransmissionManager = new CarTransmissionManager(carData);
        _carDial = new CarDial(carData);
        _carSteering = new CarSteering(carData, wheelColliders, steeringWheel);

        _carTransmissionManager.GenerateTransmission();
    }
    private void UpdateWheelTransforms()
    {
        foreach (var wheelCollider in wheelColliders)
        {
            wheelCollider.GetWorldPose(out var position, out var rotation);
            wheelCollider.transform.position = position;
            wheelCollider.transform.rotation = rotation;
        }
    }
    
    private void FixedUpdate()
    {
        if (!carData.isMotorRunning) return;

        float steeringInput = CarInputHandler.GetSteeringInput();
        _carSteering.UpdateSteering(steeringInput);

        UpdateWheelTransforms(); // Tekerlek modellerini güncelle
    }

    private void Update()
    {
        _carIgnition.HandleIgnition();
        _carPedal.HandlePedals();
        _carTransmissionManager.HandleTransmission();
        _carDial.UpdateDial();

        float steeringInput = CarInputHandler.GetSteeringInput();
        _carSteering.UpdateSteering(steeringInput);
    }
}