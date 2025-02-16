using System;

public interface ICarDataProvider
{
    event Action<float> OnRpmChanged;
    event Action<float> OnSpeedChanged;
    event Action<float> OnTemperatureChanged;
    event Action<float> OnFuelChanged;
}