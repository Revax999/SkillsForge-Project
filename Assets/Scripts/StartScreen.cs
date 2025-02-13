using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Detects Enter key press
        {
            SceneManager.LoadScene("GamePlay"); // Change "GamePlay" to your actual game scene name
        }
    }
}
