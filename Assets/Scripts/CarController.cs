using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Components")]
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private CarInputHandler m_inputHandler;
    [SerializeField] private CarEngine m_engine;
    [SerializeField] private CarTransmission m_transmission;
    [SerializeField] private CarHandbrake m_handbrake;
    [SerializeField] private CarIgnition m_ignition;
    
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>(); 
    
        if (m_inputHandler == null)
            m_inputHandler = new CarInputHandler();

        if (m_engine == null)
            m_engine = new CarEngine();
    
        if (m_transmission == null)
            m_transmission = new CarTransmission();
    
        if (m_handbrake == null)
            m_handbrake = new CarHandbrake();
    
        if (m_ignition == null)
            m_ignition = new CarIgnition();
    }


    private void FixedUpdate()
    {
        if (!m_ignition.IsCarRunning) return;
        
        float throttle = m_inputHandler.GetThrottleInput();
        float brake = m_inputHandler.GetBrakeInput();
        float steering = m_inputHandler.GetSteeringInput();
        bool clutch = m_inputHandler.GetClutchInput();
        
        m_engine.ApplyThrottle(throttle);
        m_engine.ApplyBraking(brake);
        m_transmission.HandleGearShift(clutch);
        m_handbrake.HandleHandbrake(m_inputHandler.GetHandbrakeInput());
        
        ApplyMovement(steering);
    }

    private void ApplyMovement(float steering)
    {
        if (!m_handbrake.IsHandbrakeApplied)
        {
            m_rigidbody.AddForce(transform.forward * m_engine.CurrentSpeed, ForceMode.Acceleration);
            m_rigidbody.AddTorque(transform.up * steering, ForceMode.VelocityChange);
        }
    }
}