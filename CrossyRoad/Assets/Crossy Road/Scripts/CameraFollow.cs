using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 0.25f;
    public bool autoMove = true; // 
    public GameObject player = null; // Follow who\what
    public Vector3 offset = new Vector3(3, 6, -3); //
    Vector3 depth = Vector3.zero; //
    Vector3 pos = Vector3.zero; //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.CanPlay()) return; // locking system if this false next lines of code doesn't work

        if (autoMove)
        {
            depth = this.gameObject.transform.position += new Vector3(0, 0, speed * Time.deltaTime); // cameta slight movement in z 
            pos = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, Time.deltaTime); // constant updating camera and object position
            gameObject.transform.position = new Vector3(pos.x, offset.y, depth.z); // new camera position
        }
        else
        {
            pos = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, Time.deltaTime);
            gameObject.transform.position = new Vector3(pos.x, offset.y, pos.z); // pos.z cause camera stop moving in z and stop
        }
    }
}
