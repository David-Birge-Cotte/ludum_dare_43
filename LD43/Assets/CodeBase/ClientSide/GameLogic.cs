﻿﻿using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class GameLogic : MonoBehaviour 
{
	// ------------------------------------------------------------------------
	// Variables
	public static GameLogic Instance;
	public Vector3 StartPos = new Vector3();
	public Transform PlayerTransform;
	// ------------------------------------------------------------------------

	private void Start()
	{
		Instance = this;

        var player = NetworkManager.Instance.InstantiatePlayer(position:StartPos);
		PlayerTransform = player.transform;

		BMSLogger.DebugLog("Player " + player.networkObject.NetworkId + " joined the game");
	}

	public static Player GetPlayerByID(uint id)
	{
		Player[] pls = GameObject.FindObjectsOfType<Player>();

		foreach (var p in pls)
		{
			if(p.networkObject.NetworkId == id)
				return p;
		}
		return null;
	}

	public void Quit()
	{
		//NetworkManager.Instance.Networker.Disconnect(true);
		Application.Quit(); // will do everything
	}
}