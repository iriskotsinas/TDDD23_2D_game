using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject Enemy1;

    // Update is called once per frame
    public void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        StartCoroutine("SpawnObj");
    }

    IEnumerator SpawnObj()
    {
        Instantiate(Enemy1, new Vector3(transform.position.x, transform.position.y,0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        StartCoroutine("SpawnObj");
    }
}
