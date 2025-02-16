using UnityEngine;

/// <summary>
/// Araba hızına bağlı olarak kapının fiziksel hareketini yöneten sınıf.
/// </summary>
public class CarDoorPhysicsHandler : MonoBehaviour
{
    [Header("Kapı Fiziksel Davranışı")]
    [SerializeField] private Rigidbody carRigidbody;
    [SerializeField] private float windEffectMultiplier = 5f; // Hava direncinin kapıya etkisi

    private HingeJoint _hingeJoint;
    private CarDoorController _doorController;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _doorController = GetComponent<CarDoorController>();
    }

    private void FixedUpdate()
    {
        if (carRigidbody == null || !_hingeJoint.useMotor) return;

        float carSpeed = carRigidbody.velocity.magnitude;

        // Kapı açıkken araba hızlanıyorsa hava akışı nedeniyle kapıyı daha fazla aç
        if (_doorController != null && carSpeed > 5f)
        {
            ApplyWindEffect(carSpeed);
        }
    }

    /// <summary>
    /// Kapıya rüzgar etkisi uygular (Araba hızlandıkça kapı daha fazla açılır).
    /// </summary>
    /// <param name="speed">Araba hızı.</param>
    private void ApplyWindEffect(float speed)
    {
        JointMotor motor = _hingeJoint.motor;
        motor.targetVelocity = Mathf.Clamp(speed * windEffectMultiplier, 0, _doorController.motorSpeed);
        _hingeJoint.motor = motor;
    }
}
