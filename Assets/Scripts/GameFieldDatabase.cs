using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

[Serializable]
public class GameFieldDatabase : ScriptableObject {
	public void RegisterBehavior(ScriptableObject scriptableObject)
	{
		AssetDatabase.AddObjectToAsset (scriptableObject, this);
		EditorUtility.SetDirty(scriptableObject);
		EditorApplication.SaveAssets();
		AssetDatabase.SaveAssets ();
		EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene());
	}

	public void UnRegisterAsset(ScriptableObject scriptableObject)
	{
		UnityEngine.Object.DestroyImmediate (scriptableObject, true);
		EditorApplication.SaveAssets();
		AssetDatabase.SaveAssets ();
		EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene());

	}


}