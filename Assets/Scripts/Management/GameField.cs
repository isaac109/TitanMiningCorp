using System;
using UnityEngine;
using System.Collections.Generic;


public class GameField : MonoBehaviour
{
	public static GameField Instance;

	[SerializeField]
	private ArenaWall arenaWall;
	[SerializeField]
	private DebrisManager debrisManager;

	[SerializeField]
	private GeneratorManager generatorManager;

	[SerializeField]
	public List<Generator> generators;

	public GameField ()
	{

	}
	void Start()
	{
		
	}

	void Awake()
	{
		GameField.Instance = this;
		arenaWall.Start ();
		debrisManager.Start ();
	}

	public ArenaWall GetArenaWall()
	{
		return arenaWall;
	}

	public void OnGUILoad()
	{
		if (generatorManager == null) {
			generatorManager = new GeneratorManager ();

		}
		generatorManager.OnGUILoad ();
	}

	public void InspectorGUI(GameFieldDatabase gameFieldDatabase)
	{
		
		generatorManager.InspectorGUI (gameFieldDatabase);
	}
		
}

