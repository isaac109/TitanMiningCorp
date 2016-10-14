using UnityEngine;
using System.Collections;

public class ExitGate : MonoBehaviour {

	public bool canExit;

	
	// Update is called once per frame
	void Update () {
		if (PlayerStats.ceziumCount >= PlayerStats.ceziumQuota) {
			canExit = true;
		} else {
			canExit = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			if (canExit) {
				Debug.Log ("Exit Level");
			}
		}
	}
}
