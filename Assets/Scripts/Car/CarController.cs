using System;
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

    [Header("Steering")]
    [SerializeField] private Transform steeringWheel;
    [SerializeField] private WheelCollider[] frontWheels;

    private void Awake()
    {
        _carIgnition = new CarIgnition(carData);
        _carPedal = new CarPedal(carData);
        _carTransmissionManager = new CarTransmissionManager(carData);
        _carSteering = new CarSteering(carData, steeringWheel, frontWheels);
        _carEngineController = new CarEngineController(carData);
        _carMovementController = new CarMovementController(carData , frontWheels);

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

    //Esgal Kodları Başladı
    //using System; ben ekledim
    [SerializeField] private Rigidbody _rigidbody;
    public CarDial carDial = new CarDial();

    public event Action<float> OnRpmChanged;
    public event Action<float> OnSpeedChanged;
    public event Action<float> OnTemperatureChanged;
    public event Action<float> OnFuelChanged;

    private void FixedUpdate()
    {
        UpdateCarStats();
        NotifyGaugeUpdates();
    }

    private void UpdateCarStats()
    {
        float speedKmh = _rigidbody.velocity.magnitude * 3.6f; // m/s -> km/h dönüşümü
        carDial.currentSpeed = Mathf.Lerp(carDial.currentSpeed, speedKmh, Time.deltaTime * 5f);

        // Devir sayacı (RPM) hesaplama
        float maxRPM = 8000f;
        carDial.tachometer = Mathf.Lerp(carDial.tachometer, Mathf.Clamp(speedKmh * 30, 0, maxRPM), Time.deltaTime * 3f);

        // Motor sıcaklığı yavaş yavaş artar
        carDial.temperatureIndicator = Mathf.Lerp(carDial.temperatureIndicator, 90f, Time.deltaTime * 0.1f);

        // Yakıt seviyesi azalmaya devam eder
        carDial.fuelLevel = Mathf.Max(0, carDial.fuelLevel - Time.deltaTime * 0.001f);
    }

    private void NotifyGaugeUpdates()
    {
        OnRpmChanged?.Invoke(carDial.tachometer);
        OnSpeedChanged?.Invoke(carDial.currentSpeed);
        OnTemperatureChanged?.Invoke(carDial.temperatureIndicator);
        OnFuelChanged?.Invoke(carDial.fuelLevel);
    }

    //Esgal Kodları Bitti
}