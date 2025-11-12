using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class StarUI : MonoBehaviour
{
    // STATIC: reference to the text component
    private static Text starText;

    // ATTRIBUTE: [SerializeField] allows a private field to be set in the Unity Inspector
    [SerializeField] private GameObject particlePrefab;

    void Awake()
    {
        starText = GetComponent<Text>();
        // EVENT subscription: listen for when a star is collected
        StarTracker.OnStarCollectedEvent += UpdateStarText;
        UpdateStarText(StarTracker.totalStarsCollected, StarTracker.totalStarsOnMap); // Initialize UI
    }


    // STATIC method: called by the event to update the UI text
    public static void UpdateStarText(int collected, int total)
    {
        if (starText != null)
        {
            starText.text = $"Stars: {collected}/{total}";
            starText.GetComponent<StarUI>().StartCoroutine(starText.GetComponent<StarUI>().AnimateCounter());
        }
    }

    // Coroutine to animate the UI counter
    private IEnumerator AnimateCounter()
    {
        // Scale punch
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 1.5f;
        float duration = 0.2f;
        float timer = 0;

        while (timer < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        // Return to original scale
        timer = 0;
        while (timer < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Instantiate the particle effect when the counter updates
        if (particlePrefab != null)
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity, transform);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, 1f);
        starText.color = Color.Lerp(Color.yellow, Color.red, t);
    }
}
