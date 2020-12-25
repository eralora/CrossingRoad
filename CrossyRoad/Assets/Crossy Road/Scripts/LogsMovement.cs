using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsMovement : MonoBehaviour
{
    
    float a = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "logs")
        {
            
            a = other.GetComponent<Mover>().speed;
            other.GetComponent<Mover>().speed = 1.5f; // logs speed getting slower
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "logs")
        {
            other.GetComponent<Mover>().speed = a; // logs speed getting faster again
        }
    }
}
