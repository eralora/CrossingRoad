using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Coin value when you pick firat coin
    public GameObject coin = null;
    public AudioClip audioClip = null;

    private void OnTriggerEnter(Collider other) // When object interact with player use this method
    {
        if (other.tag == "Player") // other mean other object
        {
            Debug.Log("Player picked up a coin");

            Manager.instance.UpdateCoinCount(coinValue);

            coin.SetActive(false);
            this.GetComponent<AudioSource>().PlayOneShot(audioClip);


            Destroy(this.gameObject, audioClip.length); // destroy coin on scene
        }
    }

}
