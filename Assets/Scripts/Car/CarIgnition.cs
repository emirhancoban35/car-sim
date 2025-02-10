using UnityEngine;


public class CarIgnition
{
    private CarData _carData;
    private float _keyPressTime;
    private bool _isHoldingKey;
    
    public CarIgnition(CarData carData) => _carData = carData;
    
    public void HandleIgnition()
    {
        if (CarInputHandler.StartKeyDown())
        {
            if (!_carData.carInterior.isCarRunning)
            {
                _carData.carInterior.isCarRunning = true;
                Debug.Log("Araba çalıştı.");
            }
            else
            {
                _isHoldingKey = true;
                _keyPressTime = Time.time;
            }
        }
        
        if (CarInputHandler.StartKeyUp() && _isHoldingKey && _carData.carInterior.isMotorRunning == false)
        {
            _isHoldingKey = false;
            float heldDuration = Time.time - _keyPressTime;
            
            if (heldDuration >= 1.5f)
            {
                _carData.carInterior.isMotorRunning = true;
                Debug.Log("Motor çalıştı.");
            }
            else
            {
                Debug.Log("Kontak tam dönmedi!");
            }
        }
        
        if (CarInputHandler.StopKeyDown() && _carData.carInterior.isCarRunning)
        {
            _carData.carInterior.isCarRunning = false;
            _carData.carInterior.isMotorRunning = false;
            Debug.Log("Araba kapandı.");
        }
    }
}