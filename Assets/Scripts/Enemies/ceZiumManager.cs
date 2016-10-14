using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ceZiumManager : MonoBehaviour {

	//This script is used to manage all currently active CeZium Units. It contains only a list and the methods for modifying that list
	public static int exposedCeziumUnits;
	public static List<Transform> ceziumLocations = new List<Transform>();

	void Start(){
		ceziumLocations.Clear ();
	}

	public static void AddToList(Transform newCezium){
		ceziumLocations.Add (newCezium);
	}

	public static void SubtractFromList(Transform takenCezium){
		ceziumLocations.Remove (takenCezium);
	}
}
