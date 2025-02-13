using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public List<Image> hearts;
    public GameObject gameOverText;
    public GameObject fadeBackground;
    public Text PlayerScore;
    private int currentHealth;
    public bool isGameOver = false;

    void Start()
    {
        currentHealth = hearts.Count;
        gameOverText.SetActive(false);
        fadeBackground.SetActive(false);
    }

    public void LoseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            
            hearts[currentHealth].enabled = false;

            if (currentHealth <= 0)
            {
                StartCoroutine(GameOver());
            }
        }
    }

    IEnumerator GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
        fadeBackground.SetActive(true);

        int finalScore = PlayerPrefs.GetInt("Score", 0);
        PlayerScore.text = "Your Score: " + finalScore;

        //Stops player movement
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerControl>().enabled = false;
        }

        //Stops spawning falling sprites
        GameObject fallingObjectsManager = GameObject.Find("FallingObjectsManager");
        if (fallingObjectsManager != null)
        {
            fallingObjectsManager.SetActive(false);
        }

        yield return new WaitForSeconds(5f); //Wait for 5 seconds
        SceneManager.LoadScene("StartScreen"); //Load start scene
    }
}

