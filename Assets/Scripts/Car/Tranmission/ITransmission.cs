using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransmission
{
    void ShiftUp();
    void ShiftDown();
    int GetCurrentGear();
}
