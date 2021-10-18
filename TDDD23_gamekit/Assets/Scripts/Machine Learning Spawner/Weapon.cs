using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    public string weaponName;
    public bool spawnedWithVoice = false;
    public Sprite originalSprite;
    public int hitPower = 0;


    private List<string> heavyWeapons = new List<string> { "anvil", "axe", "sword", };
    private List<string> lightWeapons = new List<string> { "arm", "baseball_bat", "basket", "violin", "vase", "umbrella", "trumpet", "trombone", "triangle","tennis_racquet","syringe","suitcase","stop_sign",  "shovel","broom","cactus", "drill","fire_hydrant","frying_pan","golf_club","guitar","hammer",   "hockey_stick","saw","scissors","screwdriver","knife","harp", "rake"};
    private List<string> weakWeapons = new List<string> { "lighter", "pencil", "spoon", "toothbrush", "fork",  "clarinet","crayon","garden_hose",};
    private List<string> oneShotWeapon = new List<string> { "baseball", "basketball", "soccer_ball", "boomerang", "hockey_puck", };
    private List<string> projectileWeapon = new List<string> { "cannon", "rifle" }; 

   

    void Start()
    {
        if (heavyWeapons.Contains(weaponName))
        {
            hitPower = 6;
        }
        else if (lightWeapons.Contains(weaponName))
        {
            hitPower = 3;
        }
        else if (weakWeapons.Contains(weaponName))
        {
            hitPower = 2;
        }
        else if (oneShotWeapon.Contains(weaponName))
        {
            hitPower = 2; 
        }
        else if (projectileWeapon.Contains(weaponName))
        {
            hitPower = 3;
        }

        if (spawnedWithVoice && hitPower > 1)
        {
            hitPower = hitPower/2;
        }


    }
}
