using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private Vector2 origPos;
    public string animalName;
    public bool returnToOrigin = false;

    // Start is called before the first frame update
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
}
