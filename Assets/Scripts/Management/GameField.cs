using System;
using UnityEngine;
using System.Collections.Generic;


public class GameField : MonoBehaviour
{
	public static GameField Instance;

	public event EventHandler OnGameFieldReady;

	[SerializeField]
	private ArenaWall arenaWall;
	[SerializeField]
	private DebrisManager debrisManager;
	[SerializeField]
	private GeneratorManager generatorManager;
	[SerializeField]
	public List<Generator> generators;

	void Start()
	{
		//TODO: setup up event for event
		OnGameFieldReady.Invoke (this, new EventArgs ());
	}

	void Awake()
	{
		GameField.Instance = this;
		arenaWall.Start ();
		debrisManager.Start ();
	}

	public ArenaWall ArenaWall{ 
		get { 
			if (arenaWall == null)
				arenaWall = new ArenaWall ();
			return arenaWall; 
		} 
	}

	public DebrisManager DebrisManager{ 
		get { 
			if (debrisManager == null)
				debrisManager = new DebrisManager ();
			return debrisManager; 
		} 
	}

	public GeneratorManager GeneratorManager{ 
		get { 
			if (generatorManager == null)
				generatorManager = new GeneratorManager ();
			return generatorManager; 
		} 
	}


		
}

