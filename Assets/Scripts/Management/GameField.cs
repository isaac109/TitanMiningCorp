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

		
}

