using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float openAngle = -90f;
    public float smoothSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private Coroutine doorCoroutine;

    private Transform parentObject;

    void Start()
    {
        parentObject = transform.parent;
        closedRotation = parentObject.rotation;
        openRotation = Quaternion.Euler(parentObject.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void OnMouseDown()
    {
        ToggleDoor();
    }

    void ToggleDoor()
    {
        if (doorCoroutine != null)
        {
            StopCoroutine(doorCoroutine);
        }

        isOpen = !isOpen;
        doorCoroutine = StartCoroutine(MoveDoor(isOpen ? openRotation : closedRotation));
    }

    IEnumerator MoveDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(parentObject.rotation, targetRotation) > 0.1f)
        {
            parentObject.rotation = Quaternion.Lerp(parentObject.rotation, targetRotation, smoothSpeed * Time.deltaTime);
            yield return null;
        }

        parentObject.rotation = targetRotation;
    }
}