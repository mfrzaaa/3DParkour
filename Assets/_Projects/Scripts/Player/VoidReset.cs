using UnityEngine;

public class VoidReset : MonoBehaviour
{
    public Transform respawnPoint; // Tempat pemain akan dikembalikan

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika yang jatuh adalah Player (bisa pakai Tag atau cek komponen)
        if (other.CompareTag("Player")) // Pastikan Player punya Tag "Player"
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                // 1. Matikan velocity agar momentum jatuh hilang (tidak meluncur pas respawn)
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // 2. Pindahkan posisi pemain ke titik respawn
                other.transform.position = respawnPoint.position;
            }
        }
    }
}