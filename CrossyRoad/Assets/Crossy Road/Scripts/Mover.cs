using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float speed = 1.0f; // Speed of movevable object
    public float moveDirection = 0; // Need for 'spawn system' or in level generator
    public bool parentOnTrigger = true; // parented to the object f.i jump on log and move with log
    public bool hitBoxOnTrigger = false; // if you hit you Gothit method
    public GameObject moverObject = null; //mover object f.e car

    private Renderer render = null;
    private bool isVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        render = moverObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(speed * Time.deltaTime, 0, 0); // giving speed for moving object(car, log)

        IsVisible();
    }

    void IsVisible() // This method checks object seen by camera or not
    {
        if (render.isVisible) // if seen by camera
        {
            isVisible = true;
        }
        if (!render.isVisible && isVisible) // if not seen just destroy object
        {
            Debug.Log("Remove object. Object no longer seen by camera");

            Destroy(this.gameObject);
        }
    }
    // These two methods allow us to check mover actually collid 
    void OnTriggerEnter(Collider other) // object enter in mover
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter.");

            if (parentOnTrigger) // if this checked in inspector
            {
                Debug.Log("Enter: Parent to me.");

                other.transform.parent = this.transform; // object parented by this mover and move with them

                other.GetComponent<PlayerController>().parentedToObject = true;
            }
            if (hitBoxOnTrigger)
            {
                Debug.Log("Enter: GotHit. Game over");

                other.GetComponent<PlayerController>().GotHit(); // get GotHit method from PlayerController
            }
        }
    }

    void OnTriggerExit(Collider other) // object exit in mover
    {
        if (other.tag == "Player")
        {
           

            if (parentOnTrigger)
            {
                Debug.Log("Exit.Again");

                other.transform.parent = null; // Exiting from mover and parented obj

                other.GetComponent<PlayerController>().parentedToObject = false;
            }
        }
    }
}

