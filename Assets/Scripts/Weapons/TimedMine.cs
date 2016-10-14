using UnityEngine;
using System.Collections;

public class TimedMine : Mine {

    public float delayTime;
    float timer;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= delayTime)
        {
            Detonate();
        }
	
	}
}
