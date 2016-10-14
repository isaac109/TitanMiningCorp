using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

    public float radius;
    public float damage;

    public GameObject explosion;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Detonate()
    {
        //Get all colliders within the blast radius of the mine
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider col in hitObjects)
        {
            if(col.gameObject.tag == "Asteroid" || col.gameObject.tag == "Raptor")
            {
                col.gameObject.SendMessage("ApplyDamage", damage);
            }
            
        }
        //Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
