using UnityEngine;
using System.Collections;

public class ProximityMine : Mine {

    public bool canDetonate = false; //Used to stop the mine from detonating too early
    public float delayTime; //Time before the mine can detonate
    float timer;
	
	
	// Update is called once per frame
	void Update () {
        if (!canDetonate)
        {
            timer += Time.deltaTime;
        }
        if(timer >= delayTime && !canDetonate)
        {
            Debug.Log("Proximity Timer Up");
            Collider[] nearObjects = Physics.OverlapSphere(transform.position, (GetComponent<SphereCollider>().radius * transform.localScale.x));
            //we multiply the collider radius by the scale since the trigger zone scales with the object, whereas the OverlapSphere is based on global distance
            //Be sure to delete the scale reference once the final mine is created, as its scale will be 1
            foreach (Collider col in nearObjects)
            {
                if(col.gameObject.tag == "Asteroid" || col.gameObject.tag == "Raptor")
                {
                    Detonate();
                }
            }
            canDetonate = true;
        }
	}

    void OnTriggerEnter (Collider other)
    {
        if (canDetonate)
        {
            if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Raptor")
            {
                Detonate();
            }
        }
        
    }
}
