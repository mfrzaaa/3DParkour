using UnityEngine;

public class DoorRandomizer : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] doors;

    private void Start()
    {
        ShuffleDoors();
    }

    private void ShuffleDoors()
    {
        // 1. Reset semua pintu menjadi Solid
        foreach (GameObject door in doors)
        {
            // Pastikan pintu punya Collider
            Collider col = door.GetComponent<Collider>();
            if (col != null)
            {
                col.isTrigger = false;
            }
        }

        // 2. Pilih satu angka acak
        int safeIndex = Random.Range(0, doors.Length);

        // 3. Ubah pintu yang terpilih menjadi TEMBUS (Is Trigger)
        Collider safeDoorCol = doors[safeIndex].GetComponent<Collider>();
        if (safeDoorCol != null)
        {
            safeDoorCol.isTrigger = true;
            
            // Opsional: Print di console untuk contekan
            Debug.Log("Pintu aman ada di urutan ke: " + (safeIndex + 1));
        }
    }
}