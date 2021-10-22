using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCharacter : MonoBehaviour
{

    public GameObject allyBook;

    public void changeObject()
    {
        allyBook.SetActive(true);
        Destroy(gameObject);
    }
}
