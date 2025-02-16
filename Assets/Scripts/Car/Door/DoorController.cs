using UnityEngine;

/// <summary>
/// Kullanıcı kapıya tıkladığında açma ve kapama işlemini yöneten sınıf.
/// </summary>
[RequireComponent(typeof(HingeJoint))]
public class CarDoorController : MonoBehaviour
{
    [Header("Kapı Açılma Ayarları")]
    [SerializeField] private float openAngle = 90f;       // Kapının açılma açısı
    [SerializeField] private float closeAngle = 0f;       // Kapının kapanma açısı
    [SerializeField] private float motorForce = 100f;     // Kapıyı açmak için uygulanacak kuvvet
    [SerializeField] public float motorSpeed = 90f;      // Motorun hızı (derece/saniye)

    private HingeJoint _hingeJoint;
    private bool _isOpen = false;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        ConfigureHingeJoint();
    }

    /// <summary>
    /// HingeJoint bileşenini ayarlar.
    /// </summary>
    private void ConfigureHingeJoint()
    {
        JointLimits limits = _hingeJoint.limits;
        limits.min = closeAngle;
        limits.max = openAngle;
        _hingeJoint.limits = limits;
        _hingeJoint.useLimits = true;
    }

    /// <summary>
    /// Kapıya fare ile tıklanınca çağrılır.
    /// </summary>
    private void OnMouseDown()
    {
        ToggleDoor();
    }

    /// <summary>
    /// Kapıyı açıp kapatan fonksiyon.
    /// </summary>
    public void ToggleDoor()
    {
        _isOpen = !_isOpen;
        SetDoorMotor(_isOpen ? motorSpeed : -motorSpeed);
    }

    /// <summary>
    /// Kapının motor hızını ayarlar.
    /// </summary>
    /// <param name="speed">Kapının döneceği hız.</param>
    private void SetDoorMotor(float speed)
    {
        JointMotor motor = _hingeJoint.motor;
        motor.force = motorForce;
        motor.targetVelocity = speed;
        _hingeJoint.motor = motor;
        _hingeJoint.useMotor = true;
    }
}
