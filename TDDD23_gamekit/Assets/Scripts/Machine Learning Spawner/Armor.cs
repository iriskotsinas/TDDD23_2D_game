using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    // Start is called before the first frame update

    public string armorName;
    public int addedMaxHealth;
    public bool spawnedWithVoice = false;
    private List<string> strongArmors = new List<string> { "t-shirt", "sweater", "stethoscope", "sleeping_bag", "crown", "hat", "jacket", "moustache", "pants", "shoe",
            "shorts","diamond",
            "helmet","camouflage",};
    private List<string> weakArmors = new List<string> { "bandage",
            "beard",
            "belt",
            "binoculars",
            "bowtie",
            "bracelet",
            "brain",
            "wristwatch",
            "underwear",
            "goatee",
            "tooth",
            "toe", "stitches", "sock",
            "snorkel",
            "smiley_face", "skull", "eyeglasses","face","finger",
            "foot",
            "hand",  "headphones", "lipstick", "mouth","nail",  "necklace", "purse", 
            "cooler",
            "ear",
            "elbow",
            "eye",
            "flip_flops",
            "knee",
            "leg",
            "nose",
            "clock",};

    
    void Start()
    {
        if (strongArmors.Contains(armorName))
        {
            addedMaxHealth = 5;
        }
        else if (weakArmors.Contains(armorName))
        {
            addedMaxHealth = 2;
        }

        if (spawnedWithVoice)
        {
            addedMaxHealth = addedMaxHealth / 2;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
