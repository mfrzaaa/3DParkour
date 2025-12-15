using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Settings")]
    public Transform newRespawnLocation; // Titik di mana player akan muncul nanti

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang lewat adalah Player
        if (other.CompareTag("Player"))
        {
            // Cari script VoidReset yang ada di scene (biasanya di objek Void raksasa)
            VoidReset voidScript = FindObjectOfType<VoidReset>();

            if (voidScript != null)
            {
                // Update titik respawn di script VoidReset menjadi lokasi baru ini
                voidScript.respawnPoint = newRespawnLocation;
                
                Debug.Log("Checkpoint Reached! Respawn point updated.");
                
                // Opsional: Matikan checkpoint ini agar tidak dipanggil berkali-kali
                // gameObject.SetActive(false); 
            }
        }
    }
}