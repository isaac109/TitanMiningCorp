using System;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(GameField))]
public class GameFieldWrapper : Editor
{
	protected GameFieldDatabase assetDatabase;

	AnimBool showArenaConfigurations;
	AnimBool showGenerators;

	private bool isLoaded = false;
	public GameFieldWrapper ()
	{
		assetDatabase = AssetDatabase.LoadAssetAtPath<GameFieldDatabase> ("Assets/TitanMining/" + SceneManager.GetActiveScene ().name + ".asset");
		if ( assetDatabase == null) {
			assetDatabase = ScriptableObject.CreateInstance<GameFieldDatabase> ();
			AssetDatabase.CreateAsset (assetDatabase, "Assets/TitanMining/" + SceneManager.GetActiveScene().name + ".asset");
		}
	}

	void OnEnable(){
		showArenaConfigurations = new AnimBool(true);
		showArenaConfigurations.valueChanged.AddListener(Repaint);

		showGenerators = new AnimBool(true);
		showGenerators.valueChanged.AddListener(Repaint);
	}
		

	#if UNITY_EDITOR
	public override void OnInspectorGUI()
	{
		GameField gameField = (GameField)target;

		if (!isLoaded) {
			gameField.GeneratorManager.InspectorGUILoad ();
			gameField.ArenaWall.InspectorGUILoad ();
			isLoaded = true;
		}

		showGenerators.target = EditorGUILayout.ToggleLeft("Show Generator Configurations", showGenerators.target);
		if (EditorGUILayout.BeginFadeGroup (showGenerators.faded)) {
			gameField.GeneratorManager.InspectorGUI (assetDatabase);
		}
		EditorGUILayout.EndFadeGroup();

		showArenaConfigurations.target = EditorGUILayout.ToggleLeft("Show Arena Configurations", showArenaConfigurations.target);
		if (EditorGUILayout.BeginFadeGroup (showArenaConfigurations.faded)) {
			gameField.ArenaWall.InspectorGUI (assetDatabase);
		}
		EditorGUILayout.EndFadeGroup();
		//base.OnInspectorGUI();


	}
	#endif

}

