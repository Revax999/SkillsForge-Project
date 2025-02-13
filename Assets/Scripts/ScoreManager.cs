using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    private FallingObjectsManager fallingObjectsManager;

    void Start()
    {
        fallingObjectsManager = FindObjectOfType<FallingObjectsManager>();
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        score++;
        PlayerPrefs.SetInt("Score", score);
        UpdateScoreText();

        if (fallingObjectsManager != null)
        {
            fallingObjectsManager.IncreaseDifficulty(score);
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
