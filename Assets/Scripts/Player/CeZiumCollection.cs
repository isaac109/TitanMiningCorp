using UnityEngine;
using System.Collections;

public class CeZiumCollection : MonoBehaviour {

    PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		playerStats = GetComponent<PlayerStats> ();
	}
	

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "CeZium")
        {
			ceZiumManager.exposedCeziumUnits--;
			ceZiumManager.SubtractFromList (other.transform);
            PlayerStats.ceziumCount += other.gameObject.GetComponent<CeziumUnit>().ceziumAmount;
			//Limit the amount of CeZium the player can collect to the ceziumTank variable
			PlayerStats.ceziumCount = Mathf.Clamp (PlayerStats.ceziumCount, 0, PlayerStats.ceziumTank);
            Destroy(other.gameObject);
        }
    }
}
