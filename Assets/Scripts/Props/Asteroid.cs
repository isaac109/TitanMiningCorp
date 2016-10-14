using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    public float health;
    public float torque;
    public float torqueVariation;

	public GameObject ceZium;

    //asteroids should rotate along their local 'Z' axis

	// Use this for initialization
	void Start () {
        torque += Random.Range(-torqueVariation, torqueVariation);
        transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * torque * 10); //*10 is used because applying torque for only one frame doesn't move the asteroid
        //very much, so the torque has to be a really high value
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ApplyDamage (float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            DestroyAsteroid();
        }
    }

    void DestroyAsteroid()
    {
		//Code for creating sub-asteroids will need to go here
		Instantiate (ceZium, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
