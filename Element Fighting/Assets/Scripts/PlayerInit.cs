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
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (n == 0)
        {
            playerController.changeElement(Element.Fire);
        }
        else if (n == 1)
        {
            playerController.changeElement(Element.Wood);
        }
        else if(n == 2)
        {
            playerController.changeElement(Element.Water);
        }
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
        player1.transform.position = new Vector3(-2, 3, 0);
        player2.transform.position = new Vector3(2, 3, 0);
        initElement(player1, n1);
        initElement(player2, n2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
