using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            int healthLeft = player.GetComponent<NewPlayer>().health;

            player.GetComponent<NewPlayer>().GetHurt(1, healthLeft); // (int direction, int hurtpower) should kill him
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            int healthLeft = player.GetComponent<NewPlayer>().health;

            player.GetComponent<NewPlayer>().GetHurt(1, healthLeft); // (int direction, int hurtpower) should kill him
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            int healthLeft = player.GetComponent<NewPlayer>().health;

            player.GetComponent<NewPlayer>().GetHurt(1, healthLeft); // (int direction, int hurtpower) should kill him
        }
    }

}
