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
    private new Rigidbody2D rigidbody;
    private bool canJump = false;

    private SpriteRenderer spriteRenderer;
    public Element element;


    public void changeElement(Element element)
    {
        this.element = element;
        if (element == Element.Fire)
        {
            spriteRenderer.color = Color.red;
        }
        else if (element == Element.Wood)
        {
            spriteRenderer.color = Color.green;
        }
        else if (element == Element.Water)
        {
            spriteRenderer.color = Color.blue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        // Move left/right
        horizontalInput = Input.GetAxis("Horizontal"+inputID);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        // Jump
        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            canJump = false;
            rigidbody.velocity = new Vector2(0, jumpForce);
        }
    }

    // When on the platform, player can jump
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0)
                {
                    canJump = true;
                }
            }
        }
    }

    // When leaving the platform, player cannot jump
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            canJump = false;
        }
    }
}