using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    private static bool hasInitialized = false; // Ensure it runs only once

    void Awake()
    {
        if (!hasInitialized)
        {
            hasInitialized = true;
            SceneManager.LoadScene("StartScreen");
        }
        else
        {
            Destroy(gameObject); // Destroy if it already ran
        }
    }
}
