using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class DropZone : DropZoneBehavior 
{
	public override void Drop(RpcArgs args)
	{
		uint playerID = args.GetNext<uint>();

		GameLogic.GetPlayerByID(playerID).networkObject.score++;
		BMSLogger.DebugLog("Player scored : " + playerID);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		BMSLogger.DebugLog("Trig enter");
		if (!networkObject.IsServer)
			return;
		
		PickableItem item = other.GetComponent<PickableItem>();
		if (item != null)
		{
			BMSLogger.DebugLog("before rpc");
			networkObject.SendRpc(RPC_DROP, Receivers.Owner, item.lastPlayerIDTouched);
			item.networkObject.Destroy();
		}
	}
}
