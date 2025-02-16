using UnityEngine;
using System.Collections.Generic;

public class CarSeatsManager : MonoBehaviour
{
    private List<CarSeat> seats = new List<CarSeat>();

    private void Awake()
    {
        seats.AddRange(GetComponentsInChildren<CarSeat>());
    }

    public CarSeat GetAvailableSeat()
    {
        foreach (var seat in seats)
        {
            if (!seat.IsOccupied)
                return seat;
        }
        return null;
    }
}