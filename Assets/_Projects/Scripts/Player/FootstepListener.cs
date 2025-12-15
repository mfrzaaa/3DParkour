using UnityEngine;

public class FootstepListener : MonoBehaviour
{
    // Fungsi ini dipanggil otomatis oleh Animasi saat kaki menyentuh tanah
    public void OnFootstep()
    {
        // Kosongkan saja jika belum mau ada suara.
        // Peringatan di console akan hilang karena fungsi ini sudah ada.
        
        // Debug.Log("Tap!"); // Uncomment ini kalau mau tes visual di console
    }
}