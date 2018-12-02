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

		Player p = GameLogic.GetPlayerByID(playerID);
		p.networkObject.SendRpc(Player.RPC_ADD_SCORE, Receivers.AllBuffered, 1);
		BMSLogger.DebugLog("Player scored : " + playerID);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!networkObject.IsServer)
			return;
		
		PickableItem item = other.GetComponent<PickableItem>();
		if (item != null)
		{
			networkObject.SendRpc(RPC_DROP, Receivers.Owner, item.lastPlayerIDTouched);
			item.networkObject.Destroy();
		}
	}
}
