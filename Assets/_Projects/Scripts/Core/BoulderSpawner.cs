using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject boulderPrefab; 
    public float spawnInterval = 3f;
    public float startDelay = 1f;
    
    [Header("Auto Stop")]
    public float activeDuration = 30f;
    public bool stopWhenPlayerPasses = true;

    private void Start()
    {
        InvokeRepeating("SpawnBoulder", startDelay, spawnInterval);

        Invoke("StopSpawning", activeDuration);
    }

    void SpawnBoulder()
    {
        if (boulderPrefab != null)
        {
            Instantiate(boulderPrefab, transform.position, transform.rotation);
        }
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnBoulder");
        Debug.Log("Spawner Bola Berhenti (Waktu Habis)");
        
        this.enabled = false;
    }
}