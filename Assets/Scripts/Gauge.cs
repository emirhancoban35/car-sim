using UnityEngine;

public class Gauge : MonoBehaviour
{
    [SerializeField] private Transform _needle;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    
    [Header("Açı Aralığı")]
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    private float _currentValue = 0f;

    public void UpdateNeedle(float value)
    {
        _currentValue = Mathf.Lerp(_currentValue, value, Time.deltaTime * 5f); // Yumuşak geçiş
        float normalizedValue = Mathf.InverseLerp(_minValue, _maxValue, _currentValue);
        float angle = Mathf.Lerp(_minAngle, _maxAngle, normalizedValue);
        _needle.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
