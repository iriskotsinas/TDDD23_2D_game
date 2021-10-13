using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimal : MonoBehaviour
{
    public GameObject animalPrefab;
    public Sprite[] animalSprites;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    MakeAnimal("bat");
        //}
    }

    public void MakeAnimal(string name, GameObject player)
    {
        int arrayIdx = Array.FindIndex(animalSprites, s => s.name == name);
        Sprite animalSprite = animalSprites[arrayIdx];
        string animalName = animalSprite.name;

        GameObject newAnimal = Instantiate(animalPrefab, player.transform.position, player.transform.rotation);

        newAnimal.name = animalName;
        newAnimal.GetComponent<Animal>().animalName = animalName;

        newAnimal.GetComponent<SpriteRenderer>().sprite = animalSprite;
        var _sprite = FindObjectOfType<SpriteRenderer>();
        var _collider = FindObjectOfType<BoxCollider2D>();

        Vector2 S = _sprite.sprite.bounds.size;
        _collider.size = S;
        _collider.offset = new Vector2(0, 0);

    }
}
