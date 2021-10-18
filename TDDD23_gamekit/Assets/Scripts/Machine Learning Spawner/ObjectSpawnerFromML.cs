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

    //TODO: If withVoice is true, then we get half the speed, damage, armor etc on the things we spawn.
    public GameObject MakeObject(string name, GameObject player, string groupName, bool withVoice = false)
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




        //if (groupName == "vehicles")
        //{
        //    var _collider = FindObjectOfType<CapsuleCollider2D>();
        //    var _sprite = FindObjectOfType<SpriteRenderer>();

        //    Vector2 S = _sprite.sprite.bounds.size;
        //    _collider.size = S;
        //    _collider.offset = new Vector2(0, 0);
        //}
        //else
        //{
        //    resizeSprite();
        //}

        PolygonCollider2D polygonCollider = FindObjectOfType<PolygonCollider2D>();
        Sprite _sprite = newObj.GetComponent<SpriteRenderer>().sprite;

        setColliderToFitSprite(ref polygonCollider, ref _sprite);

        return newObj;
    }

    public GameObject InstantiateObject(string name, Sprite[] objectSprites, GameObject player, string groupName, GameObject ObjectPrefab)
    {
        int arrayIdx = Array.FindIndex(objectSprites, s => s.name == name);
        Sprite objectSprite = objectSprites[arrayIdx];
        string objectName = objectSprite.name;
        GameObject newObject = Instantiate(ObjectPrefab, player.transform.position + new Vector3(4.0f,8.0f,0.0f), Quaternion.identity);
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


    public void setColliderToFitSprite(ref PolygonCollider2D polygonCollider, ref Sprite _sprite)
    {
        polygonCollider.pathCount = 0;
        polygonCollider.pathCount = _sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            path.Clear();
            _sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        }
    }

}
