using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 origPos;
    public string vehicleName;
    public bool returnToOrigin = false;

    void Start()
    {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (returnToOrigin)
        {
            transform.position = origPos;
        }
    }


    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.gameObject.name.Equals("Player"))
    //        GameControl.playerNearTheCarDoor = false;
    //}

    
}
