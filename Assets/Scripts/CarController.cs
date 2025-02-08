using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarData carData;
    private CarIgnition _carIgnition;
    private CarPedal _carPedal;
    private CarTransmissionManager _carTransmissionManager;
    private CarDial _carDial;
    private CarSteering _carSteering;
    private CarEngine _carEngine;

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
        _carEngine = new CarEngine(carData, frontWheels);

        _carTransmissionManager.GenerateTransmission();
    }

    private void Update()
    {
        _carIgnition.HandleIgnition();
        _carPedal.HandlePedals();
        _carTransmissionManager.UpdateTransmission();
        _carDial.UpdateDial();
        _carEngine.UpdateMovement();

        float steeringInput = CarInputHandler.GetSteeringInput();
        _carSteering.UpdateSteering(steeringInput);
    }
}