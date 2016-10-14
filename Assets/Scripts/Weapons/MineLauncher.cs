using UnityEngine;
using System.Collections;

public class MineLauncher : MonoBehaviour {

    public GameObject mine;
    public Rigidbody playerRB;
    //temp variable until final player prefab is created
    public string playerName;
	public static int ammo = 6;
	
	void Start()
    {
        playerRB = GameObject.Find(playerName).GetComponent<Rigidbody>();
    }
	
	void Fire ()
    {
		if (ammo > 0) {
			ammo--;
			GameObject deployedMine = Instantiate (mine, transform.position, transform.rotation) as GameObject;
			deployedMine.GetComponent<Rigidbody> ().velocity = playerRB.velocity;
		}
	}
}
