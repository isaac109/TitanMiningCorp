using System;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(GameField))]
public class GameFieldWrapper : Editor
{
	protected GameFieldDatabase assetDatabase;

	private bool isLoaded = false;
	public GameFieldWrapper ()
	{
		assetDatabase = AssetDatabase.LoadAssetAtPath<GameFieldDatabase> ("Assets/TitanMining/" + SceneManager.GetActiveScene ().name + ".asset");
		if ( assetDatabase == null) {
			assetDatabase = ScriptableObject.CreateInstance<GameFieldDatabase> ();
			AssetDatabase.CreateAsset (assetDatabase, "Assets/TitanMining/" + SceneManager.GetActiveScene().name + ".asset");
		}
	}

	public override void ReloadPreviewInstances ()
	{
		base.ReloadPreviewInstances ();
	}

	public override void OnInspectorGUI()
	{
		GameField gameField = (GameField)target;

		if (!isLoaded) {
			gameField.OnGUILoad ();
			isLoaded = true;
		}
			
	
		gameField.InspectorGUI (assetDatabase);
		base.OnInspectorGUI ();


	}

}

