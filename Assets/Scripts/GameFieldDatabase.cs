using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[Serializable]
public class GameFieldDatabase : ScriptableObject {
	public void RegisterBehavior(ScriptableObject scriptableObject)
	{
		AssetDatabase.AddObjectToAsset (scriptableObject, this);
		EditorUtility.SetDirty(scriptableObject);
		EditorUtility.SetDirty (this);
		EditorApplication.SaveAssets();
		AssetDatabase.SaveAssets ();
		EditorApplication.MarkSceneDirty ();
	}

	public void UnRegisterAsset(ScriptableObject scriptableObject)
	{
		UnityEngine.Object.DestroyImmediate (scriptableObject, true);
		EditorApplication.SaveAssets();
		AssetDatabase.SaveAssets ();
		EditorApplication.MarkSceneDirty ();

	}


}