using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerGoat : PlayerGoatBehavior 
{
	// ------------------------------------------------------------------------
	// Variables
	public float speed = 5.0f;
	public string Name;
	private Rigidbody2D rb2d;
	// ------------------------------------------------------------------------

	protected override void NetworkStart()
	{
		base.NetworkStart();

		// The server will destroy the network object when the owner disconnects
        if (networkObject.IsServer)
            networkObject.Owner.disconnected += delegate { DestroyPlayer(); };
	
		if (!networkObject.IsOwner)
		{
			Destroy(GetComponent<Rigidbody2D>());
			return;
		}
		rb2d = GetComponent<Rigidbody2D>();

		// Ask the server to call a function on all clients 
		// Buffered means the server will even call it on new players when connecting
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
		networkObject.direction = (Vector2)dir;
		rb2d.MovePosition(transform.position + dir);
	}

	private void Push(PickableItem item)
	{
		Vector2 dir = (item.transform.position - transform.position).normalized * 3;
		item.networkObject.SendRpc(PickableItem.RPC_PUSH, Receivers.Owner, dir);
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

	// ------------------------------------------------------------------------
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!networkObject.IsOwner)
			return;
		
		PickableItem item = other.transform.GetComponent<PickableItem>();
		if (item != null)
			Push(item);
	}
	// ------------------------------------------------------------------------
}
