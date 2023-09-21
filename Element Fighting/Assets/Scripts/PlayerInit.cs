using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        int n1 = UnityEngine.Random.Range(0, 3);
        player1.GetComponent<PlayerController>().n = n1;
        int n2 = UnityEngine.Random.Range(0, 3);
        while (n2 == n1)
        {
            n2 = UnityEngine.Random.Range(0, 3);
        }
        player2.GetComponent<PlayerController>().n = n2;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
