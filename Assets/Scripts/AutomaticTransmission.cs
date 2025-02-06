using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTransmission : ITransmission
{
    private int _currentGear = 1;
    private readonly CarData _carData;

    public AutomaticTransmission(CarData carData)
    {
        _carData = carData;
    }

    public void ShiftUp()
    {
        if (_carData.currentSpeed > _currentGear * 20)
        {
            _currentGear = Mathf.Min(_currentGear + 1, 6);
            _carData.gearStatus = (byte)_currentGear;
            Debug.Log($"Otomatik Vites: {_currentGear}");
        }
    }

    public void ShiftDown()
    {
        if (_carData.currentSpeed < (_currentGear - 1) * 20)
        {
            _currentGear = Mathf.Max(_currentGear - 1, 1);
            _carData.gearStatus = (byte)_currentGear;
            Debug.Log($"Otomatik Vites: {_currentGear}");
        }
    }

    public int GetCurrentGear() => _currentGear;
}
