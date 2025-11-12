using UnityEngine;
using System;

public class StarTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // STATIC: keep track of stars collected globally across all Star objects
    public static int totalStarsCollected = 0;
    public static int totalStarsOnMap = 4;

    // STATIC: instance reference so other scripts can access this MonoBehavior.
    public static StarTracker instance;

    // EVENT: notifies subscribers (like the other Monobehaviour, StarUI) whenever a star is collected
    public static event Action<int, int> OnStarCollectedEvent; // event to notify when a star is collected


    void Awake()
    {
        instance = this;
    }

    // OUT PARAMETER: returns the last collected star value to the caller
    public static void RegisterStarCollected(out int lastStarValue)
    {
        totalStarsCollected++;
        lastStarValue = totalStarsCollected;
        Debug.Log($"Stars collected: {totalStarsCollected}/{totalStarsOnMap}");
        OnStarCollectedEvent?.Invoke(totalStarsCollected, totalStarsOnMap);

        if (instance != null)
        {
            // StarUI.UpdateStarText(totalStarsCollected, totalStarsOnMap); removed this to use event subscription instead
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    // UPDATE: runs in real-tie every frame
    void Update()
    {
        // continously log debugging to the console for every frame
        Debug.Log($"[Debug] Current stars collected: {totalStarsCollected}/{totalStarsOnMap}");
    }
}
