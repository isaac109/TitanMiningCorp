using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
    //This script contains all player stats like health, speed, weapons, etc.

    public static int health;
    public static float shield;
    public float enginePower; //Acceleration
    public float maxSpeed;
    public float turnSpeed;
    public int engineEfficiency;

    public static float ceziumCount;
    public static int ceziumTank;
    public static int ceziumQuota;

	void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
