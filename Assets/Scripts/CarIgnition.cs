using UnityEngine;


public class CarIgnition : MonoBehaviour
{
    private CarData carData;
    private float keyPressTime;
    private bool isHoldingKey;
    
    public void Initialize(CarData data)
    {
        carData = data;
    }

    void Update()
    {
        HandleIgnition();
    }
    
    private void HandleIgnition()
    {
        if (CarInputHandler.StartKeyDown())
        {
            if (!carData.isCarRunning)
            {
                carData.isCarRunning = true;
                Debug.Log("Araba çalıştı.");
            }
            else
            {
                isHoldingKey = true;
                keyPressTime = Time.time;
            }
        }
        
        if (CarInputHandler.StartKeyUp() && isHoldingKey)
        {
            isHoldingKey = false;
            float heldDuration = Time.time - keyPressTime;
            
            if (heldDuration >= 2.5f)
            {
                carData.isMotorRunning = true;
                Debug.Log("Motor çalıştı.");
            }
            else
            {
                Debug.Log("Kontak tam dönmedi!");
            }
        }
        
        if (CarInputHandler.StopKeyDown() && carData.isCarRunning)
        {
            carData.isCarRunning = false;
            carData.isMotorRunning = false;
            Debug.Log("Araba kapandı.");
        }
    }
}