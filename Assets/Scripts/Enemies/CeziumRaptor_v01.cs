using UnityEngine;
using System.Collections;

public class CeziumRaptor_v01 : Raptor {
	public enum state {Wander,Tracking,Fleeing};
	public state currentState;

	public bool hasCeZium;

	ArenaWall arenaManager;

	// Use this for initialization
	void Start () {
		arenaManager = GameObject.FindGameObjectWithTag ("ArenaManager").GetComponent<ArenaWall>();
		currentState = state.Wander;
		InvokeRepeating ("AvoidObstacles", 0, 0.75f);
		PickWaypoint ();
		InvokeRepeating ("CheckNearestCezium", 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        print();
		Move (); //moves the Raptor forward by its moveSpeed
		if (ceZiumManager.exposedCeziumUnits > 0 && !hasCeZium) { 
			//CeZium is exposed but the Raptor hasn't collected any of it
			currentState = state.Tracking;
		} 
		else if (hasCeZium) {
			currentState = state.Fleeing;

		} 
		else {
			//No CeZium is exposed and the Raptor hasn't collected any
			currentState = state.Wander;
		}
		if (currentState == state.Wander) {//Wandering around
			moveSpeed = slowSpeed;
			if (!avoidingObstacle) {
				//Regular wandering
				turnSpeed = slowTurnSpeed;
				if (Vector3.Distance (transform.position, target) < waypointDistanceThreshold) {
					PickWaypoint ();
				}
			} else { //avoiding an obstacle
				turnSpeed = fastTurnSpeed;
				if (Vector3.Distance (transform.position, avoidanceTarget) < waypointDistanceThreshold / 2) {
					avoidingObstacle = false;
				}
			}
		} 

		else if (currentState == state.Tracking) { //Tracking down CeZium
			moveSpeed = fastSpeed;
			turnSpeed = fastTurnSpeed;
			if (avoidingObstacle) {
				//The Raptor still needs to avoid obstacles, even if it's chasing down CeZium
				if (Vector3.Distance (transform.position, avoidanceTarget) < waypointDistanceThreshold / 2) {
					avoidingObstacle = false;
				}
			}
		} 

		else if (currentState == state.Fleeing) { //Has gottne CeZium and is fleeing the mining region
			moveSpeed = fastSpeed;
			if (avoidingObstacle) {
				//The Raptor still needs to avoid obstacles, even if it's chasing down CeZium
				if (Vector3.Distance (transform.position, avoidanceTarget) < waypointDistanceThreshold / 2) {
					avoidingObstacle = false;
				}
			}
			if (Vector3.Distance (transform.position, target) < waypointDistanceThreshold) {
				Destroy (gameObject);
			}
		}
	}

	void CheckNearestCezium(){
		if (ceZiumManager.exposedCeziumUnits > 0) {
			float shortestDistance = Mathf.Infinity; //Start with an infinite distance, so any distance between the raptor and CeZium will be less that this starting value
			for(int index = 0; index < ceZiumManager.ceziumLocations.Count; index++) {
				float unitDistance = Vector3.Distance (transform.position, ceZiumManager.ceziumLocations [index].position);
				if (unitDistance < shortestDistance) {
					shortestDistance = unitDistance;
					target = ceZiumManager.ceziumLocations [index].position;
				}
			}
		}
	}

	void AvoidObstacles()
	{
		Debug.DrawRay(transform.position, transform.forward * obstacleRayDistance, Color.red, 0.75f);
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit, obstacleRayDistance ))
		{
			if(hit.collider.gameObject.tag == "Asteroid")//The raycast has detected an asteroid in front of the Raptor
			{
				bool leftOpen = true; //Is the left path open?
				bool rightOpen = true; //Is the right path open?

				Vector3 leftRay = transform.FindChild("Avoidance_L").position - transform.position; //The direction and magnitude to the left avoidance target
				Vector3 rightRay = transform.FindChild("Avoidance_R").position - transform.position; //The direction and magnitude to the right avoidance target
				//Determine if the left or right fields are open
				if (Physics.Raycast(transform.position, leftRay, leftRay.magnitude))
				{
					Debug.DrawRay(transform.position, leftRay, Color.black, 0.1f);
					leftOpen = false;
				}
				if (Physics.Raycast(transform.position, rightRay, rightRay.magnitude))
				{
					Debug.DrawRay(transform.position, rightRay, Color.black, 0.1f);
					rightOpen = false;
				}

				if(leftOpen && !rightOpen) //Get the Left Obstacle avoidance position;
				{
					avoidanceTarget = transform.FindChild("Avoidance_L").position;
					avoidingObstacle = true;
				}
				else if (rightOpen && !leftOpen) //Get the Right Obstacle avoidance position;
				{
					avoidanceTarget = transform.FindChild("Avoidance_R").position;
					avoidingObstacle = true;
				}

				else if (!rightOpen && !leftOpen) //if both are closed
				{
					PickWaypoint ();
				}

				else //if both are open
				{
					int directionChoice = Random.Range(0, 2); //Choose one of the two avoidance locations to go to
					if (directionChoice == 0)
					{
						avoidanceTarget = transform.FindChild("Avoidance_L").position;
					}
					else
					{
						avoidanceTarget = transform.FindChild("Avoidance_R").position;
					}
					avoidingObstacle = true;
				}

			}
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "CeZium") {
			hasCeZium = true;
			currentState = state.Fleeing;
			Destroy (other.gameObject);

			//Set a target Vector3 that is outside the mining region walls.
			Vector3 direction = transform.position - Vector3.zero;
			float distanceFromCenter = arenaManager.radius * 1.35f;
			target = direction.normalized * distanceFromCenter;

			//Stop checking for CeZium
			CancelInvoke ("CheckNearestCezium");
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(target, 0.5f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(avoidanceTarget, 0.25f);
	}
}
