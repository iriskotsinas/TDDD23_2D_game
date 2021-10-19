using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update

    public string foodName;
    public bool spawnedWithVoice = false;
    public int healthRegen;

    private List<string> largeFoods = new List<string> {
            "birthday_cake", "pizza",
            "sandwich","toaster","watermelon","steak",
           "cake", "hamburger",};
    private List<string> simpleFoods = new List<string> {"apple",
            "asparagus",
            "banana", "blackberry",
            "blueberry",
            "carrot",
            "coffee_cup",
            "mushroom",
            "peas",
            "pineapple","strawberry",
            "string_bean",
            "teapot","wine_bottle",
            "wine_glass",
            "toothpaste", "bread","broccoli",
            "cookie",
            "donut",
            "grapes",
            "ice_cream",
            "lollipop",
            "peanut",
            "pear",
            "popsicle",
            "potato",
            "onion"};

    void Start()
    {
        if (largeFoods.Contains(foodName))
        {
            healthRegen = 4;
        }
        else if (simpleFoods.Contains(foodName))
        {
            healthRegen = 2;
        }

        if (spawnedWithVoice)
        {
            healthRegen = healthRegen / 2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
