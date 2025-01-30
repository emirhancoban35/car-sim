using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarTransmission
{
    private int _currentGear;
    private readonly int[] _gearRatios = { 0, 1, 2, 3, 4, 5 };

    public void HandleGearShift(bool isClutchPressed)
    {
        if (isClutchPressed)
        {
            if (Input.GetKeyDown(KeyCode.E))
                ShiftUp();
            if (Input.GetKeyDown(KeyCode.Q))
                ShiftDown();
        }
    }

    private void ShiftUp()
    {
        if (_currentGear < _gearRatios.Length - 1)
            _currentGear++;
    }

    private void ShiftDown()
    {
        if (_currentGear > 0)
            _currentGear--;
    }
}