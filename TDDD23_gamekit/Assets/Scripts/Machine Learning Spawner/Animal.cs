using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    
    public string animalName;
  
    public bool spawnedWithVoice = false;

    public float animalSpeed;
    public float animalMaxSpeed;
    public int animalDamage;
    public float scaleSize;
    public string typeOfAnimal;


    private List<string> smallAnimals = new List<string> {"ant",
            
            "dog", "crab", "fish", "frog",  "hedgehog",
            "lobster",  "monkey",  "rabbit", "raccoon","scorpion", "sea_turtle", "snail","spider",
            "squirrel",
            "teddy-bear",   "snake",
            "cat","mouse",
            };

    private List<string> largeAnimals = new List<string> {"horse",
            "mermaid",
            "bear",  "camel",
            "cow", "crocodile",  "dolphin",  "flamingo","giraffe","elephant", "octopus", "panda", "pig",  "rhinoceros", "shark", "zebra",
            "whale",
            "tiger", "lion","sheep",
            "kangaroo"};

    private List<string> flyingAnimals = new List<string> { "bird", "bat",
            "bee",
            "butterfly",  "dragon", "owl",  "parrot", "duck", "swan", "mosquito",};

    // Start is called before the first frame update
    void Start()
    {
        if (smallAnimals.Contains(animalName))
        {
            animalSpeed = 5f;
            animalMaxSpeed = 5f;
            animalDamage = 1;
            scaleSize = 0.5f;
            typeOfAnimal = "smallAnimals";
        }
        else if (largeAnimals.Contains(animalName))
        {
            animalSpeed = 3f;
            animalMaxSpeed = 5f;
            animalDamage = 3;
            scaleSize = 2f;
            typeOfAnimal = "largeAnimals";
        }
        else if (flyingAnimals.Contains(animalName))
        {
            animalSpeed = 5f;
            animalMaxSpeed = 5f;
            animalDamage = 2;
            scaleSize = 2f;
            typeOfAnimal = "flyingAnimals";
        }

    }

    // Update is called once per frame
    void Update()
    {
          
    }
}
