using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Assign Variables
    
    public float moveDistance = 1; // How far player is going when we push a button
    public float moveTime = 0.4f; // How many time player get to move start position to end position
    public float colliderDistCheck = 1; // Collider distance check
    public bool isIdle = true; // Check player in Idle state
    public bool isDead = false;
    public bool isMoving = false; // related with moveDistance
    public bool isJumping = false; 
    public bool jumpStart = false;
    public ParticleSystem particle = null; // if dead state is true particle would be active
    public GameObject chick = null; // relating with chick object
    public GameObject gamePause = null; 
    private Renderer render = null; // check whether or not player seen by camera
    private bool isVisible = false; // is player visible in camera


    public AudioClip audioIdle1 = null; // idle state
    public AudioClip audioIdle2 = null; // after reach final position
    public AudioClip audioHop = null; // turning somewhere
    public AudioClip audioHit = null; // GotHit
    public AudioClip audioSplash = null; // under water

    public ParticleSystem splash = null; // for particle when player into water
    public bool parentedToObject = false;

    // Start is called before the first frame update
    void Start()
    {
        render = chick.GetComponent<Renderer>(); // assign variable chick object view
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.CanPlay()) return; // Do not understand???????

        if (isDead) return; // if do not put this line you can play with player again

        if (Input.GetKeyDown(KeyCode.Escape)) { gamePause.SetActive(true); }

        if (Input.GetKeyDown(KeyCode.Space)) { gamePause.SetActive(false); }

        CanIdle();

        CanMove();

        IsVisible();
    }
    // Can be state player be Idle
    void CanIdle()
    {
        if (isIdle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))     { CheckIfIdle(270, 0, 0); } // Quaterion.Euler angle
            if (Input.GetKeyDown(KeyCode.DownArrow))   { CheckIfIdle(270, 180, 0); }
            if (Input.GetKeyDown(KeyCode.LeftArrow))   { CheckIfIdle(270, -90, 0); }
            if (Input.GetKeyDown(KeyCode.RightArrow))  { CheckIfIdle(270, 90, 0); }
                                                         
        }
    }

    void CheckIfIdle(float x, float y, float z)
    {
        chick.transform.rotation = Quaternion.Euler(x, y, z);

        CheckIfCanMove();

        int a = Random.Range(0, 12);

        if (a < 4) PlayAudioClip(audioIdle1); // Random playing audio
    }
    // Checking if player can move, if collider front of player
    void CheckIfCanMove()
    {
        // raycast - find if there's any collider box in front of player.
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -chick.transform.up, out hit, colliderDistCheck); // using'this' because this script located in 'chick' object

        Debug.DrawRay(this.transform.position, -chick.transform.up, Color.red, 2);

        // if no collider front of player
        if (hit.collider == null)
        {
            SetMove();
        }
        else
        {
            if (hit.collider.tag == "collider") // If object tagged collider hit and print debug.log
            {
                Debug.Log("We hit something tagged collider");

                isIdle = true; // why???
            }
            else // other objects go SetMove
            {
                SetMove();
            }
        }

    }

    // If palyer can move
    void SetMove()
    {
        Debug.Log("No collision. Keep moving.");

        // state when moving
        isIdle = false;
        isMoving = true;
        jumpStart = true;
    }

    // Via key moving objects(using Moving methods)
    void CanMove()
    {
        if (isMoving) // If player dead isMoving - false, then you can not move player
        {
            // Using GetKeyUp instead of GetKeyDown because we need movement after key up. When we hold key down no mevement allowed
            if      (Input.GetKeyUp(KeyCode.UpArrow))   { Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance)); SetMoveForwardState(); }
            else if (Input.GetKeyUp(KeyCode.DownArrow)) { Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance)); }
            else if (Input.GetKeyUp(KeyCode.LeftArrow)) { Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z)); }
            else if (Input.GetKeyUp(KeyCode.RightArrow)){ Moving(new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z)); }
        }
        
    }

    // Move object using LeanTween
    void Moving( Vector3 pos)
    {
        // Locking system for jumping
        isIdle = false;
        isMoving = false;
        isJumping = true;
        jumpStart = false;

        PlayAudioClip(audioHop);

        LeanTween.move(this.gameObject, pos, moveTime).setOnComplete(MoveComplete);
    }

    // If moving is finished, just movement complete
    void MoveComplete()
    {
        isIdle = true;
        isJumping = false;

        int a = Random.Range(0, 12);

        if (a < 4) PlayAudioClip(audioIdle2);
        
    }

    //Setting movement forward for getting points
    void SetMoveForwardState() // Update distance count
    {
        Manager.instance.UpdateDistanceCount();
    }

    // IsVisible state
    void IsVisible()
    {
        if (render.isVisible) // if camera see object first time
        {
            isVisible = true; // do this
        }
        if (!render.isVisible && isVisible) // camera checking is object visible or not 
        {
            Debug.Log("Player off screen. Apply GotHit().");

            GotHit(); // do GotHit method
        }
    }

    // Public method which say in console if hit something
    public void GotHit()
    {
        isDead = true;

        ParticleSystem.EmissionModule em = particle.emission;

        em.enabled = true;

        PlayAudioClip(audioHit);

        

        Manager.instance.GameOver();

        
    }

    public void GotSoaked()
    {
        isDead = true;


        ParticleSystem.EmissionModule em = splash.emission;

        em.enabled = true;

        PlayAudioClip(audioSplash);

        chick.SetActive(false);

        Manager.instance.GameOver();

        
    }
    void PlayAudioClip(AudioClip clip)
    {
        this.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
