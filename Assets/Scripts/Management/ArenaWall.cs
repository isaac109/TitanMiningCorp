using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

[System.Serializable]
public class ArenaWall : System.Object , IManager{
	[NonSerialized]
    List<GameObject> buoyList = new List<GameObject>();

	[SerializeField]
	public GameObject Buoy;

	[SerializeField]
    public int buoyCount = 1;
	[SerializeField]
	public float radius;

    const float TWOPI = Mathf.PI * 2;

	public void Start () {
        float increment = TWOPI / buoyCount; //determine an increment distance based on the number of buoys
        float distanceAround = 0;
	    for(int i = 1; i <= buoyCount; i++)
        {
            Vector3 placementPos = new Vector3(Mathf.Sin(distanceAround) * radius, 0, Mathf.Cos(distanceAround) * radius); //use Sine and Cosine to determine placement
            Quaternion direction = Quaternion.LookRotation(-placementPos); //the placementPos is negative so the buoys are facing towards the center, rather than away

			GameObject newBuoy = GameObject.Instantiate(Buoy, placementPos, direction) as GameObject;
            //newBuoy.transform.parent = this.gameObject.transform;
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

	public void Awake ()
	{
	}


	#if UNITY_EDITOR
	public void InspectorGUI (GameFieldDatabase gameFieldDatabase)
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Buoy:");
		Buoy = (GameObject)EditorGUILayout.ObjectField (Buoy,typeof(GameObject),true);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Buoy Count:");
		buoyCount = EditorGUILayout.IntField (buoyCount);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("radius:");
		radius = EditorGUILayout.FloatField (radius);
		GUILayout.EndHorizontal ();
	}

	public void InspectorGUILoad ()
	{
	}
	#endif


}
