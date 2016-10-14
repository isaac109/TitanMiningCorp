using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {

    //Replace with a generic script that instantiates weapon objects?

    public GameObject missile;
	public static int ammo = 12;

	public void Fire()
    {
		if (ammo > 0) {
			ammo--;
			Instantiate (missile, transform.position, transform.rotation);
		}
    }
}
