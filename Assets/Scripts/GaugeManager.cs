using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    [SerializeField] private CarController _carController;
    
    [Header("Göstergeler")]
    [SerializeField] private Gauge _rpmGauge;
    [SerializeField] private Gauge _speedGauge;
    [SerializeField] private Gauge _temperatureGauge;
    [SerializeField] private Gauge _fuelGauge;

    private void Awake()
    {
        _carController.OnRpmChanged += _rpmGauge.UpdateNeedle;
        _carController.OnSpeedChanged += _speedGauge.UpdateNeedle;
        _carController.OnTemperatureChanged += _temperatureGauge.UpdateNeedle;
        _carController.OnFuelChanged += _fuelGauge.UpdateNeedle;
    }

    private void OnDestroy()
    {
        _carController.OnRpmChanged -= _rpmGauge.UpdateNeedle;
        _carController.OnSpeedChanged -= _speedGauge.UpdateNeedle;
        _carController.OnTemperatureChanged -= _temperatureGauge.UpdateNeedle;
        _carController.OnFuelChanged -= _fuelGauge.UpdateNeedle;
    }
}
