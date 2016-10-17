using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
[Serializable]
public class GeneratorManager : System.Object, IManager
{

	[SerializeField]
	private List<Generator> activeGenerator;

	public GeneratorManager ()
	{
		if (activeGenerator != null)
			activeGenerator = new List<Generator> ();
	}

	public void Awake ()
	{
	}

	public void Start ()
	{
	}

	#if UNITY_EDITOR

	[NonSerialized]
	int selected = 0;
	[NonSerialized]
	string[] options = new string[]{};
	[NonSerialized]
	List<Type> generators = new List<Type>();

	public void InspectorGUILoad ()
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
	}
		
	public void InspectorGUI(GameFieldDatabase gameFieldDatabase)
	{
		GUILayout.BeginHorizontal ();
		selected = EditorGUILayout.Popup("Generators", selected, options); 
		if (GUILayout.Button ("Add") && selected != -1) {
			if (activeGenerator == null)
				activeGenerator = new List<Generator> ();
			
			Generator generator = (Generator)ScriptableObject.CreateInstance(generators [selected]);
			gameFieldDatabase.RegisterBehavior (generator);

			activeGenerator.Add (generator);
		}
		GUILayout.EndHorizontal ();

		if(activeGenerator != null)
			for (int x = 0; x < activeGenerator.Count; x++) {
				//GUILayout.Label ((gameField.generators [x].GetType ().GetCustomAttributes(typeof(GeneratorAttribute),false)[0] as GeneratorAttribute).Name);
				if(GUILayout.Button("Delete"))
				{
					gameFieldDatabase.UnRegisterAsset (activeGenerator [x]);
					activeGenerator.RemoveAt (x);
					break;
				}
				activeGenerator [x].OnInspectorGUI ();
			}


	}
	#endif


}

