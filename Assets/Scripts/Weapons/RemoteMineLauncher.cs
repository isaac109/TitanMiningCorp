using UnityEngine;
using System.Collections;

public class RemoteMineLauncher : MonoBehaviour {

    public GameObject mine;
    public Rigidbody playerRB;
    public string playerName;

    GameObject deployedMine;

    public bool canLaunchMine = true; //used to prevent another mine being launched when detonating an already deployed mine

	public static int ammo = 6;

	// Use this for initialization
	void Start () {
        playerRB = GameObject.Find(playerName).GetComponent<Rigidbody>();
	}
	

    void Fire()
    {
        //the canLaunchMine variable is used to determine if a mine has already been deployed, since firing the mine and detonating the mine use the same button
        if (canLaunchMine)
        {
			if (ammo > 0) {
				ammo--;
				deployedMine = Instantiate (mine, transform.position, transform.rotation) as GameObject;
				deployedMine.GetComponent<Rigidbody> ().velocity = playerRB.velocity;
				canLaunchMine = false;
			}
        }
        else
        {
            deployedMine.SendMessage("Detonate");
            canLaunchMine = true;
        }
        
    }
}
