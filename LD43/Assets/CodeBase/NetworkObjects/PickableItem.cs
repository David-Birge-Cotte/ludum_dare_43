﻿using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class PickableItem : PickableItemBehavior 
{
	public uint lastPlayerIDTouched = 0;
	private Rigidbody2D rb2d;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	protected override void NetworkStart()
	{
		base.NetworkStart();
	}

	void Update () 
	{
		if (networkObject == null)
			return;

        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
			rb2d.velocity = networkObject.velocity;
            return;
        }
		networkObject.velocity = rb2d.velocity;
        networkObject.position = transform.position;
	}

	public override void Push(RpcArgs args)
	{
		lastPlayerIDTouched = args.GetNext<uint>();
		Vector2 dir = args.GetNext<Vector2>();

		if (lastPlayerIDTouched == 0)
			return;

		BMSLogger.DebugLog("-- push rpc by " + lastPlayerIDTouched + " --");
		rb2d.AddForce(dir, ForceMode2D.Impulse);
	}
}