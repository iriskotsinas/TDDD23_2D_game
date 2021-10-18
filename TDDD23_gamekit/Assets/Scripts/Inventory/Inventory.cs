using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slots[0].GetComponent<Slot>().useItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slots[1].GetComponent<Slot>().useItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slots[2].GetComponent<Slot>().useItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            slots[3].GetComponent<Slot>().useItem();
        }
    }

}
