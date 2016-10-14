using UnityEngine;
using System.Collections;

public class Raptor : MonoBehaviour {

	//*** Movement Variables ***
	public Transform[] waypoints;
	protected int currentWaypointIndex;

	public float slowSpeed;
	public float fastSpeed;
	protected float moveSpeed;

	public float slowTurnSpeed;
	public float fastTurnSpeed;
    public float targetAttractionThreshold;
    public float targetAngleThreshold;
	protected float turnSpeed;

	protected Vector3 target;
	protected Vector3 avoidanceTarget;  

	public float waypointDistanceThreshold; //Minimum distance to a waypoint before the raptor picks a new one
	public float obstacleRayDistance; //Distance to check for obstacles in front of the raptor

	protected bool avoidingObstacle; //Used for when the raptor is avoiding an obstacle
	//*** Movement Variables ***

	//*** Gameplay Variables ***
	public float health;

    Quaternion lookDir;

	protected virtual void Move(){
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		Vector3 relativePos;
		if (!avoidingObstacle)
		{
            relativePos = target - transform.position;
		}
		else
		{
			//If the Raptor is avoiding an obstacle, then move towards the avoidanceTarget
			relativePos = avoidanceTarget - transform.position;
		}
        if (Vector3.Distance(target, transform.position) < targetAttractionThreshold)
        {
            float angle = Vector3.Angle(transform.forward, relativePos);
            if (angle > targetAngleThreshold)
            {
                lookDir = Quaternion.LookRotation(Vector3.forward);
            }
        }
        else
        {
            lookDir = Quaternion.LookRotation(relativePos);
        }
		transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, turnSpeed * Time.deltaTime);
	}

    protected virtual void print()
    {
        Vector3 targetDir = target - transform.position;
        float angle = Vector3.Angle(transform.forward, targetDir);
        Debug.Log(angle);
    }

	protected void PickWaypoint(){
		bool hasFoundTarget = false;
		while (!hasFoundTarget)
		{
			//We used a newWaypointIndex variable to ensure the AI doesn't pick the same waypoint twice in a row
			int newWaypointIndex = Random.Range(0,waypoints.Length);
			if(newWaypointIndex == currentWaypointIndex)
			{
				continue;
			}

			target = waypoints[newWaypointIndex].position;
			currentWaypointIndex = newWaypointIndex;
			hasFoundTarget = true;
		}
	}

	public void ApplyDamage(int damage){
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
