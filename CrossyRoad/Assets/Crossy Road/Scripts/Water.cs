﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    bool hitWater = false;
    private void OnTriggerStay(Collider other)
    {
        if (hitWater) return;

        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (!playerController.parentedToObject && !playerController.isJumping) 
            {
                Debug.Log("Player fell in to water!");

                hitWater = true;

                playerController.GotSoaked();
            }
        }
    }
}
