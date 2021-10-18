using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    // Start is called before the first frame update
   
    public string vehicleName;

    public List<string> flyingVehicles = new List<string> { "airplane", "flying_saucer", "helicopter", "hot_air_balloon", };
    public List<string> fastVehicles = new List<string> {  "police_car", "motorbike", "car", };
    public List<string> slowVehicles = new List<string> { "ambulance", "wheel", "van", "truck", "train", "tractor", "skateboard", "bicycle", "bus", "pickup_truck", "rollerskates", "school_bus", "bulldozer", "canoe", "firetruck"};
    public List<string> waterVehicles = new List<string> { "aircraft_carrier", "bathtub", "toilet", "submarine", "speedboat", "cruise_ship", "sailboat", };
    
    public float speed;
    public float maxSpeed;
    public bool isOnWater = false;
    private string type;
    public bool spawnedWithVoice = false;

    void Start()
    {
        if(flyingVehicles.Contains(vehicleName))
        {
            speed = 7f;
            maxSpeed = 50f;
            type = "flyingVehicles";

        }
        if (fastVehicles.Contains(vehicleName))
        {
            speed = 10f;
            maxSpeed = 100f;
            type = "fastVehicles";
        }
        if (slowVehicles.Contains(vehicleName))
        {
            speed = 5f;
            maxSpeed = 40f;
            type = "slowVehicles";
        }
        if (waterVehicles.Contains(vehicleName))
        {
            type = "waterVehicles";

            if (isOnWater)
            {
                speed = 10f;
                maxSpeed = 100f;
            }
            else
            {
                speed = 1f;
                maxSpeed = 10f;
            }
        }

        if (spawnedWithVoice)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnWater && type == "waterVehicles")
        {
            speed = 10f;
            maxSpeed = 100f;
        }
        else
        {
            speed = 1f;
            maxSpeed = 10f;
        }
    }


}
