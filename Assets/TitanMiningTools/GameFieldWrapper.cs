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
	int selected = 0;
	string[] options = new string[]{};
	List<Type> generators = new List<Type>();
	protected ItemDatabase assetDatabase;

	public GameFieldWrapper ()
	{
		generators.Clear ();

		IEnumerable<Assembly> scriptAssemblies = AppDomain.CurrentDomain.GetAssemblies ().Where ((Assembly assembly) => assembly.FullName.Contains ("Assembly"));
		List<String> options = new List<string> ();
		foreach (Assembly assembly in scriptAssemblies) 
		{
			foreach (Type type in assembly.GetTypes().Where(T => T.IsClass && T.IsSubclassOf(typeof(Generator))))
			{
				object[] nodeAttributes = type.GetCustomAttributes(typeof(GeneratorAttribute), false);                    
				GeneratorAttribute attr = nodeAttributes[0] as GeneratorAttribute;
				if (attr != null) {
					generators.Add (type);
					options.Add (attr.Name);
				}
			}
		}
		this.options = options.ToArray ();

		assetDatabase = AssetDatabase.LoadAssetAtPath<ItemDatabase> ("Assets/TitanMining/" + SceneManager.GetActiveScene ().name + ".asset");
		if ( assetDatabase == null) {
			assetDatabase = ScriptableObject.CreateInstance<ItemDatabase> ();
			AssetDatabase.CreateAsset (assetDatabase, "Assets/TitanMining/" + SceneManager.GetActiveScene().name + ".asset");
		}
	}

	public override void OnInspectorGUI()
	{

		GameField gameField = (GameField)target;

		GUILayout.BeginHorizontal ();
		selected = EditorGUILayout.Popup("Generators", selected, options); 
		if (GUILayout.Button ("Add") && selected != -1) {
			if (gameField.generators == null)
				gameField.generators = new List<Generator> ();
			Generator generator = (Generator)ScriptableObject.CreateInstance((generators [selected]));

			AssetDatabase.AddObjectToAsset (generator, assetDatabase);
			assetDatabase.items.Add (generator);
			EditorUtility.SetDirty(assetDatabase);


			gameField.generators.Add (generator);
		}
		GUILayout.EndHorizontal ();

		if(gameField.generators != null)
		for (int x = 0; x < gameField.generators.Count; x++) {
			//GUILayout.Label ((gameField.generators [x].GetType ().GetCustomAttributes(typeof(GeneratorAttribute),false)[0] as GeneratorAttribute).Name);
			if(GUILayout.Button("Delete"))
			{
				UnityEngine.Object.DestroyImmediate (gameField.generators [x], true);
				gameField.generators.RemoveAt (x);
				break;
			}
			gameField.generators [x].OnInspectorGUI ();
		}

		base.OnInspectorGUI ();

	}

}

