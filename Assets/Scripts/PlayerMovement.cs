
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1000f; // Speed variable
    public Rigidbody2D rb; // Set the variable 'rb' as Rigibody
    public Vector2 movement;
    [SerializeField] private GameObject yamuk;
    public bool amIFacingLeft = false;
    
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
        FlipSprite();
    }

    private void Run(){
        Vector2 playerVelocity = new Vector2(movement.x * speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = playerVelocity;
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
}
