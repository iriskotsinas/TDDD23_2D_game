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
    private GameObject[] bodyParts;
    private GameObject[] attackPart;
    public bool isAnimal = false; //Set to true on animal prefab

    // Start is called before the first frame update
    void Start()
    {
        useMount = false;               // to be sure it's false in the beginning, let's explicitly tell unity to do so
        mounted = false;
        //objControl = transform;        // in the beginning we want to move our player, thus we put the object's transform this script is attached to
        //unmount = transform;          // Save original transform
        player = GameObject.Find("Player");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        bodyParts = GameObject.FindGameObjectsWithTag("BodyPartsToTurnOff");
        attackPart = GameObject.FindGameObjectsWithTag("AttackParts");

        if (!isAnimal)
        {
            if (gameObject.GetComponent<Vehicle>().typeOfVehicle == "waterVehicles")
            {
                // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moveSpeed);
        if (mounted)
        {
            transform.localScale = transform.localScale;
            player.transform.position = transform.position - new Vector3(0f,2f,0f);
        }



        if (useMount && Input.GetKeyDown(KeyCode.E) && (!mounted))  // if we entered the mountTrigger and press E
        {
            mountTheObject();
        }

        else if (mounted && Input.GetKeyDown(KeyCode.E))
        {
            unmountTheObject();
        }

        
        if (isAnimal) 
        {
            //Move forward
            if (Input.GetKey(KeyCode.D) && mounted)
            {
                if (rb2d.velocity.magnitude > maxSpeed)
                {
                    rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                }
                else
                {
                    rb2d.AddForce(new Vector3(1f, 0f, 0f) * (moveSpeed * 200) * Time.deltaTime);
                }
            }
            //Move backwards
            if (Input.GetKey(KeyCode.A) && mounted)
            {
                if (rb2d.velocity.magnitude > maxSpeed)
                {
                    rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                }
                else
                {
                    rb2d.AddForce(new Vector3(-1f, 0f, 0f) * (moveSpeed * 200) * Time.deltaTime);
                }
            }
            //Fly
            if (Input.GetKey(KeyCode.W) && mounted && gameObject.GetComponent<Animal>().typeOfAnimal == "flyingAnimals")
            {
                if (rb2d.velocity.magnitude > maxSpeed)
                {
                    rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                }
                else
                {
                    rb2d.AddForce(new Vector3(0f, 1f, 0f) * (moveSpeed * 200) * Time.deltaTime);
                }
            }
        }
        else
        {
            //Check if car is upright
            if ((Vector3.Dot(transform.up, Vector3.down) <= 0) || gameObject.GetComponent<Vehicle>().typeOfVehicle == "flyingVehicles"
                || gameObject.GetComponent<Vehicle>().typeOfVehicle == "waterVehicles")
            {

                //Move forward
                if (Input.GetKey(KeyCode.D) && mounted)
                {
                    if (rb2d.velocity.magnitude > maxSpeed)
                    {
                        rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                    }
                    else
                    {
                        rb2d.AddForce(new Vector3(1f, 0f, 0f) * (moveSpeed * 200) * Time.deltaTime);
                    }
                }
                //Move backwards
                if (Input.GetKey(KeyCode.A) && mounted)
                {
                    if (rb2d.velocity.magnitude > maxSpeed)
                    {
                        rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                    }
                    else
                    {
                        rb2d.AddForce(new Vector3(-1f, 0f, 0f) * (moveSpeed * 200) * Time.deltaTime);
                    }

                }
                //Fly if youre an airplane
                if (Input.GetKey(KeyCode.W) && mounted && gameObject.GetComponent<Vehicle>().typeOfVehicle == "flyingVehicles")
                {
                    // backwards
                    if (rb2d.velocity.magnitude > maxSpeed)
                    {
                        rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                    }
                    else
                    {
                        rb2d.AddForce(new Vector3(0f, 1f, 0f) * (moveSpeed * 200) * Time.deltaTime);
                    }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water" && gameObject.GetComponent<Vehicle>().typeOfVehicle == "waterVehicles")
        {
            Debug.Log("Its on water now");
            gameObject.GetComponent<Vehicle>().isOnWater = true;
            maxSpeed = gameObject.GetComponent<Vehicle>().maxSpeed;
            moveSpeed = gameObject.GetComponent<Vehicle>().speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water" && gameObject.GetComponent<Vehicle>().typeOfVehicle == "waterVehicles")
        {
            Debug.Log("Its on water now");
            gameObject.GetComponent<Vehicle>().isOnWater = true;
            maxSpeed = gameObject.GetComponent<Vehicle>().maxSpeed;
            moveSpeed = gameObject.GetComponent<Vehicle>().speed;
        }
        else if (collision.gameObject.tag == "Water" && gameObject.GetComponent<Vehicle>().typeOfVehicle != "waterVehicles")
        {
            destroyVehicle();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water" && gameObject.GetComponent<Vehicle>().typeOfVehicle == "waterVehicles")
        {
            gameObject.GetComponent<Vehicle>().isOnWater = false;
        }
    }

    void destroyVehicle()
    {
        unmountTheObject();
        Destroy(gameObject);
    }

    private void mountTheObject()
    {
        useMount = false; // we don't want the GUI to show the message anymore and do not want this code to be able to be executed again while we sit on the mount

        if (isAnimal)
        {
            moveSpeed += gameObject.GetComponent<Animal>().animalSpeed; // Add the speed of the vehicle we are in
            maxSpeed += gameObject.GetComponent<Animal>().animalMaxSpeed;
            NewPlayer.Instance.isMounted = true;
            NewPlayer.Instance.isOnAnimal = true;
        }
        else
        {
            moveSpeed += gameObject.GetComponent<Vehicle>().speed; // Add the speed of the vehicle we are in
            maxSpeed += gameObject.GetComponent<Vehicle>().maxSpeed;
            NewPlayer.Instance.isMounted = true;
            NewPlayer.Instance.isOnAnimal = false;
            foreach (GameObject part in attackPart)
            {
                part.SetActive(false);
            }
        }


        mounted = true;

        player.transform.parent = transform;

        player.GetComponent<CapsuleCollider2D>().enabled = false;

        foreach (GameObject part in bodyParts)
        {
            part.SetActive(false);
        }
    }

    private void unmountTheObject()
    {
        //Check if mounted for safety
        if (mounted)
        {
            //Get off the vehicle
            player.GetComponent<CapsuleCollider2D>().enabled = true;

            foreach (GameObject part in bodyParts)
            {
                part.SetActive(true);
            }
            foreach (GameObject part in attackPart)
            {
                part.SetActive(true);
            }

            player.transform.rotation = Quaternion.identity; //Reset rotation of player
            player.transform.position = new Vector3(transform.position.x + 3.5f, transform.position.y + 1.5f, 0); //Move away from object
            useMount = false;

            if (isAnimal)
            {
                // Remove added speed
                moveSpeed -= gameObject.GetComponent<Animal>().animalSpeed;
                maxSpeed -= gameObject.GetComponent<Animal>().animalMaxSpeed;
            }
            else
            {
                // Remove added speed
                moveSpeed -= gameObject.GetComponent<Vehicle>().speed;
                maxSpeed -= gameObject.GetComponent<Vehicle>().maxSpeed;
            }


            player.transform.parent = null; //Detach from parent
            mounted = false;
            NewPlayer.Instance.isMounted = false; //So the player know it is not mounted
            myMessage = "";
            //objControl = unmount.transform; //Reset back to player controller
        }
    }
}


 
