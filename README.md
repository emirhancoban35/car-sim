# Car Sim

A car simulator prototype built in Unity, with driving controls, manual and automatic transmission, vehicle entry and an interactive cockpit.

The project focuses on the systems behind operating a car: turning on the ignition, starting the engine, using the clutch, changing gears and reading the dashboard.

## Vehicle Systems

- **Driving:** WheelCollider-based acceleration, steering, braking and reverse.
- **Transmission:** Manual gear changes require the clutch. Automatic transmission supports drive and reverse.
- **Ignition:** Separate electrical and engine states, with a hold-and-release interaction to start the engine.
- **Dashboard:** Speed, RPM, fuel and temperature gauges updated through vehicle data events.
- **Seating:** Vehicle entry and exit, seat assignment and camera handling.

## Controls

| Input | Action |
| --- | --- |
| Horizontal axis | Steering |
| C | Accelerator |
| X | Brake |
| Z | Clutch |
| V / B | Shift up / down |
| E | Vehicle entry / ignition, depending on context |
| Q | Switch off ignition |
| F | Exit vehicle |

To start the engine, press E to enable the electrical system, then hold E for at least 1.5 seconds and release.

## Code Layout

Vehicle code is under `Assets/Scripts/Car`.

Movement, steering, ignition, transmission, seats and gauges have separate implementations. `CarData` holds the vehicle state, while `CarDataProvider` exposes changes to systems such as the dashboard.

`ITransmission` provides a common interface for the manual and automatic transmission implementations.

## Opening the Project

Open the repository through Unity Hub using **Unity 2022.3.47f1**.

This is a prototype exploring vehicle mechanics and interaction.

**Built with Unity and C#.**
