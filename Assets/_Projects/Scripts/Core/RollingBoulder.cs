using UnityEngine;

public class RollingBoulder : MonoBehaviour
{
    [Header("Zigzag Settings")]
    public float initialSpeed = 10f;
    public float sideForce = 5f;

    [Header("Damage Settings")]
    public float knockbackForce = 20f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Logika Zigzag Awal
        Vector3 randomSide = Random.Range(0, 2) == 0 ? transform.right : -transform.right;
        rb.AddForce((transform.forward * initialSpeed) + (randomSide * sideForce), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                direction += Vector3.up * 0.5f;
                playerRb.AddForce(direction * knockbackForce, ForceMode.Impulse);
            }
        }
    }

    // FITUR BARU: Hapus jika kena Trigger bernama "Void" atau Tag "Finish"
    private void OnTriggerEnter(Collider other)
    {
        // Pastikan objek Void Anda punya nama "Void" atau Tag "Void"
        if (other.name.Contains("Void") || other.CompareTag("Finish")) 
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // FITUR BARU: Hapus otomatis jika jatuh di bawah ketinggian Y -20
        // (Jaga-jaga kalau tidak kena trigger Void)
        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }
}