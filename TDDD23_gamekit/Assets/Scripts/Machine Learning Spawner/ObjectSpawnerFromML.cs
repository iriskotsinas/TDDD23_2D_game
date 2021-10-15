using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerFromML : MonoBehaviour
{
    public GameObject animalPrefab;
    public Sprite[] animalSprites;

    public GameObject weaponPrefab;
    public Sprite[] weaponSprites;

    public GameObject armorPrefab;
    public Sprite[] armorSprites;

    public GameObject vehiclePrefab;
    public Sprite[] vehicleSprites;

    public GameObject foodPrefab;
    public Sprite[] foodSprites;

    public GameObject otherPrefab;
    public Sprite[] otherSprites;

    private GameObject newObj;

    private void Update()
    {
        
    }

    public GameObject MakeObject(string name, GameObject player, string groupName)
    {

        if (groupName == "animals")
        {
            newObj = InstantiateObject(name, animalSprites, player, groupName, animalPrefab);
        }
        else if(groupName == "armors")
        {
            newObj = InstantiateObject(name, armorSprites, player, groupName, armorPrefab);
        }
        else if (groupName == "vehicles")
        {
            newObj = InstantiateObject(name, vehicleSprites, player, groupName, vehiclePrefab);
        }
        else if (groupName == "foods")
        {
            newObj = InstantiateObject(name, foodSprites, player, groupName, foodPrefab);
        }
        else if (groupName == "weapons")
        {
            newObj = InstantiateObject(name, weaponSprites, player, groupName, weaponPrefab);
        }
        else if (groupName == "others")
        {
            newObj = InstantiateObject(name, otherSprites, player, groupName, otherPrefab);
        }
        else
        {
            newObj = new GameObject("Could not be found");
        }


        

        if (groupName == "vehicles")
        {
            var _collider = FindObjectOfType<CapsuleCollider2D>();
            var _sprite = FindObjectOfType<SpriteRenderer>();

            Vector2 S = _sprite.sprite.bounds.size;
            _collider.size = S;
            _collider.offset = new Vector2(0, 0);
        }
        else
        {
            resizeSprite();
        }
        
        

        

        return newObj;
    }

    public GameObject InstantiateObject(string name, Sprite[] objectSprites, GameObject player, string groupName, GameObject ObjectPrefab)
    {
        int arrayIdx = Array.FindIndex(objectSprites, s => s.name == name);
        Sprite objectSprite = objectSprites[arrayIdx];
        string objectName = objectSprite.name;
        GameObject newObject = Instantiate(ObjectPrefab, player.transform.position + new Vector3(1.0f,1.0f,0.0f), player.transform.rotation);
        newObject.name = objectName;

        if (groupName == "animals")
        {
            newObject.GetComponent<Animal>().animalName = objectName;
        }
        else if (groupName == "armors")
        {
            newObject.GetComponent<Armor>().armorName = objectName;
        }
        else if (groupName == "vehicles")
        {
            newObject.GetComponent<Vehicle>().vehicleName = objectName;
        }
        else if (groupName == "foods")
        {
            newObject.GetComponent<Food>().foodName = objectName;
        }
        else if (groupName == "weapons")
        {
            newObject.GetComponent<Weapon>().weaponName = objectName;
        }
        else if (groupName == "others")
        {
            Debug.Log(newObject.GetComponent<Other>().otherName);
            newObject.GetComponent<Other>().otherName = objectName;
        }
        
        newObject.GetComponent<SpriteRenderer>().sprite = objectSprite;

        return newObject;
    }


    public void resizeSprite()
    {
        var _collider = FindObjectOfType<BoxCollider2D>();
        var _sprite = FindObjectOfType<SpriteRenderer>();

        Vector2 S = _sprite.sprite.bounds.size;
        _collider.size = S;
        _collider.offset = new Vector2(0, 0);
    }

}
