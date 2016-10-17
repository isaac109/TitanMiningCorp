using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;


[GeneratorAttribute("Simple Generator")]
[Serializable]
public class SimpleGenerator : Generator
{
	[SerializeField]
	private GameObject asteroid;
	[SerializeField]
	private int numberOfAsteroids;
	[SerializeField]
	private float distanceThreshold;

	public SimpleGenerator ()
	{
		
	}

	public override void Gen ()
	{
		List<Vector3> asteroidSpawnPoints = new List<Vector3> ();

		for(int currentAsteroid = 0; currentAsteroid < numberOfAsteroids; currentAsteroid++)
		{
			bool hasFoundPosition = false;
			while (!hasFoundPosition) //Here, we pick a new Vector3 and check if it is too close to any other location picked thus far. If it is too close, pick another one
			{
				Vector3 newCoordinate = new Vector3(UnityEngine.Random.Range(-GameField.Instance.ArenaWall.radius, GameField.Instance.ArenaWall.radius), 0, UnityEngine.Random.Range(-GameField.Instance.ArenaWall.radius, GameField.Instance.ArenaWall.radius));
				if(Vector3.Distance(newCoordinate,Vector3.zero) >= GameField.Instance.ArenaWall.radius - 3.5 || Vector3.Distance(newCoordinate, Vector3.zero) < 4.0)
				{
					continue;
				} 
				if(currentAsteroid == 0) //if this is the first asteroid, there is no need to check its distance to other asteroids
				{
					asteroidSpawnPoints.Add(newCoordinate);
					hasFoundPosition = true;
				}
				else
				{
					bool hasForLoopFinished = true;
					foreach (Vector3 coordinate in asteroidSpawnPoints)
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
						asteroidSpawnPoints.Add(newCoordinate);
						hasFoundPosition = true;
					}
				}
			}
		}
		foreach(Vector3 coordinate in asteroidSpawnPoints)
		{
			GameObject.Instantiate(asteroid, coordinate, Quaternion.identity);
		}
	}

	public override void OnInspectorGUI ()
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Number Of Asteroids:");
		numberOfAsteroids = EditorGUILayout.IntField (numberOfAsteroids);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Distance Threshold:");
		distanceThreshold = EditorGUILayout.FloatField (distanceThreshold);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("To Spawn:");
		asteroid = (GameObject)EditorGUILayout.ObjectField (asteroid, typeof(GameObject), true);
		GUILayout.EndHorizontal ();
		base.OnInspectorGUI ();
	}


}


