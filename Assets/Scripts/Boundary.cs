using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    float moveSpeed;
    private float xRange = 8;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        Vector2 newPosition = rb.position + new Vector2(moveInput * moveSpeed * Time.deltaTime, 0);

        newPosition.x = Mathf.Clamp(newPosition.x, -xRange, xRange);

        rb.position = newPosition;
    }

}
