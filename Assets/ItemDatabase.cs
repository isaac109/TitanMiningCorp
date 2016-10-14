using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class ItemDatabase : ScriptableObject {

	[SerializeField]
	public List<ScriptableObject> items;

	public void OnEnable () {
		if (items == null) {
			items = new List<ScriptableObject>();
		}
	}
}