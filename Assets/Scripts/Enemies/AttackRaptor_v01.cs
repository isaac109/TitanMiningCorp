using UnityEngine;
using System.Collections;

public class AttackRaptor_v01 : Raptor {

    public enum state {Wander, Attacking, Retreating};
    public state currentState;


    public float playerRayDistance;

    GameObject player;
    public string playerName; //Only used for testing purposes, once the final player Prefab is made, this will no longer be necessary

    public GameObject projectile;
	public Transform projectileSpawner;

    public float escapeTime; //Used for escaping the Raptor by staying away from it for long enough
    public float escapeTimer;

	// Use this for initialization
	void Start () {
        currentState = state.Wander;
        player = GameObject.Find(playerName);
        InvokeRepeating("AvoidObstacles", 0, 0.75f);
        InvokeRepeating("SearchForPlayer", 0, 0.5f);
        PickWaypoint();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        if (currentState == state.Wander)
        {
			moveSpeed = slowSpeed;
            if (!avoidingObstacle)
            {//Regular Wandering
                turnSpeed = slowTurnSpeed;
                if (Vector3.Distance(transform.position, target) < waypointDistanceThreshold)
                {
                    PickWaypoint();
                }
            }
            else
            {
                turnSpeed = fastTurnSpeed; //Increase the turn speed when avoiding obstacles to avoid turning into them
                //Wandering around a target
                if(Vector3.Distance(transform.position, avoidanceTarget) < waypointDistanceThreshold / 2)
                {
                    //If the Raptor reaches the avoidance target, then it turns to face the original target again
                    avoidingObstacle = false;
                }
            }
        }
        if(currentState == state.Retreating)
        {
            slowSpeed = 2;
            turnSpeed = 1.5f;
            if(Vector3.Distance(transform.position, avoidanceTarget) < waypointDistanceThreshold / 2)
            {
                avoidingObstacle = false;
                currentState = state.Wander;
                InvokeRepeating("AvoidObstacles", 0, 0.75f);
                PickWaypoint();
            }

            if(Vector3.Distance(transform.position, target) < 1.0f) //if it reaches the back target, then it is done turning around
            {
                currentState = state.Wander;
                InvokeRepeating("AvoidObstacles", 0, 0.75f);
                PickWaypoint();
            }
        }

        if(currentState == state.Attacking)
        {
            //Check to see if the player can be seen
			//If the player is close enough, then don't bother raycasting
			if (Vector3.Distance (transform.position, player.transform.position) < playerRayDistance) {
				//Here, we check if the Raptor is close enough to the player. If it is, we raycast to the player to determine if the Raptor can see them
				//If the Raptor can't see them, then increase the escape timer, otherwise, reset the timer to 0
				RaycastHit detectPlayerHit;
				Vector3 playerDirection = player.transform.position - transform.position;
				if (Physics.Raycast (transform.position, playerDirection.normalized, out detectPlayerHit, playerRayDistance)) { //Some object is between the raptor and player
					if (detectPlayerHit.collider.gameObject.tag != "Player") {
						//Debug.Log ("Can't see the player");
						escapeTimer += Time.deltaTime;
						if (escapeTimer >= escapeTime) {
							LosePlayer ();
						}
					}
					else { //Nothing is in the way
						escapeTimer = 0.0f;
					} 
				}
			} 
			else { //The player is far enough away
				escapeTimer += Time.deltaTime;
			}
            
            //Movement Conditions
            target = player.transform.position;
            if (avoidingObstacle)
            {
                if(Vector3.Distance(transform.position, avoidanceTarget) < waypointDistanceThreshold / 2)
                {
                    avoidingObstacle = false;
                }
            }

            if(Vector3.Distance(transform.position, target) < 2.5f) //The Raptor has gotten too close to the Player
            {
                currentState = state.Retreating;
                InvokeRepeating("SearchForPlayer", 3.0f, 0.5f);
                Flip();
				CancelInvoke ("Fire");
                CancelInvoke("AvoidObstacles");
            }
        }
        
	}

    void Flip()
    {
        Vector3 backTarget = (transform.position - transform.forward * 2.5f);
        Debug.DrawRay(transform.position, backTarget, Color.green, 0.5f);
        target = backTarget;
    }

    void SearchForPlayer()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > playerRayDistance)
        {
            //Check the distance to the player first. If the player is too far away, there is no need to proceed with Raycast operations
            //Do Nothing
        }

        else if(Vector3.Distance(transform.position, player.transform.position) < 4.0f)
        {
			//If the player has gotten really close to the Raptor, within a few meters, then attack regardless of the direction
            AttackPlayer();
        }
        else
        {//If the player is close enough to be seen, then raycast out to the player to see if the player is within line of sight
            Vector3 rayDirection = player.transform.position - transform.position;
            Debug.DrawRay(transform.position, rayDirection.normalized * playerRayDistance, Color.green, 0.5f);
            RaycastHit playerRayCast;
            if(Physics.Raycast(transform.position, rayDirection.normalized, out playerRayCast, playerRayDistance ))
            {
                if(playerRayCast.collider.gameObject.tag == "Player")
                {
                    float angle = Vector3.Dot(rayDirection.normalized, transform.forward);
                    if(angle > 0.5f) //Only attack of the player is within a 45 degree vision cone of the Raptor
                    {
                        AttackPlayer();
                    }
                }
            }
        }
    }

    void AttackPlayer()
    {
        escapeTimer = 0.0f;
        currentState = state.Attacking;
        CancelInvoke("SearchForPlayer");
        moveSpeed = fastSpeed;
        turnSpeed = 2f;
		InvokeRepeating ("Fire", 1.0f, 1.75f);
    }

    void Fire() //Shoot a projectile at the player
    {
		Instantiate (projectile, projectileSpawner.position, transform.rotation);
    }

    void LosePlayer() //Called to change the Raptor's state from attacking to wandering. Conditions for calling this function are tested in the Update method
    {
		escapeTimer = 0.0f;
        currentState = state.Wander;
		CancelInvoke ("Fire");
        InvokeRepeating("SearchForPlayer", 0, 0.5f);
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
                    currentState = state.Retreating;
                    Flip();
                    CancelInvoke("AvoidObstacles");
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(target, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(avoidanceTarget, 0.25f);
    }
}
