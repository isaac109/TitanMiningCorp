using UnityEngine;
using System.Collections;

public class AsteroidRaptor_v01 : Raptor {

	public enum state {Wander,Windup,Charging};
	public state currentState;

	public int damage; //Damage applied to other objects when the Raptor is charging

	public float windUpTime; //The time for the windup animation to complete

	public float chargeDistance; //The distance in front of the raptor it will charge

	// Use this for initialization
	void Start () {
		currentState = state.Wander;
		turnSpeed = slowTurnSpeed; //Initialize the turnSpeed as being slow, as it will not change during gameplay
		PickWaypoint ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		if (currentState == state.Wander) {
			moveSpeed = slowSpeed;
			if (Vector3.Distance (transform.position, target) < waypointDistanceThreshold) {
				PickWaypoint ();
			}

		} 
		else if (currentState == state.Windup) {
			moveSpeed = Mathf.Lerp (moveSpeed, 0, 2f * Time.deltaTime);
		} 
		else if (currentState == state.Charging) {
			moveSpeed = Mathf.Lerp(moveSpeed,fastSpeed,4f * Time.deltaTime);
			if (Vector3.Distance (transform.position, target) < waypointDistanceThreshold / 2) {
				currentState = state.Wander;
			}
		}
	
	}

	void OnTriggerEnter(Collider other){
		if (currentState == state.Wander) { //Only check for asteroids if the Raptor is wandering
			if (other.gameObject.tag == "Asteroid") { //***CHANGE THIS LATER FOR WHEN NOT EVERY ASTEROID IS A CEZIUM ASTEROID***
				currentState = state.Windup;
				Vector3 tempTarget = other.transform.position;
				Vector3 relativeDirection = tempTarget - transform.position;
				relativeDirection.Normalize ();
				target = transform.position + relativeDirection * chargeDistance;
				Invoke ("WindUp", windUpTime);
			}
		}
	}

	void OnCollisionEnter(Collision other){
		if (currentState == state.Charging) {
			if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Player") {
				other.gameObject.SendMessage ("ApplyDamage", damage);
			}
		}
	}

	void WindUp(){
		currentState = state.Charging;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(target, 0.5f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(avoidanceTarget, 0.25f);
	}
}
