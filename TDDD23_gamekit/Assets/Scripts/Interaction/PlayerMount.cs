using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMount : MonoBehaviour
{
    private bool useMount; // are we in range of the mount?
    private bool mounted; // this was actually implemented for further control when mounted, not needed at this current state

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    public string myMessage = "Press 'E' to use the mount!";


    public Transform mountObject; // the reference for the mount object that we will sit on
    public Transform objControl;     // reference to the transform that our movement input will affect
    public Transform unmount;

    public Vector3 mountedPos;    // the position the player will be moved to when we use the mount
    public float moveSpeed = 2f;    // player's walking speed

    public float mountSpeed = 3f;   // additional speed through the mount
    // Start is called before the first frame update
    void Start()
    {
        useMount = false;               // to be sure it's false in the beginning, let's explicitly tell unity to do so
        mounted = false;
        objControl = transform;        // in the beginning we want to move our player, thus we put the object's transform this script is attached to
        unmount = transform;          // Save original transform
    }

    // Update is called once per frame
    void Update()
    {

        if (useMount && Input.GetKeyDown(KeyCode.E) && (!mounted))  // if we entered the mountTrigger and press E
        {
            Debug.Log("I AM IN USE MOUNT");
            //mountObject.gameObject.SetActive(false); // just to prevent some bugs, disable the trigger temporarily
            useMount = false; // we don't want the GUI to show the message anymore and do not want this code to be able to be executed again while we sit on the mount
                              //mounted = true;

            transform.parent = mountObject.transform; // parent the player to the mount so that he moves with its transform
            objControl = mountObject; // now we want to control the mountObject, not the player itself anymore
            
            mountObject.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            mountedPos = mountObject.position + new Vector3(0f,0.44601395f,0f); // you can also use a gameObject for the position offset
                                                            //so that you can adjust it in runtime/inspector by moving it instead of editing the script or a public vector variable
            moveSpeed += mountSpeed; // let's add the speed for the movement, don't forget to remove it when unmounting

            transform.position = mountedPos; // let's finally put the player object onto the mount
            mounted = true;
            //mountObject.gameObject.SetActive(true); // just to prevent some bugs, disable the trigger temporarily
        }

        else if (mounted && Input.GetKeyDown(KeyCode.E))
        {
            //Get off the vehicle
            useMount = false;
            //transform.parent = unmount.parent.transform;
            transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1.5f); //Move away from object
            transform.parent = null; //Detach object from parent
            mounted = false;
            myMessage = "";
            objControl = unmount.transform; //Reset back to player controller
            mountObject = null; // i don't know why, this may cause some exceptions later

        }


        //else
        //{
        //    //useMount = false;               // to be sure it's false in the beginning, let's explicitly tell unity to do so
        //    mounted = false;
        //    objControl = transform;        // in the beginning we want to move our player, thus we put the object's transform this script is attached to
        //    //unmount = transform;          // Save original transform

        //}

        if (Input.GetKey(KeyCode.D) && mounted)
        {
            objControl.Translate(new Vector3(1f,0f,0f) * moveSpeed * Time.deltaTime); // just move forward
        }
        if (Input.GetKey(KeyCode.A) && mounted)
        {
            objControl.Translate(new Vector3(-1f, 0f, 0f) * moveSpeed * Time.deltaTime); // backwards
        }

        // more input
        // plus it's better to make a movement vector and first add all the input,
        // transform the direction to worldspace and normalize the vector before moving
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
        if (collision.gameObject.tag == "Vehicle")
        {
            useMount = true; // now we enable the piece of code in update, so that only the key needs to be pressed
            mountObject = collision.gameObject.transform; // let's already get the mount in our range, which is the parent object of our trigger
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("I left");
        // disable the piece of code for mounting when we exit that trigger
        if (collision.gameObject.tag == "Vehicle")
        {
            myMessage = "";
            useMount = false; // disable the code so that we can press E as often as we want when we do not enter a mountTrigger
            mountObject = null; // i don't know why, this may cause some exceptions later
            // but also prevents bugs like attaching us to the wrong object whatever... needs to be checked whether it's null or not whenever you try to do something with the object this is
            // you can remove it if you want to
        }
    }


}


 
