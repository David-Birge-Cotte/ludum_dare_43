using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class GameLogic : GameLogicBehavior 
{
	public Vector3 StartPos = new Vector3();

	private void Start()
	{
		// This will be called on every client, so each client will essentially instantiate
        // their own player on the network. We also pass in the position we want them to spawn at
        NetworkManager.Instance.InstantiatePlayerGoat(position:StartPos);
	}
}
