using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenuAttribute]
public class AssetManager: ScriptableSingleton<AssetManager>
{
	[SerializeField]
	public GameObjectContainer seralizableObjects;

	private Dictionary<Type,Raptor> raptors = new Dictionary<Type, Raptor>();

	public void Initialize()
	{
		raptors.Clear ();

		foreach(GameObject gameObject in seralizableObjects.GameObjects)
		{
			Raptor raptor = gameObject.GetComponent<Raptor> ();
			if (raptor != null) {
				raptors.Add (raptor.GetType (), raptor);
			}

		}

	}

	public Raptor GetRaptor<T>() where T : Raptor
	{
		Raptor raptor = null;
		if (raptors.TryGetValue (typeof(T),out raptor)) {
			return raptor;
		}

		return null;
	}

	/*	public T Create<T>() where T: SerializableBehavior,new()
	{
		return new T();
	}*/


}