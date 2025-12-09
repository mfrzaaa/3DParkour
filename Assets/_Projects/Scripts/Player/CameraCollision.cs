using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [Header("Settings")]
    public Transform cameraHolder; // Pivot (Kepala/Leher)
    public float minDistance = 0.5f;
    public float maxDistance = 4.0f;
    public float smooth = 20.0f; 
    
    // INI VARIABLE YANG HILANG TADI:
    public float cameraRadius = 0.3f; // Radius "badan" kamera agar tidak tembus

    [Header("Layer Mask")]
    public LayerMask whatIsObstacle; // Jangan lupa centang GROUND di Inspector!

    private Vector3 currentVelocity; 

    private void LateUpdate()
    {
        Vector3 direction = -cameraHolder.forward; 
        float targetDistance = maxDistance;

        // Gunakan SphereCast (Bola) agar deteksi lebih luas daripada sekadar garis
        RaycastHit hit;
        if (Physics.SphereCast(cameraHolder.position, cameraRadius, direction, out hit, maxDistance, whatIsObstacle))
        {
            // Jarak kamera dipotong sesuai jarak benturan
            targetDistance = hit.distance;
            
            // Kurangi sedikit lagi agar tidak pas banget nempel tembok (buffer)
            targetDistance -= cameraRadius; 
        }

        // Clamp agar tidak minus atau terlalu dekat
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

        // Pindahkan posisi kamera secara lokal di sumbu Z
        Vector3 targetLocalPos = new Vector3(0, 0, -targetDistance);
        
        // Pindah posisi dengan halus
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, Time.deltaTime * smooth);
    }

    // Visualisasi Debugging (Bola Merah)
    private void OnDrawGizmos()
    {
        if (cameraHolder == null) return;

        Gizmos.color = Color.red;
        Vector3 direction = -cameraHolder.forward;
        
        // Menggambar garis jalur kamera
        Gizmos.DrawLine(cameraHolder.position, cameraHolder.position + direction * maxDistance);
        
        // Menggambar bola deteksi di posisi kamera saat ini
        Gizmos.DrawWireSphere(transform.position, cameraRadius);
        
        // Menggambar bola target (posisi ideal tanpa halangan)
        Gizmos.DrawWireSphere(cameraHolder.position + direction * maxDistance, cameraRadius);
    }
}