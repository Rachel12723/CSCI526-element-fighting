using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public List<Sprite> elementSprites;

    private void initElement(GameObject player, int n)
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        PlayerController playerController = player.GetComponent<PlayerController>();
        Transform childObject = playerController.transform.GetChild(0);
        if (n == 0)
        {
            spriteRenderer.color = Color.red;
            playerController.element = Element.Fire;
        }
        else if (n == 1)
        {
            spriteRenderer.color = Color.green;
            playerController.element = Element.Wood;
        }
        else if(n == 2)
        {
            spriteRenderer.color = Color.blue;
            playerController.element = Element.Water;
        }
        childObject.GetComponent<SpriteRenderer>().sprite = elementSprites[n];
    }

    // Start is called before the first frame update
    void Start()
    {
        int n1 = UnityEngine.Random.Range(0, 3);
        int n2 = UnityEngine.Random.Range(0, 3);
        while (n2 == n1)
        {
            n2 = UnityEngine.Random.Range(0, 3);
        }
        initElement(player1, n1);
        initElement(player2, n2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
