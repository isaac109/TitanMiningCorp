using System;
public interface IManager
{

	void Awake();
	void Start();

	#if UNITY_EDITOR
	void InspectorGUI (GameFieldDatabase gameFieldDatabase);
	void InspectorGUILoad();
	#endif
}

