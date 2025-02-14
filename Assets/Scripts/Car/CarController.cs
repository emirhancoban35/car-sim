using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarData carData;
    private CarIgnition _carIgnition;
    private CarPedal _carPedal;
    private CarTransmissionManager _carTransmissionManager;
    private CarMovementController _carMovementController;
    private CarSteering _carSteering;
    private CarEngineController _carEngineController;

    private Rigidbody _carRigidBody;

    [Header("Steering")]
    [SerializeField] private Transform steeringWheel;
    [SerializeField] private WheelCollider[] frontWheels;

    private void Awake()
    {
        _carRigidBody = gameObject.GetComponent<Rigidbody>();
        _carIgnition = new CarIgnition(carData);
        _carPedal = new CarPedal(carData);
        _carTransmissionManager = new CarTransmissionManager(carData);
        _carSteering = new CarSteering(carData, steeringWheel, frontWheels);
        _carEngineController = new CarEngineController(carData , _carRigidBody);
        _carMovementController = new CarMovementController(carData, frontWheels);

        _carTransmissionManager.GenerateTransmission();
    }

    private void Update()
    {
        _carIgnition.HandleIgnition();
        _carPedal.HandlePedals();
        _carTransmissionManager.UpdateTransmission();
        _carEngineController.UpdateEngine();
        _carMovementController.UpdateMovement();

        float steeringInput = CarInputHandler.GetSteeringInput();
        _carSteering.UpdateSteering(steeringInput);
    }
}