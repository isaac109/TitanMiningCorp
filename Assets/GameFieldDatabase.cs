using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[Serializable]
public class GameFieldDatabase : ScriptableObject {

	public void RegisterBehavior(ScriptableObject scritableObject)
	{
		AssetDatabase.AddObjectToAsset (scritableObject, this);

		EditorUtility.SetDirty(scritableObject);
		EditorUtility.SetDirty(this);
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh ();
	}

	public void UnRegisterAsset(ScriptableObject scriptableObject)
	{
		UnityEngine.Object.DestroyImmediate (scriptableObject, true);
	}
}