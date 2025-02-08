using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float openAngle = -90f; // Kapı dışa doğru açılacak
    public float smoothSpeed = 2f; // Açılıp kapanma hızı

    private Quaternion closedRotation; // Kapının kapalı olduğu rotasyon
    private Quaternion openRotation; // Kapının açık olduğu rotasyon
    private bool isOpen = false; // Kapının açık olup olmadığını tutan flag
    private Coroutine doorCoroutine; // Aktif coroutine'i tutmak için

    private Rigidbody rb; // Kapının Rigidbody bileşeni

    void Start()
    {
        // Rigidbody bileşenini al
        rb = GetComponent<Rigidbody>();

        // Kapının başlangıç rotasyonunu (kapalı durumu) kaydet
        closedRotation = transform.rotation;
        // Kapının açık durumdaki rotasyonunu hesapla
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void OnMouseDown() // Fare ile tıklama kontrolü
    {
        ToggleDoor();
    }

    void ToggleDoor()
    {
        // Eğer bir coroutine çalışıyorsa durdur
        if (doorCoroutine != null)
        {
            StopCoroutine(doorCoroutine);
        }

        // Kapının durumunu tersine çevir ve coroutine başlat
        isOpen = !isOpen;
        doorCoroutine = StartCoroutine(MoveDoor(isOpen ? openRotation : closedRotation));
    }

    IEnumerator MoveDoor(Quaternion targetRotation)
    {
        // Kapıyı hedef rotasyona doğru yumuşak bir şekilde hareket ettir
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime));
            yield return null; // Bir sonraki frame'e kadar bekle
        }

        // Kapıyı tam olarak hedef rotasyona ayarla
        rb.MoveRotation(targetRotation);
    }
}