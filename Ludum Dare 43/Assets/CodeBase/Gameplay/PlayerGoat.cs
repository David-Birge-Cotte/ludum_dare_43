using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class PlayerGoat : PlayerGoatBehavior 
{
	public float speed = 5.0f;
	public string Name;

	protected override void NetworkStart()
	{
		base.NetworkStart();

		// The server will destroy the network object when the owner disconnects
        if (networkObject.IsServer)
            networkObject.Owner.disconnected += delegate { DestroyPlayer(); };
		
		// Ask the server to call a function on all clients 
		// Buffered means the server will even call it on new players when connecting
		if (networkObject.IsOwner)
			networkObject.SendRpc(RPC_CHANGE_NAME, Receivers.AllBuffered, Name);
	}

	private void Update()
    {
		// if the network object is not ready
		if (networkObject == null)
			return;

		// If not owner, update position based on network informations
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            return;
        }

		// Do some gameplay
		Move();

		// Update the network object
        networkObject.position = transform.position;
    }

	private void Move()
	{
		Vector3 dir = new Vector3(
			Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical")
		);
		dir *= Time.deltaTime * speed;
		transform.position += dir;
	}

	// Clean network destroy
	private void DestroyPlayer()
	{
		networkObject.Destroy();
	}

	// A RPC is called by the server on all clients
	public override void ChangeName(RpcArgs args)
	{
		Name = args.GetNext<string>();
	}
}
