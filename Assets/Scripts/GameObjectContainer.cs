using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

[CreateAssetMenuAttribute]
public class GameObjectContainer : ScriptableObject
{

	[SerializeField]
	private GameObject[] gameObjects;
	private Dictionary<string,GameObject> gameObjectAssoc = new Dictionary<string, GameObject>();

	void OnEnable()
	{
		if(gameObjectAssoc.Count == 0)
			for (int x = 0; x < gameObjects.Length; x++) {
				gameObjectAssoc.Add (gameObjects [x].name, gameObjects [x]);
			}
	}

	public ReadOnlyCollection<GameObject> GameObjects {
		get{
			return new ReadOnlyCollection<GameObject> (gameObjects);
		}
	}


	public GameObject GetGameObjectByName(string name)
	{
		if (!gameObjectAssoc.ContainsKey (name))
			UnityEngine.Debug.Log ("coulden't find in collection:" + name);
		return gameObjectAssoc [name];
	}

	public GameObject GetGameObjectByClone(GameObject go)
	{
		return gameObjectAssoc[HelperGameObject.RemoveClone (go.name)];
	}


}