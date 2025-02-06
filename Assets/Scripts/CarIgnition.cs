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
            if (!_carData.isCarRunning)
            {
                _carData.isCarRunning = true;
                Debug.Log("Araba çalıştı.");
            }
            else
            {
                _isHoldingKey = true;
                _keyPressTime = Time.time;
            }
        }
        
        if (CarInputHandler.StartKeyUp() && _isHoldingKey && _carData.isMotorRunning == false)
        {
            _isHoldingKey = false;
            float heldDuration = Time.time - _keyPressTime;
            
            if (heldDuration >= 1.5f)
            {
                _carData.isMotorRunning = true;
                Debug.Log("Motor çalıştı.");
            }
            else
            {
                Debug.Log("Kontak tam dönmedi!");
            }
        }
        
        if (CarInputHandler.StopKeyDown() && _carData.isCarRunning)
        {
            _carData.isCarRunning = false;
            _carData.isMotorRunning = false;
            Debug.Log("Araba kapandı.");
        }
    }
}