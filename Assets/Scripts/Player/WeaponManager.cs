using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

    public GameObject Primary;
    public GameObject Secondary;
    public GameObject RearDeploy;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("PrimaryFire"))
        {
            Primary.SendMessage("Fire");
        }

        if (Input.GetButtonDown("SecondaryFire"))
        {
            Secondary.SendMessage("Fire");
        }

        if (Input.GetButtonDown("RearDeployment"))
        {
            RearDeploy.SendMessage("Fire");
        }
	}
}
