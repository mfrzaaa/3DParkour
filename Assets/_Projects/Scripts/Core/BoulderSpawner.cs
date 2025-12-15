using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject boulderPrefab; 
    public float spawnInterval = 3f; // Muncul setiap 3 detik
    public float startDelay = 1f;
    
    [Header("Auto Stop")]
    public float activeDuration = 30f; // Spawner akan mati setelah 30 detik
    public bool stopWhenPlayerPasses = true; // Opsi tambahan (lihat penjelasan di bawah)

    private void Start()
    {
        // Mulai memunculkan bola berulang-ulang
        InvokeRepeating("SpawnBoulder", startDelay, spawnInterval);

        // Pasang timer untuk MENGHENTIKAN spawner
        Invoke("StopSpawning", activeDuration);
    }

    void SpawnBoulder()
    {
        if (boulderPrefab != null)
        {
            Instantiate(boulderPrefab, transform.position, transform.rotation);
        }
    }

    // Fungsi untuk mematikan spawner
    public void StopSpawning()
    {
        CancelInvoke("SpawnBoulder");
        Debug.Log("Spawner Bola Berhenti (Waktu Habis)");
        
        // Opsional: Matikan script ini agar hemat resource
        this.enabled = false;
    }
}