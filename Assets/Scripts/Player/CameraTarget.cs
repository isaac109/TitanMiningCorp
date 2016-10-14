using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {

    public GameObject playerShip;
    Rigidbody rb;
    public float upSpeed;
    
	void Start () {
        rb = playerShip.GetComponent<Rigidbody>();
	}
	
	
	void FixedUpdate () {
        float upMagnitude = 0; //change the height of the camera based on the ship's speed;
        upMagnitude = Mathf.Lerp(upMagnitude,rb.velocity.magnitude * 1.5f , upSpeed);
        Vector3 movePos = new Vector3(playerShip.transform.position.x,upMagnitude,playerShip.transform.position.z);
        transform.position = movePos;
    }

    
}
