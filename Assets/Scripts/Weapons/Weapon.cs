using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public int damage;
    public float deadTime;
    public float speed;

	public int ceziumConsumption;
	public int ammoConsumption;

    protected virtual void Move()
    {

    }

	protected virtual void KillProjectile(){

	}
}
