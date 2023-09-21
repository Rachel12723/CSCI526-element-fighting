using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 6;
    private float horizontalInput;
    public string inputID;

    public float jumpForce = 6;
    public KeyCode jumpKey;
    private new Rigidbody2D rigidbody;
    private bool jumpStatus = false;

    private SpriteRenderer spriteRenderer;
    public Element element;

    public float freezeDuration = 2;
    public float freezeLeft = 0;
    public bool freezeStatus = false;
    public GameObject freezeSlider;


    private void freeze(Element element1,Element element2)
    {
        if(!freezeStatus && ((element1==Element.Fire && element2 == Element.Water)|| (element1 == Element.Wood && element2 == Element.Fire)|| (element1 == Element.Water && element2 == Element.Wood)))
        {
            freezeLeft = freezeDuration;
            freezeSlider.GetComponent<PlayerFrozenTimer>().freezeLeft = freezeDuration;
            freezeStatus = true;
        }
    }

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
        freezeSlider.GetComponent<Slider>().maxValue = freezeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeLeft<=0)
        {
            freezeSlider.SetActive(false);
            freezeStatus = false;

            // Move left/right
            horizontalInput = Input.GetAxis("Horizontal" + inputID);
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

            // Jump
            if (Input.GetKeyDown(jumpKey) && jumpStatus)
            {
                jumpStatus = false;
                rigidbody.velocity = new Vector2(0, jumpForce);
            }
        }
        else
        {
            freezeSlider.SetActive(true);
            freezeLeft -= Time.deltaTime;
        }
    }

    // When on the platform, player can jump
    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0)
                {
                    jumpStatus = true;
                }
            }
        }
        else if (gameObject.CompareTag("Player"))
        {
            freeze(element, gameObject.GetComponent<PlayerController>().element);
        }
    }

    // When leaving the platform, player cannot jump
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            jumpStatus = false;
        }
    }
}