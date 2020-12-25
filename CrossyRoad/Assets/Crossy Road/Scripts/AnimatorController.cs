using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // PlayerController is public cause we shoud assign PlayerController script into animator script
    public PlayerController playerController = null;
    private Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>(); // using 'chick' objects for get access to Animator
    }

    // Update is called once per frame
    void Update()
    {
        // setting new value for 'dead' state animation
        if (playerController.isDead)
        {
            animator.SetBool("dead", true); // Dead animation
        }
        if (playerController.jumpStart)
        {
            animator.SetBool("jumpStart", true); // jumpStart animation
        }
        else if (playerController.isJumping)
        {
            animator.SetBool("jump", true); // Jumping animation
        }
        else // else make Ide state
        {
            animator.SetBool("jump", false);
            animator.SetBool("jumpStart", false);
        }



        // Making roatation 

        //if (playerController.isDead) return;



        // Why this line of code doesn't work???? In the previus line.
        //if (!playerController.isIdle) return;
    }
}
