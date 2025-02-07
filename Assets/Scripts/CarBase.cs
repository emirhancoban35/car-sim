using UnityEngine;

public class CarBase : MonoBehaviour
{
    public CarData carData;
    private CarIgnition _carIgnition;
    private CarPedal _carPedal;
    private CarTransmissionManager _carTransmissionManager;
    private CarDial _carDial;
    private CarSteering _carSteering;

    [Header("Steering")]
    [SerializeField] private Transform steeringWheel;
    [SerializeField] private WheelCollider[] frontWheels;

    private void Awake()
    {
        _carIgnition = new CarIgnition(carData);
        _carPedal = new CarPedal(carData);
        _carTransmissionManager = new CarTransmissionManager(carData);
        _carDial = new CarDial(carData);
        _carSteering = new CarSteering(carData, steeringWheel, frontWheels);

        _carTransmissionManager.GenerateTransmission();
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