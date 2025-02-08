using UnityEngine;

public class CarTransmissionManager
{
    private ITransmission _transmission;
    private CarData _carData;

    public CarTransmissionManager(CarData carData)
    {
        _carData = carData;
        GenerateTransmission();
    }

    public void GenerateTransmission()
    {
        _transmission = _carData.isManual ? new ManualTransmission(_carData) : new AutomaticTransmission(_carData);
    }

    public void UpdateTransmission()
    {
        if (!_carData.isManual)
        {
            ((AutomaticTransmission)_transmission).UpdateTransmission();
        }
        else
        {
            if (CarInputHandler.ShiftUpKey())
                _transmission.ShiftUp();
            
            if (CarInputHandler.ShiftDownKey())
                _transmission.ShiftDown();
        }

        // if (CarInputHandler.SetDriveKey()) _carData.transmissionMode = TransmissionMode.Drive;
        // if (CarInputHandler.SetNeutralKey()) _carData.transmissionMode = TransmissionMode.Neutral;
        // if (CarInputHandler.SetReverseKey()) _carData.transmissionMode = TransmissionMode.Reverse;
    }
}