using UnityEngine;
using System.Collections;

public class CeziumUnit : MonoBehaviour {

    public int ceziumAmount;
	public int minCeziumAmount;
	public int maxCeziumAmount;

	void Start(){
		ceZiumManager.exposedCeziumUnits++;
		ceZiumManager.AddToList (gameObject.transform);
		ceziumAmount = (int)Random.Range (minCeziumAmount, maxCeziumAmount);
	}

}
