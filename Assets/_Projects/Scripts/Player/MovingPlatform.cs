using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 3f;
    public float distance = 4f;
    public bool moveLeftRight = true;

    [Header("Variation")]

    public bool useRandomStart = true; 
    
    public float manualOffset = 0f;

    private Vector3 startPosition;
    private float timeOffset; // Variabel rahasia untuk membedakan waktu

    private void Start()
    {
        startPosition = transform.position;

        if (useRandomStart)
        {
            timeOffset = Random.Range(0f, 100f);
        }
        else
        {
            timeOffset = manualOffset;
        }
    }

    private void FixedUpdate()
    {
        // Matematika: Sin( (Waktu + WaktuAcak) * Kecepatan )
        float offset = Mathf.Sin((Time.time + timeOffset) * speed) * distance;

        if (moveLeftRight)
        {
            transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
        }
        else
        {
            transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + offset);
        }
    }
}