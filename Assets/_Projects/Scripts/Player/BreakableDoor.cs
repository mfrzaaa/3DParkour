using UnityEngine;

public class BreakableDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Jika yang menabrak adalah Player DAN pintu ini adalah pintu Trigger (palsu)
        if (other.CompareTag("Player") && GetComponent<Collider>().isTrigger == true)
        {
            // Matikan visual pintunya (seolah-olah pecah/hilang)
            // Kita disable MeshRenderer-nya
            GetComponent<MeshRenderer>().enabled = false;
            
            // Opsional: Mainkan suara pecahan kaca di sini
        }
    }
}