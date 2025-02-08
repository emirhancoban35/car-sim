using UnityEngine;

public class PlayerSit : MonoBehaviour
{
    public Transform carSeat;  // Oyuncunun oturacağı yer
    public GameObject player;  // Oyuncu karakteri
    public GameObject car;     // Araba nesnesi
    public Transform carCameraPosition; // Arabanın içindeki kamera noktası
    private bool isNearCar = false;
    private bool isInsideCar = false;

    private CarController carController; // Araba kontrol scripti
    private Camera mainCamera;  // Ana kamera

    private void Start()
    {
        carController = car.GetComponent<CarController>();
        carController.enabled = false; // Arabayı başlangıçta kontrol edilemez yap
        mainCamera = Camera.main; // Ana kamerayı al
    }

    void Update()
    {
        if (isNearCar && Input.GetKeyDown(KeyCode.E)) 
        {
            if (!isInsideCar)
                EnterCar();
            else
                ExitCar();
        }
    }

    private void EnterCar()
    {
        isInsideCar = true;
        player.SetActive(false);  // Oyuncuyu gizle
        player.transform.position = carSeat.position;  // Oyuncuyu arabaya taşı
        player.transform.parent = car.transform;  // Oyuncuyu arabaya bağla

        carController.enabled = true;  // Araba kontrolünü aç
        
        // Kamerayı araba içi noktaya taşı
        mainCamera.transform.position = carCameraPosition.position;
        mainCamera.transform.rotation = carCameraPosition.rotation;
        mainCamera.transform.parent = car.transform;  // Kamerayı arabaya sabitle
    }

    private void ExitCar()
    {
        isInsideCar = false;
        player.SetActive(true);  // Oyuncuyu tekrar görünür yap
        player.transform.parent = null;  // Oyuncunun bağını kaldır
        player.transform.position = carSeat.position + new Vector3(1, 0, 0); // Arabadan çıkış pozisyonu

        carController.enabled = false;  // Araba kontrolünü kapat

        // Kamerayı tekrar oyuncuya geçir
        mainCamera.transform.parent = null;
        mainCamera.transform.position = player.transform.position + new Vector3(0, 1.5f, -2f); // Oyuncunun arkasına al
        mainCamera.transform.LookAt(player.transform); // Oyuncuya bakmasını sağla
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isNearCar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isNearCar = false;
        }
    }
}
