using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class ServerSide : ServerSideBehavior 
{
	public static ServerSide Instance;

	protected override void NetworkStart()
	{
		base.NetworkStart();

		if (!networkObject.IsServer)
			Destroy(this);
		else
		{
			Instance = this;
			SpawnItems();
		}
	} 

	void SpawnItems()
	{
		BMSLogger.DebugLog("Spawning items");
		for (int i = 0; i < 5; i++)
		{
			var item = NetworkManager.Instance.InstantiatePickableItem(
				position:new Vector3(Random.Range(-10, 10), Random.Range(-8, -2)));
		}
	} 
}
