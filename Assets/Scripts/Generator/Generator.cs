using System;
using UnityEngine;

[Serializable]
public class Generator : ScriptableObject
{
	public Generator ()
	{
	}
	public virtual void Gen ()
	{
	}

	public virtual void OnInspectorGUI()
	{
	}
}


