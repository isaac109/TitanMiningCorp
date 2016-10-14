using UnityEngine;
using System.Collections;

public class RaptorProjectile : Weapon {
	
	void Start(){
		Invoke ("KillProjectile", deadTime);
	}
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime,Space.Self);
	}

	protected override void KillProjectile(){
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.SendMessage ("ApplyDamage",damage);
		}
		KillProjectile ();
	}
}
