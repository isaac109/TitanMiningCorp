using UnityEngine;
using System.Collections;

public class PlayerInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerStats.ceziumCount = 40f;
		PlayerStats.ceziumTank = 120;
		PlayerStats.ceziumQuota = 85;
		PlayerStats.shield = 50f;
		PlayerStats.health = 100;
	}

}
