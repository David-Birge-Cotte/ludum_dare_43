using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class GameLogic : GameLogicBehavior 
{
	// ------------------------------------------------------------------------
	// Variables
	public static GameLogic Instance;
	public Vector3 StartPos = new Vector3();
	public Transform PlayerTransform;
	// ------------------------------------------------------------------------

	private void Awake()
	{
		Instance = this;

        var player = NetworkManager.Instance.InstantiatePlayerGoat(position:StartPos);
		//PlayerTransform = player.transform;

		//BMSLogger.DebugLog("Player " + player.networkObject.NetworkId + " joined the game");
	}

	protected override void NetworkStart()
	{
		base.NetworkStart();
		BMSLogger.DebugLog("Network Start");

		if (!networkObject.IsServer)
			return;

		BMSLogger.DebugLog("Spawning items");
		for (int i = 0; i < 5; i++)
		{
			var item = NetworkManager.Instance.InstantiatePickableItem(
				position:new Vector3(Random.Range(-10, 10), Random.Range(-8, -2)));
		}
	}
}
