using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController Instance;

	public GameController ()
	{

	}

	void Awake()
	{
		GameController.Instance = this;
	}

	void Start(){
		AssetManager.Instance.Initialize ();
	}


}


