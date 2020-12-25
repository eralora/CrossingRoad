using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public bool goLeft = false;
    public bool goRight = false;
    public List<GameObject> items = new List<GameObject>();
    public List<Spawner> spawnersLeft = new List<Spawner>();
    public List<Spawner> spawnersRight = new List<Spawner>();

    // Start is called before the first frame update
    void Start()
    {
        int itemId = Random.Range(0, items.Count);

        GameObject item = items[itemId];

        int direction = Random.Range(0, 2);
        if (direction > 0) { goLeft = false; goRight = true; } else { goLeft = true; goRight = false; }
        
        if (goRight)
        {
            if (spawnersLeft.Count == 1)
            {
                spawnersLeft[0].item = item;
                spawnersLeft[0].goLeft = goLeft;
                spawnersLeft[0].gameObject.SetActive(goRight);
                spawnersLeft[0].spawnLeftPos = spawnersLeft[0].transform.position.x;
            }

            else if (spawnersLeft.Count == 2)
            {
                spawnersLeft[0].item = item;
                spawnersLeft[0].goLeft = goLeft;
                spawnersLeft[0].gameObject.SetActive(goRight);
                spawnersLeft[0].spawnLeftPos = spawnersLeft[0].transform.position.x;

                spawnersRight[1].item = item;
                spawnersRight[1].goLeft = goRight;
                spawnersRight[1].gameObject.SetActive(goRight);
                spawnersRight[1].spawnRightPos = spawnersRight[1].transform.position.x;

            }
        }
        
        else
        {
            if (spawnersRight.Count == 1)
            {
                spawnersRight[0].item = item;
                spawnersRight[0].goLeft = goLeft;
                spawnersRight[0].gameObject.SetActive(goLeft);
                spawnersRight[0].spawnRightPos = spawnersRight[0].transform.position.x;
            }

            else if (spawnersRight.Count == 2)
            {
                spawnersRight[0].item = item;
                spawnersRight[0].goLeft = goLeft;
                spawnersRight[0].gameObject.SetActive(goLeft);
                spawnersRight[0].spawnRightPos = spawnersRight[0].transform.position.x;

                spawnersLeft[1].item = item;
                spawnersLeft[1].goLeft = goRight;
                spawnersLeft[1].gameObject.SetActive(goLeft);
                spawnersLeft[1].spawnLeftPos = spawnersLeft[1].transform.position.x;
            }
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
