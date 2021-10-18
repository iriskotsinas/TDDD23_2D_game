using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMount : MonoBehaviour
{
    private bool useMount; // are we in range of the mount?
    private bool mounted; // this was actually implemented for further control when mounted, not needed at this current state

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    public string myMessage = "Press 'E' to use the mount!";
    private Rigidbody2D rb2d;

    //public Transform objControl;     // reference to the transform that our movement input will affect
    public Transform unmount;

    public Vector3 mountedPos;    // the position the player will be moved to when we use the mount
    public float moveSpeed = 0f;    // player's walking speed
    public float maxSpeed = 0f;//Max speed of Vehicle

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        useMount = false;               // to be sure it's false in the beginning, let's explicitly tell unity to do so
        mounted = false;
        //objControl = transform;        // in the beginning we want to move our player, thus we put the object's transform this script is attached to
        //unmount = transform;          // Save original transform
        player = GameObject.Find("Player");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (useMount && Input.GetKeyDown(KeyCode.E) && (!mounted))  // if we entered the mountTrigger and press E
        {
            useMount = false; // we don't want the GUI to show the message anymore and do not want this code to be able to be executed again while we sit on the mount
            moveSpeed += GetComponent<Vehicle>().speed; // Add the speed of the vehicle we are in
            maxSpeed += GetComponent<Vehicle>().maxSpeed;
            mounted = true;
            player.transform.parent = transform;
            player.SetActive(false);
        }

        else if (mounted && Input.GetKeyDown(KeyCode.E))
        {
            //Get off the vehicle
            player.SetActive(true);
            player.transform.rotation = Quaternion.identity; //Reset rotation of player
            useMount = false;
            transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1.5f); //Move away from object

            // Remove added speed
            moveSpeed -= GetComponent<Vehicle>().speed; 
            maxSpeed += GetComponent<Vehicle>().maxSpeed;

            player.transform.parent = null; //Detach from parent
            mounted = false;
            myMessage = "";
            //objControl = unmount.transform; //Reset back to player controller
            
        }

        //Check if car/animal is upright
        if ((Vector3.Dot(transform.up, Vector3.down) <= 0))
        {
            if (Input.GetKey(KeyCode.D) && mounted)
            {
                // forward
                if (rb2d.velocity.magnitude > maxSpeed)
                {
                    rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                }
                else
                {
                    rb2d.AddForce(new Vector3(1f, 0f, 0f) * (moveSpeed * 500) * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.A) && mounted)
            {
                // backwards
                if (rb2d.velocity.magnitude > maxSpeed)
                {
                    rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                }
                else
                {
                    rb2d.AddForce(new Vector3(-1f, 0f, 0f) * (moveSpeed * 500) * Time.deltaTime);
                }
            
            }
        }
            
    }

    void OnGUI()
    {
        if (useMount) // when this is set to true, we want the text to be shown
        {
            guiStyle.fontSize = 24;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 100, 20), myMessage, guiStyle);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // enable the Label showing further instructions and 'enable' the key input for mounting
        // but only do so if the trigger we entered is the trigger of a mount
        myMessage = "Press 'E' to use the mount!";
        if (collision.gameObject.tag == "Player")
        {
            useMount = true; // now we enable the piece of code in update, so that only the key needs to be pressed
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // disable the piece of code for mounting when we exit that trigger
        if (collision.gameObject.tag == "Player")
        {
            myMessage = "";
            useMount = false; // disable the code so that we can press E as often as we want when we do not enter a mountTrigger
        }
    }


}


 
