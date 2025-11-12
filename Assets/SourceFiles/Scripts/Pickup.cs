using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    [Header("Effects")]
    public GameObject particleEffectPrefab; // Assign particle system prefab in Inspector

    [Header("Motion Settings")]
    public float rotationSpeed = 100f; // Rotation speed in degrees per second
    public float bobbingAmount = 0.1f; // Amplitude of bobbing motion
    public float bobbingSpeed = 1f; // Speed of bobbing motion

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        // Remember the original position of the GameObject
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate the object around its up axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Create a bobbing motion up and down
        timer += Time.deltaTime * bobbingSpeed;
        float newY = startPosition.y + Mathf.Sin(timer) * bobbingAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CollectStar());
        }
    }

    private IEnumerator CollectStar()
    {
        // Instantiate the particle effect
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }

        int lastValue;
        StarTracker.RegisterStarCollected(out lastValue);

        Destroy(gameObject);

        yield return null;
        
    }

}
