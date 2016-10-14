using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public string playerName;
	PlayerStats playerInfo;
	public Text shieldNumber;
	public Text healthNumber;
	public Text ceziumCount;
	public Text ceziumQuota;

	// Use this for initialization
	void Start () {
		playerInfo = GameObject.Find (playerName).GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		shieldNumber.text = ""+ Mathf.Round(PlayerStats.shield);
		healthNumber.text = "" + Mathf.Round (PlayerStats.health);
		ceziumCount.text = "CZM: " + PlayerStats.ceziumCount + "/" + PlayerStats.ceziumTank;
		ceziumQuota.text = "Quota: " + PlayerStats.ceziumQuota;

	}
}
