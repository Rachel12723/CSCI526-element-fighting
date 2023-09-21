using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6;
    private float horizontalInput;
    public string inputID;

    public float jumpForce = 6;
    public KeyCode jumpKey;
    private Rigidbody2D rigidbody;
    private bool canJump = false;

    public KeyCode colorKey;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        int n = UnityEngine.Random.Range(0, 3);
        if (n == 0)
        {
            spriteRenderer.color = Color.red;
        }
        else if (n==1) 
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.blue;
        }
        Collider2D collider = GetComponent<Collider2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        // rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        // Move left/right
        horizontalInput = Input.GetAxis("Horizontal"+inputID);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        // Jump
        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            canJump = false;
            rigidbody.velocity = new Vector2(0, jumpForce);
        }

        // Color change
        if (Input.GetKeyDown(colorKey))
        {
            if (spriteRenderer.color==Color.red)
            {
                spriteRenderer.color = Color.blue;
            }
            else if(spriteRenderer.color == Color.blue)
            {
                spriteRenderer.color = Color.green;
            }
            else if (spriteRenderer.color == Color.green)
            {
                spriteRenderer.color = Color.red;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y>0)
                {
                    canJump = true;
                }
            }
        }
        
        
    }
    
    
    
}
