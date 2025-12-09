using UnityEngine;

public class ModelAlignment : MonoBehaviour
{
    [SerializeField] private Transform orientation; // Target arah (Camera Orientation)
    [SerializeField] private float turnSpeed = 15f; // Kecepatan putar badan

    private void Update()
    {
        // Menyamakan rotasi model dengan orientasi kamera secara halus (Lerp)
        transform.rotation = Quaternion.Lerp(transform.rotation, orientation.rotation, Time.deltaTime * turnSpeed);
    }
}