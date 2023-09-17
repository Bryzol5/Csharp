using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private Rigidbody2D myrigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject PanelLose;
    public GameObject PanelWin;
    public bool canMove;

    // Use this for initialization
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
    }
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> (); 
        animator = GetComponent<Animator> ();
    }
    void Lose()
    {
        myrigidbody.bodyType = RigidbodyType2D.Static;
        PanelLose.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Finish()
    {

    }
    protected override void ComputeVelocity()
    {
        if (!canMove)
        {
            return;
        }
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            StartCoroutine(Jump());

        } else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if(move.x > 0.01f)
        {
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        } 
        else if (move.x < -0.01f)
        {
            if(spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }

        animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", velocity.y / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        velocity.y = jumpTakeOffSpeed;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Death")
        {
            Lose();
        }
        if (col.tag == "Portal")
        {
            Finish();
        }
    }
}