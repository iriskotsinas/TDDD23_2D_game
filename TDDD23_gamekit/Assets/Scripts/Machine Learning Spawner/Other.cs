using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 origPos;
    public string otherName;
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
}
