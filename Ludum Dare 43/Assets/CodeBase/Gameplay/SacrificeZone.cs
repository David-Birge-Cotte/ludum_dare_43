using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class SacrificeZone : SacrificeZoneBehavior 
{
	public override void Sacrifice(RpcArgs args)
	{
		uint playerID = args.GetNext<uint>();

		PlayerGoat p = null;
		var pg = GameObject.FindObjectsOfType(typeof(PlayerGoat)) as PlayerGoat[];
		foreach (var pl in pg)
		{
			if(pl.PlayerID == playerID)
				p = pl;
		}

		if (p == null)
			Debug.LogError("No Player ID ??!");
		else
			p.networkObject.score++;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!networkObject.IsServer)
			return;

		if (col.GetComponent<PickableItem>())
		{
			Debug.Log("Item in sacrifice zone");
			networkObject.SendRpc(RPC_SACRIFICE, Receivers.Server,
				col.GetComponent<PickableItem>().lastPlayerIDTouched);
		}
	}
}
