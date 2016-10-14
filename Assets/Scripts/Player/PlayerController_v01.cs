using UnityEngine;
using System.Collections;

public class PlayerController_v01 : MonoBehaviour {

    //Camera variables
    public GameObject CameraGo;
    Camera gameCamera;

    //Look position variables
    Vector3 lookPosition;

    //movement variables
    float lowPowerEngine;
    float highPowerEngine; //For when the ship is trying to fly in the opposite direction of its travel
    Rigidbody rb;
    //bool facingOpposite = false; //determine if the player is facing the opposite direction of the way they are moving
    public float angleThreshold;

    //Player stats script
    public PlayerStats playerStats;

    bool canTakeDamage = true; //Used to make the player invulnerable after taking a hit;
    float damageDelay = 1.0f;
    float previousSpeed; //Used to keep track of the player's speed one frame before they collide with anything
    Vector3 previousVector; //Used to keep track of the player's velocity one frame before they collid with anything

	void Start () {
        playerStats = GetComponent<PlayerStats>();//The variables in the PlayerStats scripts will be used for all movement and turning variables to maintain organization
        highPowerEngine = playerStats.enginePower * 2;
        lowPowerEngine = playerStats.enginePower;
        rb = GetComponent<Rigidbody>();
        gameCamera = CameraGo.GetComponent<Camera>();
	}
	
	void Update () {
        
        //Increase the engine power if the ship is trying to fly against its own momentum
        if (Vector3.Angle(rb.velocity, gameObject.transform.forward) > angleThreshold )
        {
            playerStats.enginePower = highPowerEngine;
        }else
            {
            playerStats.enginePower = lowPowerEngine;
            }
   
	}

    void FixedUpdate()//Here we add force to make the ship move.
    {
        //Turn the ship to point at the mouse
        //The turn code is in FixedUpdate because in Update, the physics calculation for the rotation will by out of sync with the coded rotation here
        lookPosition = gameCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraGo.transform.position.y));
        Vector3 relativePos = lookPosition - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * playerStats.turnSpeed);

        //Get the target force from the engine power, then modulate the force depending on how close the ship is to its maximum speed
        float targetForce = Input.GetAxis("Thrust") * playerStats.enginePower;
        float speedModulation = (playerStats.maxSpeed - rb.velocity.magnitude) / playerStats.maxSpeed;
        float appliedForce = targetForce * speedModulation;
        if(PlayerStats.ceziumCount > 0)
        {
            rb.AddRelativeForce(Vector3.forward * appliedForce);
            //Decrease the amount of Cezium in the player's tank when they are using the engines
            float ceziumLoss = Input.GetAxis("Thrust") / 50; //Divided by 50 to slow the rate of cezium loss
            PlayerStats.ceziumCount -= (ceziumLoss * (2 / playerStats.engineEfficiency));
			PlayerStats.ceziumCount = Mathf.Round(PlayerStats.ceziumCount * 100) / 100;
        }
        
        previousSpeed = rb.velocity.magnitude;
        previousVector = rb.velocity;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.DrawRay(other.contacts[0].point, other.contacts[0].normal * 10, Color.green, 2f,false);
        Debug.DrawRay(other.contacts[0].point, previousVector.normalized * 10, Color.blue, 2f, false);

        //If the ship hits an asteroid, take damage
        if(other.gameObject.tag == "Asteroid")
        {
            Vector3 collisionNormal = other.contacts[0].normal;
            Vector3 flightDirection = previousVector;
            //Determin the angle at which the ship hit the asteroid. Did they hit it head on or just skim past it?
            float dotProduct = Vector3.Dot(collisionNormal, flightDirection);
            dotProduct = Mathf.Clamp(dotProduct, -1, 1); //Clamp the dot product to avoid excessive damage amounts
            //Debug.Log(dotProduct);

            if (dotProduct < -0.2f)
            {
                float damageAmount = previousSpeed * Mathf.Abs(dotProduct); //previousSpeed is used because the rigidbody magnitude is near 0 right when a collision occurs
                damageAmount *= 2;
                if (canTakeDamage)
                {
                    ApplyDamage((int)damageAmount);
                }
            }
            
        }

    }

    void ApplyDamage(int damage)
    {
        canTakeDamage = false;
        //Debug.Log(damage);
        if(PlayerStats.shield <= 0)
        {
            PlayerStats.shield = 0;
            PlayerStats.health -= damage;
        }
        else
        {
            PlayerStats.shield -= damage;
        }
        StartCoroutine(DamageWait());
    }

    IEnumerator DamageWait()
    {
        yield return new WaitForSeconds(damageDelay);
        canTakeDamage = true;
    }

}
