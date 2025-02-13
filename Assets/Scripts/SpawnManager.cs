using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsManager : MonoBehaviour
{
    public GameObject fallingObjectPrefab;
    public Sprite sprite1;
    public Sprite sprite2;
    public float initialSpawnRate = 1.5f;
    public float initialFallSpeed = 3f;
    private float spawnRate;
    private float fallSpeed;
    private int lastScoreCheckpoint = 0;
    private Vector2 screenBounds;


    
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        spawnRate = initialSpawnRate;
        fallSpeed = initialFallSpeed;
        
        StartCoroutine(SpawnObjects());

    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnRate * 0.5f, spawnRate * 1.5f)); // Random interval
            SpawnObject();
        }
    }    

    void SpawnObject()
    {
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
    Vector3 spawnPosition = new Vector3(randomX, screenBounds.y + 1, 0); // Spawn just above screen
    GameObject obj = Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);

    
    SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
    FallingObjectBehavior fallingObject = obj.AddComponent<FallingObjectBehavior>();

    if (sr != null)
    {
        if (Random.value > 0.5f)
        {
            sr.sprite = sprite1;
            fallingObject.isDangerous = false;
        }
        else
        {
            sr.sprite = sprite2;
            fallingObject.isDangerous = true;
        }
    }

    fallingObject.fallSpeed = fallSpeed; // Ensure fall speed is set
    }


public void IncreaseDifficulty(int score)
    {
        if (score >= lastScoreCheckpoint + 10) // Every 10 points, increase difficulty
        {
            lastScoreCheckpoint = score;
            fallSpeed += 1f; // Increase fall speed
            spawnRate *= 0.9f; // Decrease spawn interval slightly (faster spawns)
            spawnRate = Mathf.Clamp(spawnRate, 0.5f, initialSpawnRate); // Prevent it from getting too fast
        }
    }
}

public class FallingObjectBehavior : MonoBehaviour
{
    public float fallSpeed = 3f;
    public bool isDangerous; // Make sure this is correctly set in SpawnObject()
    
    private float lowerScreenLimit;

    void Start()
    {
        lowerScreenLimit = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 1;
    }

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < lowerScreenLimit)
        {
            Destroy(gameObject); // Destroy if it falls offscreen
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isDangerous) // Only lose health if this is a harmful sprite
            {
                PlayerControl playerControl = other.GetComponent<PlayerControl>();
                if (playerControl != null)
                {
                    playerControl.TriggerHurtAnimation();  // Disable movement when hurt
                }

                HealthManager healthManager = FindObjectOfType<HealthManager>();
                if (healthManager != null)
                {
                    healthManager.LoseHealth();
                }
            }

            if (!isDangerous) // If it's a safe object
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.IncreaseScore(); // Add 1 point
            }
        }

            Destroy(gameObject); // Remove the object when caught
        }
    }
}

