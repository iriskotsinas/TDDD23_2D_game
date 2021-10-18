using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inventory;
    public GameObject itemButton;
    private bool canPickup = false;
    private int ARMOR_SLOT = 4;
    private int WEAPON_SLOT = 5;


    //Inventory should only be able to store "foods, other"

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canPickup && Input.GetKeyDown(KeyCode.E))
        {
            //Check if its a weapon
            if(gameObject.TryGetComponent<Weapon>(out Weapon weaponObject))
            {
                if (inventory.isFull[WEAPON_SLOT] == false)
                {
                    inventory.isFull[WEAPON_SLOT] = true;
                    itemButton.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    itemButton.GetComponent<itemInformation>().spawnedWithVoice = weaponObject.spawnedWithVoice;
                    itemButton.GetComponent<itemInformation>().objectName = weaponObject.weaponName;
                    itemButton.GetComponent<itemInformation>().groupName = "weapons";

                    Instantiate(itemButton, inventory.slots[WEAPON_SLOT].transform, false);

                    //Switch Blast Sprite (The weapon)
                    GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
                    thePlayer.transform.Find("Blast").GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    PolygonCollider2D polygonCollider = thePlayer.transform.Find("AttackHit").GetComponent<PolygonCollider2D>();
                    Sprite _sprite = thePlayer.transform.Find("Blast").GetComponent<SpriteRenderer>().sprite;

                    //Reshape the collider to match the sprite
                    setColliderToFitSprite(ref polygonCollider, ref _sprite);
                    polygonCollider.transform.localScale = new Vector3(3f, 3f, 0);
                    thePlayer.transform.Find("Blast").transform.localScale = new Vector3(3f, 3f, 0);

                    //Update the attack hit damage
                    GameObject.FindGameObjectWithTag("Player").transform.Find("AttackHit").GetComponent<AttackHit>().isWeaponInHand = true;
                    GameObject.FindGameObjectWithTag("Player").transform.Find("AttackHit").GetComponent<AttackHit>().hitPower = gameObject.GetComponent<Weapon>().hitPower;
                    Destroy(gameObject);
                }
            }
            //Check if its armor
            else if(gameObject.TryGetComponent<Armor>(out Armor armorObject))
            {
                inventory.isFull[ARMOR_SLOT] = true;
                itemButton.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                itemButton.GetComponent<itemInformation>().spawnedWithVoice = armorObject.spawnedWithVoice;
                itemButton.GetComponent<itemInformation>().objectName = armorObject.armorName;
                itemButton.GetComponent<itemInformation>().groupName = "armors";

                Instantiate(itemButton, inventory.slots[ARMOR_SLOT].transform, false);

                ////Switch Blast Sprite (The weapon)
                //GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
                //thePlayer.transform.Find("Blast").GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                //PolygonCollider2D polygonCollider = thePlayer.transform.Find("AttackHit").GetComponent<PolygonCollider2D>();
                //Sprite _sprite = thePlayer.transform.Find("Blast").GetComponent<SpriteRenderer>().sprite;

                ////Reshape the collider to match the sprite
                //setColliderToFitSprite(ref polygonCollider, ref _sprite);
                //polygonCollider.transform.localScale = new Vector3(3f, 3f, 0);
                //thePlayer.transform.Find("Blast").transform.localScale = new Vector3(3f, 3f, 0);

                //Update the maxhealth
                GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayer>().isWearingArmor = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayer>().addedHealth = gameObject.GetComponent<Armor>().addedMaxHealth;
                Destroy(gameObject);
            }
            //Add to foods / Object slots
            else
            {
                for (int i = 0; i < inventory.slots.Length - 2; i++)
                {
                    if (inventory.isFull[i] == false)
                    {
                        //ITEM CAN BE ADDED TO INVENTORY
                        inventory.isFull[i] = true;
                        itemButton.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

                        //Save information about the item we are about to destroy
                        if (gameObject.TryGetComponent<Food>(out Food foodObject))
                        {
                            itemButton.GetComponent<itemInformation>().spawnedWithVoice = foodObject.spawnedWithVoice;
                            itemButton.GetComponent<itemInformation>().objectName = foodObject.foodName;
                            itemButton.GetComponent<itemInformation>().groupName = "foods";
                        }
                        else if (gameObject.TryGetComponent<Other>(out Other otherObject))
                        {
                            itemButton.GetComponent<itemInformation>().spawnedWithVoice = otherObject.spawnedWithVoice;
                            itemButton.GetComponent<itemInformation>().objectName = otherObject.otherName;
                            itemButton.GetComponent<itemInformation>().groupName = "others";
                        }

                        Instantiate(itemButton, inventory.slots[i].transform, false);
                        Destroy(gameObject);
                        break;
                    }
                }
            }

            
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPickup = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canPickup = false;

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
