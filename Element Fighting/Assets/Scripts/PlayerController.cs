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
    public List<Sprite> elementSprites;

    public float freezeDuration = 2;
    public float freezeLeft = 0;
    public bool freezeStatus = false;
    public GameObject freezeSlider;

    public float invincibleDuration = 2;
    public float invincibleLeft = 0;
    public bool invincibleStatus = false;
    private bool invincibleBlink = false;

    private void blink()
    {
        Color color = spriteRenderer.color;
        if (color.a >= 1f)
        {
            invincibleBlink = false;
        }
        if(color.a <= 0.3f)
        {
            invincibleBlink = true;
        }
        if (invincibleBlink)
        {
            color.a += Time.deltaTime * 2.1f;
        }
        else
        {
            color.a -= Time.deltaTime * 2.1f;
        }
        spriteRenderer.color = color;
    }

    private void stopBlink()
    {
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }

    private void freeze(Element element1,Element element2)
    {
        if(!invincibleStatus && !freezeStatus && ((element1==Element.Fire && element2 == Element.Water)|| (element1 == Element.Wood && element2 == Element.Fire)|| (element1 == Element.Water && element2 == Element.Wood)))
        {
            freezeLeft = freezeDuration;
            freezeSlider.GetComponent<PlayerFrozenTimer>().freezeLeft = freezeDuration;
            freezeStatus = true;
            invincibleLeft = invincibleDuration;
            invincibleStatus = true;
        }
    }

    public void changeElement(Element element)
    {
        this.element = element;
        Transform childObject = transform.GetChild(0);
        if (element == Element.Fire)
        {
            spriteRenderer.color = Color.red;
            childObject.GetComponent<SpriteRenderer>().sprite = elementSprites[0];
        }
        else if (element == Element.Wood)
        {
            spriteRenderer.color = Color.green;
            childObject.GetComponent<SpriteRenderer>().sprite = elementSprites[1];
        }
        else if (element == Element.Water)
        {
            spriteRenderer.color = new Color(0f, 0.8f, 1f);
            childObject.GetComponent<SpriteRenderer>().sprite = elementSprites[2];
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
        if (freezeLeft > 0)
        {
            // Add Freeze state
            freezeSlider.SetActive(true);
            freezeLeft -= Time.deltaTime;
        }
        else
        {
            // Remove Freeze State
            freezeSlider.SetActive(false);
            freezeStatus = false;

            // Incincible stste
            if (invincibleStatus)
            {
                if (invincibleLeft > 0)
                {
                    invincibleLeft -= Time.deltaTime;
                }
                else
                {
                    invincibleStatus = false;
                }
                if (invincibleStatus)
                {
                    blink();
                }
                else
                {
                    stopBlink();
                }
            }

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