using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArenaWall : MonoBehaviour {

    public GameObject Buoy;
    List<GameObject> buoyList = new List<GameObject>();

    public int buoyCount = 1;
    public float radius;

    const float TWOPI = Mathf.PI * 2;

	void Start () {
        float increment = TWOPI / buoyCount; //determine an increment distance based on the number of buoys
        float distanceAround = 0;
	    for(int i = 1; i <= buoyCount; i++)
        {
            Vector3 placementPos = new Vector3(Mathf.Sin(distanceAround) * radius, 0, Mathf.Cos(distanceAround) * radius); //use Sine and Cosine to determine placement
            Quaternion direction = Quaternion.LookRotation(-placementPos); //the placementPos is negative so the buoys are facing towards the center, rather than away

            GameObject newBuoy = Instantiate(Buoy, placementPos, direction) as GameObject;
            newBuoy.transform.parent = this.gameObject.transform;
            buoyList.Add(newBuoy);
            distanceAround += increment;
        }
        //Get the angle between buoys and scale the barriers to fill the gap between them
        for(int buoy = 0; buoy < buoyList.Count; buoy++)
        {
            //Special condition if operating on the last buoy in the list
            if (buoy == buoyList.Count - 1)
            {
                float lastDistance = Vector3.Distance(buoyList[buoy].transform.position, buoyList[0].transform.position);
                Vector3 lastRelativePos = buoyList[0].transform.position - buoyList[buoy].transform.position;
                buoyList[buoy].GetComponent<BarrierBuoy>().ScaleBarrier(lastRelativePos, lastDistance);
            }
            else
            {
                float distance = Vector3.Distance(buoyList[buoy].transform.position, buoyList[buoy + 1].transform.position);
                Vector3 relativePos = buoyList[buoy + 1].transform.position - buoyList[buoy].transform.position; //Make sure the barriers are facing the right way
                buoyList[buoy].GetComponent<BarrierBuoy>().ScaleBarrier(relativePos, distance);
            }
        }
	}
}
