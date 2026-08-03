using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomTrigger : MonoBehaviour
{
    public GameObject parentRoom; // Masukkan objek induk ruangan ini ke sini

    void OnTriggerEnter(Collider other)
    {
        // Pastikan tag player adalah "Player"
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.PlayerReachedEndTrigger(parentRoom);

            // Matikan trigger sementara agar tidak terpanggil 2 kali secara tidak sengaja
            GetComponent<Collider>().enabled = false;
        }
    }
}