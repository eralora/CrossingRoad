using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform startPos = null; // objects start position

    //spawn time based
    public float delayMin = 1.5f; // delay time random number min value 
    public float delayMax = 5; // delay time random number max value
    public float speedMin = 1; // speed random number min value
    public float speedMax = 4; // speed random number max value

    // spawn at start
    public bool useSpawnPlacement = false;
    public int spawnCountMin = 4;
    public int spawnCountMax = 20; // for coins, how many times spawn between points

    private float lastTime = 0;
    private float delayTime = 0; // time between each object
    private float speed = 0;

    [HideInInspector] public GameObject item = null; // randomly select object and asign it item
    [HideInInspector] public bool goLeft = false;
    [HideInInspector] public float spawnLeftPos = 0;
    [HideInInspector] public float spawnRightPos = 0;

    private void Start()
    {
        if (useSpawnPlacement)
        {
            int spawnCount = Random.Range(spawnCountMin, spawnCountMax); // random number

            for (int i = 0; i < spawnCount; i++) // loop which is iterate SpawnItem() method
            {
                SpawnItem();
            }
        }
        else
        {
            speed = Random.Range(speedMin, speedMax);
        }
    }
    private void Update()
    {
        if (useSpawnPlacement) return;

        if (Time.time > lastTime + delayTime)
        {
            lastTime = Time.time;
            delayTime = Random.Range(delayMin, delayMax);
            SpawnItem();
        }
    }
    void SpawnItem()
    {

        Debug.Log("Spawn item");

        GameObject obj = Instantiate(item) as GameObject;

        obj.transform.position = GetSpawnPosition();

        float diretion = 0; if (goLeft) diretion = 180;

        if (!useSpawnPlacement) 
        {
            obj.GetComponent<Mover>().speed = speed; // set speed for object

            obj.transform.rotation = obj.transform.rotation * Quaternion.Euler(0, diretion, 0); // rotate object 
        } 
        
    }
    Vector3 GetSpawnPosition() // Object position f e car or truck position
    {
        if (useSpawnPlacement) // random.range random number between these numbers
        {
            int x = (int)Random.Range(spawnLeftPos, spawnRightPos);
            Vector3 pos = new Vector3(x, startPos.position.y, startPos.position.z);
            return pos;
        }
        else
        {
            return startPos.position;
        }
    }
}
