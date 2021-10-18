using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;
    public string objectName;
    public bool spawnedByVoice;

    private List<Vector2[]> originalPath = new List<Vector2[]>();
    private int pathCount;

    //private List<Vector2> points = new List<Vector2>();
    //private List<Vector2> simplifiedPoints = new List<Vector2>();

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PolygonCollider2D startCollider = GameObject.Find("Player").transform.Find("AttackHit").GetComponent<PolygonCollider2D>();
        pathCount = startCollider.pathCount;
        for (int i = 0; i < pathCount; i++)
        {
            originalPath.Add(startCollider.GetPath(i));
        }

    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }
    public void DropItem()
    {

        foreach (Transform child in transform)
        {
            GameObject playerObj = GameObject.Find("Player");
            ObjectSpawnerFromML m_someOtherScriptOnAnotherGameObject = GameObject.FindObjectOfType(typeof(ObjectSpawnerFromML)) as ObjectSpawnerFromML;

            string objectName = child.gameObject.GetComponent<itemInformation>().objectName;
            string groupName = child.gameObject.GetComponent<itemInformation>().groupName;
            bool spawnedWithVoice = child.gameObject.GetComponent<itemInformation>().spawnedWithVoice;

            GameObject newObj = m_someOtherScriptOnAnotherGameObject.MakeObject(objectName, playerObj, groupName);
            
            //Reset player so that he has regular punch again
            if (groupName == "weapons")
            {
                //Get the player
                GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
                thePlayer.transform.Find("Blast").GetComponent<SpriteRenderer>().sprite = newObj.GetComponent<Weapon>().originalSprite;
                PolygonCollider2D polygonCollider = thePlayer.transform.Find("AttackHit").GetComponent<PolygonCollider2D>();
                Sprite _sprite = thePlayer.transform.Find("Blast").GetComponent<SpriteRenderer>().sprite;
                
                //Reset the scale of collider and sprite
                polygonCollider.transform.localScale = new Vector3(20f, 20f, 0);
                thePlayer.transform.Find("Blast").transform.localScale = new Vector3(1f, 1f, 0);
                resetColliderToOriginal(ref polygonCollider, ref _sprite);

                //Reset the attack hit
                GameObject.FindGameObjectWithTag("Player").transform.Find("AttackHit").GetComponent<AttackHit>().isWeaponInHand = false;
                GameObject.FindGameObjectWithTag("Player").transform.Find("AttackHit").GetComponent<AttackHit>().hitPower = 1;

            }
            if(groupName == "armors")
            {
                //Update the maxhealth
                GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayer>().isWearingArmor = false;
            }

            if (spawnedWithVoice)
            {
                if (groupName == "others")
                {
                    newObj.GetComponent<Other>().spawnedWithVoice = true;
                }
                if (groupName == "foods")
                {
                    newObj.GetComponent<Food>().spawnedWithVoice = true;
                }
                if(groupName == "weapons")
                {
                    newObj.GetComponent<Weapon>().spawnedWithVoice = true;
                }
            }

            GameObject.Destroy(child.gameObject);
        }
    }

    public void useItem()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<itemInformation>().groupName == "foods")
            {
                Debug.Log("Added HP"); //TODO
                GameObject.Destroy(child.gameObject);
            }
            else
            {
                DropItem();
            }
        }
    }

    public void resetColliderToOriginal(ref PolygonCollider2D polygonCollider, ref Sprite _sprite)
    {
        polygonCollider.pathCount = 0;
        polygonCollider.pathCount = pathCount;

        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            polygonCollider.SetPath(i, originalPath[i]);
        }
    }
}
