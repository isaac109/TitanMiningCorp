using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebrisManager : MonoBehaviour {

    public GameObject Asteroid;

    public int numberOfAsteroids;

    public GameObject arenaManager;
    ArenaWall arenaData;

    public float distanceThreshold;

    List<Vector3> asteroidPoints = new List<Vector3>();

	// Use this for initialization
	void Start () {
        arenaData = arenaManager.GetComponent<ArenaWall>();
        SelectPoints(arenaData.radius, distanceThreshold);
        foreach(Vector3 coordinate in asteroidPoints)
        {
            GameObject newAsteroid = Instantiate(Asteroid, coordinate, Quaternion.identity) as GameObject;
            newAsteroid.transform.parent = gameObject.transform;
        }
	}
	

    void SelectPoints(float maxDistance, float distanceThreshold) //Asteroid Placement
    {
        for(int currentAsteroid = 0; currentAsteroid < numberOfAsteroids; currentAsteroid++)
        {
           bool hasFoundPosition = false;
           while (!hasFoundPosition) //Here, we pick a new Vector3 and check if it is too close to any other location picked thus far. If it is too close, pick another one
            {
                Vector3 newCoordinate = new Vector3(Random.Range(-maxDistance, maxDistance), 0, Random.Range(-maxDistance, maxDistance));
                if(Vector3.Distance(newCoordinate,Vector3.zero) >= arenaData.radius - 3.5 || Vector3.Distance(newCoordinate, Vector3.zero) < 4.0)
                {
                    continue;
                } 
                if(currentAsteroid == 0) //if this is the first asteroid, there is no need to check its distance to other asteroids
                {
                    asteroidPoints.Add(newCoordinate);
                    hasFoundPosition = true;
                }
                else
                {
                    bool hasForLoopFinished = true;
                    foreach (Vector3 coordinate in asteroidPoints)
                    {
                        //Check the distance between the newly created coordinate and every coordinate in the asteroid list so far
                        if(Vector3.Distance(newCoordinate,coordinate) < distanceThreshold)
                        {
                            //New coordinate is too close to other asteroids
                            hasForLoopFinished = false;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (hasForLoopFinished)
                    {
                        asteroidPoints.Add(newCoordinate);
                        hasFoundPosition = true;
                    }
                }
            }
        }
    }
}
