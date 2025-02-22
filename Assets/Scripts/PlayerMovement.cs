
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1000f; // Speed variable 
    public float jumpSpeed = 100f; // Jump speed variable
    public Rigidbody2D rb; // Set the variable 'rb' as Rigibody
    public Vector2 movement;
    [SerializeField] private GameObject yamuk;
    public bool amIFacingLeft = false;
    private Animator _animator;
    private bool grounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Jump();
        movement = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        FlipSprite();
        _animator.SetBool("walk", movement != Vector2.zero);
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run(){
        Vector2 playerVelocity = new Vector2(movement.x * speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = playerVelocity;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void FlipSprite()
    {
        if(movement == Vector2.zero) return;

        if (movement.x > 0) 
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            amIFacingLeft = false;
        } 
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); 
            amIFacingLeft = true;
        }
            
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Grounded");
            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Restart Trigger")
        {
            Debug.Log("Restarting");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Not Grounded");
            grounded = false;
        }
    }
}
