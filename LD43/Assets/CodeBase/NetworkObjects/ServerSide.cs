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
				position: generateRd(15, 15, 3, 3, new Vector3(0.06f, 3.41f)));
				//position:new Vector3(Random.Range(-10, 10), Random.Range(-20, -5)));
		}
	}

	Vector3  generateRd(float maxX, float maxY, float minX, float minY, Vector3 Pivot)
	{
		float x = Random.Range(minX, maxX);
		float y = Random.Range(minY, maxY);

		if (Random.Range(0, 1.0f) > 0.5f)
		{
			x = -x;
		}

		if (Random.Range(0, 1.0f) > 0.5f)
		{
			y = -y;
		}

		return new Vector3(x, y) + Pivot;
	}
}
