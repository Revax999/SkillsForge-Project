using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
   public float moveSpeed;
   private bool isMoving;
   private float inputX;
   private Animator animator;
   private float lastDirection = 1f;
   private bool isHurt = false;

   private void Start()
   {
        animator = GetComponent<Animator>();
   }

   private void Update()
    {
        if (FindObjectOfType<HealthManager>().isGameOver) return;

        if (isHurt) return;
        
        inputX = Input.GetAxisRaw("Horizontal");

        bool isMoving = Mathf.Abs(inputX) > 0;

        if (isMoving)
        {
            lastDirection = inputX;
        }

        // Apply animation values instantly
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("Move", Mathf.Abs(inputX)); // 0 = idle, 1 = running
        animator.SetFloat("FacingDirection", lastDirection);

        // Move the player
        if (isMoving)
        {
            transform.position += new Vector3(inputX * moveSpeed * Time.deltaTime, 0, 0);
        }
   }

   public void TriggerHurtAnimation()
   {
        isHurt = true;
        animator.SetBool("isMoving", false);  // Stop movement animation
        animator.SetTrigger("Hurt");

        StartCoroutine(RecoverFromHurt());
   }

   IEnumerator RecoverFromHurt()
   {
        yield return new WaitForSeconds(0.5f);
        isHurt = false; 
   }
}
