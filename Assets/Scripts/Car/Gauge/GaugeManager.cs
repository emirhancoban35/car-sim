using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    [SerializeField] private Gauge _rpmGauge;
    [SerializeField] private Gauge _speedGauge;
    [SerializeField] private Gauge _temperatureGauge;
    [SerializeField] private Gauge _fuelGauge;
    [SerializeField] private CarDataProvider _dataProvider;

    private void Awake()
    {
        _dataProvider.OnRpmChanged += _rpmGauge.UpdateNeedle;
        _dataProvider.OnSpeedChanged += _speedGauge.UpdateNeedle;
        _dataProvider.OnTemperatureChanged += _temperatureGauge.UpdateNeedle;
        _dataProvider.OnFuelChanged += _fuelGauge.UpdateNeedle;
    }

    private void OnDestroy()
    {
        if (_dataProvider == null) return;

        _dataProvider.OnRpmChanged -= _rpmGauge.UpdateNeedle;
        _dataProvider.OnSpeedChanged -= _speedGauge.UpdateNeedle;
        _dataProvider.OnTemperatureChanged -= _temperatureGauge.UpdateNeedle;
        _dataProvider.OnFuelChanged -= _fuelGauge.UpdateNeedle;
    }
}