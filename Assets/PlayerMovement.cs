using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1000f; // Speed variable
    public Rigidbody2D rb; // Set the variable 'rb' as Rigibody
    public Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        Run();
    }

    private void Run(){
        Vector2 playerVelocity = new Vector2(movement.x * speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = playerVelocity;
    }
}
