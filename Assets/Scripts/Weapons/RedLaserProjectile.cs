using UnityEngine;
using System.Collections;

public class RedLaserProjectile : Weapon {

	void Start(){
		Invoke ("KillProjectile", deadTime);
		PlayerStats.ceziumCount -= ceziumConsumption;
	}
    
	void Update () {
        Move();
	}

    protected override void Move()
    {
        base.Move();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

	protected override void KillProjectile(){
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other)
    {
		if (!other.isTrigger) {//Don't collide with triggers
			if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Raptor") {
				other.gameObject.SendMessage ("ApplyDamage", damage);
			}
			KillProjectile ();
		}
    }
}
