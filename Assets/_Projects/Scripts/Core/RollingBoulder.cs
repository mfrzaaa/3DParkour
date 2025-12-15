using UnityEngine;

public class RollingBoulder : MonoBehaviour
{
    [Header("Zigzag Settings")]
    public float initialSpeed = 10f;
    public float sideForce = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Logika Zigzag Awal
        Vector3 randomSide = Random.Range(0, 2) == 0 ? transform.right : -transform.right;
        rb.AddForce((transform.forward * initialSpeed) + (randomSide * sideForce), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // JIKA MENABRAK PLAYER
        if (collision.gameObject.CompareTag("Player"))
        {
            // 1. Cari script VoidReset yang ada di Scene
            // (Kita butuh script ini karena dia yang memegang data Checkpoint terakhir)
            VoidReset respawnManager = Object.FindAnyObjectByType<VoidReset>(); 
            // Catatan: Jika error di Unity versi lama, ganti jadi FindObjectOfType<VoidReset>();

            if (respawnManager != null)
            {
                // 2. Reset Fisika Player (PENTING!)
                // Kita harus nol-kan kecepatan player agar dia tidak "terbang" saat muncul di spawn point
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    playerRb.linearVelocity = Vector3.zero;
                    playerRb.angularVelocity = Vector3.zero;
                }

                // 3. Pindahkan posisi Player ke Respawn Point terakhir
                // (Mengambil variabel 'respawnPoint' milik script VoidReset)
                collision.transform.position = respawnManager.respawnPoint.position;
                
                Debug.Log("Player Mati Tertimpa Batu!");
            }
        }
    }

    // Hapus bola jika jatuh ke void atau kena trigger finish
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Void") || other.CompareTag("Finish")) 
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }
}