using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [Header("Settings")]
    public Transform cameraHolder; // Pivot (Kepala/Leher)
    public float minDistance = 0.5f;
    public float maxDistance = 4.0f;
    public float smooth = 20.0f; 
    
    public float cameraRadius = 0.3f; // Radius "badan" kamera agar tidak tembus

    [Header("Layer Mask")]
    public LayerMask whatIsObstacle;

    private Vector3 currentVelocity; 

    private void LateUpdate()
    {
        Vector3 direction = -cameraHolder.forward; 
        float targetDistance = maxDistance;

        // Gunakan SphereCast (Bola) agar deteksi lebih luas daripada sekadar garis
        RaycastHit hit;
        if (Physics.SphereCast(cameraHolder.position, cameraRadius, direction, out hit, maxDistance, whatIsObstacle))
        {
            targetDistance = hit.distance;

            targetDistance -= cameraRadius; 
        }

        // Clamp agar tidak minus atau terlalu dekat
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

        // Pindahkan posisi kamera secara lokal di sumbu Z
        Vector3 targetLocalPos = new Vector3(0, 0, -targetDistance);
        
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, Time.deltaTime * smooth);
    }

    private void OnDrawGizmos()
    {
        if (cameraHolder == null) return;

        Gizmos.color = Color.red;
        Vector3 direction = -cameraHolder.forward;

        Gizmos.DrawLine(cameraHolder.position, cameraHolder.position + direction * maxDistance);

        Gizmos.DrawWireSphere(transform.position, cameraRadius);

        Gizmos.DrawWireSphere(cameraHolder.position + direction * maxDistance, cameraRadius);
    }
}