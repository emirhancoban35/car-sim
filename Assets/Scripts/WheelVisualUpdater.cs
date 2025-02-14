using UnityEngine;

public class WheelVisualUpdater : MonoBehaviour
{
    [SerializeField] private WheelCollider wheelCollider;
    [SerializeField] private Transform wheelVisual;
    [SerializeField] private Vector3 rotationOffset = new Vector3(0, 90, 0);

    private void FixedUpdate()
    {
        UpdateWheelVisual();
    }

    private void UpdateWheelVisual()
    {
        if (wheelCollider == null || wheelVisual == null)
            return;
        
        Vector3 position;
        Quaternion rotation;
        
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelVisual.position = position;
        wheelVisual.rotation = rotation * Quaternion.Euler(rotationOffset);
    }
}
