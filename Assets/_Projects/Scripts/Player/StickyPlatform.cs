using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private Rigidbody playerRb;
    private Vector3 lastPlatformPos;

    private void Start()
    {
        lastPlatformPos = transform.position;
    }

    private void FixedUpdate()
    {
        // 1. Hitung berapa jauh platform bergerak sejak frame terakhir
        Vector3 platformMovement = transform.position - lastPlatformPos;

        // 2. Jika ada player di atas, geser player sejauh gerakan platform
        if (playerRb != null)
        {
            // Kita pakai MovePosition agar tetap menghormati collision (tidak tembus tembok)
            playerRb.MovePosition(playerRb.position + platformMovement);
        }

        // 3. Update posisi terakhir untuk frame berikutnya
        lastPlatformPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ambil Rigidbody player saat mendarat
            playerRb = collision.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lepaskan player saat melompat pergi
            playerRb = null;
        }
    }
}