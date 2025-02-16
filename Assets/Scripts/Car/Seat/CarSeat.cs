using UnityEngine;

public class CarSeat : MonoBehaviour
{
    public Transform seatPosition; // Koltuk pozisyonu
    private bool isOccupied = false;
    private GameObject occupant;

    public bool IsOccupied => isOccupied;
    public GameObject Occupant => occupant;

    public bool TrySit(GameObject character)
    {
        if (isOccupied) return false;

        isOccupied = true;
        occupant = character;
        return true;
    }

    public void ExitSeat()
    {
        isOccupied = false;
        occupant = null;
    }
}