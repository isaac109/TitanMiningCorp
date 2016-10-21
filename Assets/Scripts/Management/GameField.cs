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
	private GeneratorManager generatorManager;

	void Start()
	{
		//TODO: setup up event for event
//		OnGameFieldReady.Invoke (this, new EventArgs ());
		generatorManager.Start ();
	}

	void Awake()
	{
		GameField.Instance = this;
		arenaWall.Start ();
		generatorManager.Awake ();
	}

	public ArenaWall ArenaWall{ 
		get { 
			if (arenaWall == null)
				arenaWall = new ArenaWall ();
			return arenaWall; 
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

