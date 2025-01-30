using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Car Info")]
    [SerializeField] private string m_carName;
    [SerializeField] private uint m_carKm;
    [SerializeField] private string m_carYear;
    
    [Header("Car Dial")]
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _tachometer;
    [SerializeField] private float _fuelLevel;
    [SerializeField] private float _temperatureIndicator;
    
    [Header("Car Status")]
    [SerializeField] private bool _isHandbrakeApplied;
    [SerializeField] private bool _isCarRunning;
    [SerializeField] private byte _gearStatus;
    [SerializeField] private bool _isManual;
    
    [SerializeField] private List<Pedal> _pedals;
    [SerializeField] private CarEngine _engine;
    [SerializeField] private CarTransmission _transmission;
    [SerializeField] private CarHandbrake _handbrake;
    [SerializeField] private CarIgnition _ignition;
    
    private void Awake()
    {
        _engine = new CarEngine();
        _transmission = new CarTransmission();
        _handbrake = new CarHandbrake();
        _ignition = new CarIgnition();
        _pedals = new List<Pedal>
        {
            new Pedal(Pedals.Accelerator),
            new Pedal(Pedals.Brake),
            new Pedal(Pedals.Clutch)
        };
    }
}