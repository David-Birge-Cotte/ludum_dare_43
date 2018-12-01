﻿using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class GameLogic : GameLogicBehavior 
{
	public static GameLogic Instance;
	public Vector3 StartPos = new Vector3();
	public Transform PlayerTransform;

	private void Awake()
	{
		Instance = this;

        var player = NetworkManager.Instance.InstantiatePlayerGoat(position:StartPos);
		PlayerTransform = player.transform;
	}

	protected override void NetworkStart()
	{
		base.NetworkStart();

		if(!networkObject.IsServer)
			return;

		for (int i = 0; i < 5; i++)
		{
			var item = NetworkManager.Instance.InstantiatePickableItem();
			item.transform.position = new Vector3(Random.Range(-10, 0), Random.Range(-10, 0));
			item.networkObject.position = item.transform.position;
		}
	}

	public override void SpawnItem(RpcArgs args)
	{
		// not needed in fact
	}
}