using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInputSpawn : MonoBehaviour
{

    private string input;
    public GameObject gameObjectToEnable;
    public static bool inputIsOpen;
    public InputField myInputField;

    private List<string> vehicles = new List<string> {
            "aircraft_carrier",
            "airplane",
            "ambulance",
            "bathtub",
            "wheel",
            "van",
            "truck",
            "train",
            "police_car",
            "tractor",
            "toilet",
            "submarine",
            "speedboat",
            "skateboard",
            "bicycle",
            "bus",
            "cruise_ship",
            "flying_saucer",
            "helicopter",
            "hot_air_balloon",
            "motorbike",
            "pickup_truck",
            "rollerskates",
            "sailboat",
            "school_bus",
            "bulldozer",
            "canoe",
            "car",
            "firetruck"
        };
    private List<string> weapons = new List<string> {
            "anvil",
            "arm",
            "axe",
            "baseball",
            "baseball_bat",
            "basket",
            "basketball",
            "violin",
            "vase",
            "umbrella",
            "trumpet",
            "trombone",
            "triangle",
            "toothbrush",
            "tennis_racquet",
            "syringe",
            "sword",
            "suitcase",
            "stop_sign",
            "spoon",
            "soccer_ball",
            "shovel",
            "boomerang",
            "broom",
            "cactus",
            "cannon",
            "drill",
            "fire_hydrant",
            "fork",
            "frying_pan",
            "golf_club",
            "guitar",
            "hammer",
            "hockey_puck",
            "hockey_stick",
            "lighter",
            "pencil",
            "rifle",
            "saw",
            "scissors",
            "screwdriver",
            "clarinet",
            "crayon",
            "garden_hose",
            "harp",
            "knife",
            "rake"
        };
    private List<string> animals = new List<string> {
            "ant",
            "bat",
            "bee",
            "butterfly",
            "dog",
            "horse",
            "mermaid",
            "bear",
            "bird",
            "camel",
            "cow",
            "crab",
            "crocodile",
            "dolphin",
            "dragon",
            "elephant",
            "fish",
            "flamingo",
            "frog",
            "giraffe",
            "hedgehog",
            "lobster",
            "monkey",
            "octopus",
            "owl",
            "panda",
            "parrot",
            "pig",
            "rabbit",
            "raccoon",
            "rhinoceros",
            "scorpion",
            "sea_turtle",
            "shark",
            "snail",
            "spider",
            "squirrel",
            "teddy-bear",
            "zebra",
            "whale",
            "tiger",
            "swan",
            "snake",
            "cat",
            "duck",
            "lion",
            "mosquito",
            "mouse",
            "sheep",
            "kangaroo"
        };
    private List<string> foods = new List<string> {
            "apple",
            "asparagus",
            "banana",
            "birthday_cake",
            "blackberry",
            "blueberry",
            "carrot",
            "coffee_cup",
            "mushroom",
            "peas",
            "pineapple",
            "pizza",
            "sandwich",
            "strawberry",
            "string_bean",
            "teapot",
            "toaster",
            "watermelon",
            "wine_bottle",
            "wine_glass",
            "toothpaste",
            "steak",
            "bread",
            "broccoli",
            "cake",
            "cookie",
            "donut",
            "grapes",
            "hamburger",
            "ice_cream",
            "lollipop",
            "peanut",
            "pear",
            "popsicle",
            "potato",
            "onion"
        };
    private List<string> armors = new List<string> {
            "bandage",
            "beard",
            "belt",
            "binoculars",
            "bowtie",
            "bracelet",
            "brain",
            "wristwatch",
            "underwear",
            "t-shirt",
            "goatee",
            "tooth",
            "toe",
            "sweater",
            "stitches",
            "stethoscope",
            "sock",
            "snorkel",
            "smiley_face",
            "sleeping_bag",
            "skull",
            "crown",
            "eyeglasses",
            "face",
            "finger",
            "foot",
            "hand",
            "hat",
            "headphones",
            "jacket",
            "lipstick",
            "moustache",
            "mouth",
            "nail",
            "necklace",
            "pants",
            "purse",
            "shoe",
            "shorts",
            "camouflage",
            "cooler",
            "ear",
            "elbow",
            "eye",
            "flip_flops",
            "knee",
            "leg",
            "nose",
            "clock",
            "diamond",
            "helmet"
        };
    private List<string> others = new List<string> {
            "alarm_clock",
            "animal_migration",
            "backpack",
            "zigzag",
            "yoga",
            "windmill",
            "waterslide",
            "washing_machine",
            "tree",
            "angel",
            "The_Mona_Lisa",
            "traffic_light",
            "tornado",
            "The_Eiffel_Tower",
            "The_Great_Wall_of_China",
            "tent",
            "television",
            "telephone",
            "table",
            "swing_set",
            "sun",
            "streetlight",
            "stove",
            "stereo",
            "star",
            "stairs",
            "spreadsheet",
            "square",
            "squiggle",
            "snowflake",
            "snowman",
            "skyscraper",
            "sink",
            "barn",
            "beach",
            "bed",
            "bench",
            "book",
            "bottlecap",
            "bridge",
            "bucket",
            "bush",
            "calculator",
            "calendar",
            "camera",
            "castle",
            "ceiling_fan",
            "cello",
            "couch",
            "cup",
            "dishwasher",
            "diving_board",
            "fence",
            "fireplace",
            "floor_lamp",
            "hexagon",
            "paper_clip",
            "picture_frame",
            "radio",
            "rain",
            "remote_control",
            "saxophone",
            "see_saw",
            "hospital",
            "jail",
            "keyboard",
            "light_bulb",
            "lighthouse",
            "lightning",
            "mailbox",
            "moon",
            "mountain",
            "ocean",
            "octagon",
            "oven",
            "palm_tree",
            "passport",
            "penguin",
            "piano",
            "pillow",
            "pond",
            "pool",
            "postcard",
            "power_outlet",
            "rainbow",
            "river",
            "roller_coaster",
            "campfire",
            "candle",
            "cell_phone",
            "chair",
            "chandelier",
            "church",
            "circle",
            "cloud",
            "compass",
            "computer",
            "door",
            "dresser",
            "drums",
            "dumbbell",
            "envelope",
            "eraser",
            "fan",
            "feather",
            "flashlight",
            "flower",
            "garden",
            "grass",
            "hot_dog",
            "hot_tub",
            "hourglass",
            "house",
            "house_plant",
            "hurricane",
            "key",
            "ladder",
            "lantern",
            "laptop",
            "leaf",
            "line",
            "map",
            "marker",
            "matches",
            "megaphone",
            "microphone",
            "microwave",
            "mug",
            "paintbrush",
            "paint_can",
            "parachute",
            "pliers"
        };


    // Start is called before the first frame update
    void Start()
    {
        inputIsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !inputIsOpen && !DrawCanvas.CanvasIsOpen)
        {
            enableObject();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && inputIsOpen)
        {
            disableObject();
        }
    }

    public void ReadStringInput(string s)
    {
        //Try to spawn object
        input = s.Replace(" ", "_");
        Debug.Log(input);
        spawnObjectWithString(input);
        disableObject();
    }

    private void spawnObjectWithString(string input)
    {
        string groupName = findGroupName(input);

        Debug.Log(groupName);

        if(groupName != "notfound")
        {
            GameObject playerObj = GameObject.Find("Player");
            ObjectSpawnerFromML m_someOtherScriptOnAnotherGameObject = GameObject.FindObjectOfType(typeof(ObjectSpawnerFromML)) as ObjectSpawnerFromML;
            GameObject newObj = m_someOtherScriptOnAnotherGameObject.MakeObject(input, playerObj, groupName);

            if (groupName == "weapons")
            {
                newObj.GetComponent<Weapon>().spawnedWithVoice = true;
            }
            if (groupName == "vehicles")
            {
                newObj.GetComponent<Vehicle>().spawnedWithVoice = true;
            }
            if (groupName == "armors")
            {
                newObj.GetComponent<Armor>().spawnedWithVoice = true;
            }
            if (groupName == "foods")
            {
                newObj.GetComponent<Food>().spawnedWithVoice = true;
            }
            if (groupName == "animals")
            {
                newObj.GetComponent<Animal>().spawnedWithVoice = true;
            }
            if (groupName == "others")
            {
                newObj.GetComponent<Other>().spawnedWithVoice = true;
            }
        }
    }

    private string findGroupName(string item)
    {
        if (weapons.Contains(item))
        {
            return "weapons";
        }
        else if (vehicles.Contains(item))
        {
            return "vehicles";
        }
        else if (armors.Contains(item))
        {
            return "armors";
        }
        else if (foods.Contains(item))
        {
            return "foods";
        }

        else if (animals.Contains(item))
        {
            return "animals";
        }
        else if(others.Contains(item))
        {
            return "others";
        }
        else
        {
            return "notfound";
        }

    }

    public void enableObject()
    {
        NewPlayer.Instance.Freeze(true);
        gameObjectToEnable.SetActive(true);
        inputIsOpen = true;
        myInputField.Select();
    }

    public void disableObject()
    {
        NewPlayer.Instance.Freeze(false);
        inputIsOpen = false;
        myInputField.Select();
        myInputField.text = "";
        gameObjectToEnable.SetActive(false);
    }
}
