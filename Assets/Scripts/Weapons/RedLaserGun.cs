using UnityEngine;
using System.Collections;

public class RedLaserGun : MonoBehaviour {

    public GameObject redLaser;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Fire()
    {
		if (PlayerStats.ceziumCount > 0) {
			Instantiate (redLaser, transform.position, gameObject.transform.rotation);
		}
    }
}
