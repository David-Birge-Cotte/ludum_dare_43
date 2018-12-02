using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class ServerSide : ServerSideBehavior 
{
	public static ServerSide Instance;
	private int maxItems = 10;
	protected override void NetworkStart()
	{
		base.NetworkStart();

		if (!networkObject.IsServer)
			Destroy(this);
		else
		{
			Instance = this;
			InvokeRepeating("CheckAndRespawnItems", 0, 10);
		}
	}

	void CheckAndRespawnItems()
	{
		int itemNb = GameObject.FindObjectsOfType<PickableItem>().Length;
		int newItemNb = maxItems - itemNb;

		for (int i = 0; i < newItemNb; i++)
		{
			BMSLogger.DebugLog("Spawning 1 item");
			var item = NetworkManager.Instance.InstantiatePickableItem(
				position:new Vector3(Random.Range(-10, 10), Random.Range(-20, -5)));
		}
	}
}
