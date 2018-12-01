using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class PlayerGoat : PlayerGoatBehavior 
{
	public float speed = 5.0f;

	protected override void NetworkStart()
	{
		base.NetworkStart();

		// The server will destroy the network object when the owner disconnects
        if (networkObject.IsServer)
            networkObject.Owner.disconnected += delegate { DestroyPlayer(); };
	}

	private void Update()
    {
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            return;
        }

		Move();
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

	private void DestroyPlayer()
	{
		networkObject.Destroy();
	}
}
