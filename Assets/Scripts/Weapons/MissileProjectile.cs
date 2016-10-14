using UnityEngine;
using System.Collections;

public class MissileProjectile : Weapon {

    Transform target;
    bool hasTarget = false;
    public float turnSpeed;
    float timer;

    public float damageRadius;
    public float explosivePower;

	// Use this for initialization
	void Start () {
	
	}
	
	
	void Update () {
        Move();
        timer += Time.deltaTime;
        if(timer >= deadTime)
        {
            Detonate();
        }
	}

    protected override void Move()
    {
        base.Move();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (hasTarget )
        {
			if (target == null) {
				hasTarget = false;
			}
            //Get the target position and look towards it while still moving forward.
            Vector3 relativePos = target.position - transform.position;
            Quaternion lookDir = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, turnSpeed * Time.deltaTime);
        }
    }
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Raptor")
		{
			other.gameObject.SendMessage("ApplyDamage", damage);
		}
		Detonate();
	}


    void OnTriggerEnter(Collider other)
    {
        //The missile chooses a target based on the first object that enters its detection trigger. After a target has been set, it cannot be changed
        if (!hasTarget)
        {
            if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Raptor")
            {
                hasTarget = true;
                target = other.gameObject.transform;
            }
        }
        
    }

    
    void Detonate()
    {
        //Get all physics objects near the missile when it detonates and apply an explosive force to them
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
                Vector3 forceDirection = col.gameObject.transform.position - transform.position;
                rb.AddForceAtPosition(forceDirection.normalized, transform.position);
            }
            
        }
        //Insert line about instantiating an explosion effect
        Destroy(gameObject);
    }

}
