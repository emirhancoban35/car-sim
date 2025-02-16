using UnityEngine;
using System.Collections;

public class PlayerEnterVehicle : MonoBehaviour
{
    public Transform exitPosition; // Araçtan çıkış noktası
    private CharacterController characterController;
    private CarSeat currentSeat;
    private Transform originalParent;
    private Rigidbody rb;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryEnterVehicle();
        }
        else if (Input.GetKeyDown(KeyCode.F) && currentSeat != null)
        {
            ExitVehicle();
        }
    }

    private void TryEnterVehicle()
    {
        CarSeatsManager car = FindObjectOfType<CarSeatsManager>();
        if (car == null) return;

        CarSeat seat = car.GetAvailableSeat();
        if (seat == null) return;

        StartCoroutine(SmoothMoveToSeat(seat));
    }

    private IEnumerator SmoothMoveToSeat(CarSeat seat)
    {
        characterController.enabled = false;
        rb.isKinematic = true;
        originalParent = transform.parent;
        transform.parent = seat.transform;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Vector3 endPos = seat.seatPosition.position;
        Quaternion endRot = seat.seatPosition.rotation;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            transform.rotation = Quaternion.Lerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;
        currentSeat = seat;
        seat.TrySit(gameObject);
    }

    private void ExitVehicle()
    {
        if (currentSeat == null) return;

        transform.parent = originalParent;
        transform.position = exitPosition.position;
        characterController.enabled = true;
        rb.isKinematic = false;
        currentSeat.ExitSeat();
        currentSeat = null;
    }
}
