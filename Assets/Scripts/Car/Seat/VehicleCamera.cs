using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform vehicle;
    public float smoothSpeed = 2f;

    private bool isInside = false;

    private void Update()
    {
        if (!isInside) return;

        Quaternion targetRotation = Quaternion.LookRotation(vehicle.forward);
        playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }

    public void EnterVehicle(Transform vehicleTransform)
    {
        vehicle = vehicleTransform;
        isInside = true;
    }

    public void ExitVehicle()
    {
        isInside = false;
    }
}