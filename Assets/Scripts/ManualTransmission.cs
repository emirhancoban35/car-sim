using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTransmission : ITransmission
{
    private int _currentGear = 0;
    private readonly CarData _carData;

    public ManualTransmission(CarData carData)
    {
        _carData = carData;
    }

    public void ShiftUp()
    {
        if (_carData.isClutchPressed)
        {
            _currentGear = Mathf.Min(_currentGear + 1, 6);
            _carData.gearStatus = (byte)_currentGear;
            Debug.Log($"Manuel Vites: {_currentGear}");
        }
        else
        {
            Debug.Log("Debriyaja basmadan vites değiştiremezsiniz!");
        }
    }

    public void ShiftDown()
    {
        if (_carData.isClutchPressed)
        {
            _currentGear = Mathf.Max(_currentGear - 1, -1);
            _carData.gearStatus = (byte)_currentGear;
            Debug.Log($"Manuel Vites: {_currentGear}");
        }
        else
        {
            Debug.Log("Debriyaja basmadan vites küçültemezsiniz!");
        }
    }

    public int GetCurrentGear() => _currentGear;
}
