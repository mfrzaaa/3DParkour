using UnityEngine;

public class DoorRandomizer : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] doors; // Masukkan 3 objek pintu ke sini

    private void Start()
    {
        ShuffleDoors();
    }

    private void ShuffleDoors()
    {
        // 1. Reset semua pintu menjadi KERAS (Solid) dulu
        foreach (GameObject door in doors)
        {
            // Pastikan pintu punya Collider
            Collider col = door.GetComponent<Collider>();
            if (col != null)
            {
                col.isTrigger = false; // False artinya keras/memantul
            }
        }

        // 2. Pilih satu angka acak (0, 1, atau 2)
        int safeIndex = Random.Range(0, doors.Length);

        // 3. Ubah pintu yang terpilih menjadi TEMBUS (Is Trigger)
        Collider safeDoorCol = doors[safeIndex].GetComponent<Collider>();
        if (safeDoorCol != null)
        {
            safeDoorCol.isTrigger = true; // True artinya bisa ditembus
            
            // Opsional: Print di console untuk contekan developer
            Debug.Log("Pintu aman ada di urutan ke: " + (safeIndex + 1));
        }
    }
}