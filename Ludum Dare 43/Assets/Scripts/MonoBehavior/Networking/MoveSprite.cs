using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;

public class MoveSprite : MovingSpriteBehavior 
{

	protected override void NetworkStart()
    {
        base.NetworkStart();

        if (networkObject.IsServer)
            networkObject.Owner.disconnected += delegate { DestroyServer(); };
    }

	private void Update () 
	{
		if(networkObject == null)
			return;

		if (!networkObject.IsOwner)
		{
			transform.position = networkObject.Position;
			return;
		}

		transform.position += new Vector3(
			Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical"),
			0
		) * Time.deltaTime * 5f;

		networkObject.Position = transform.position;
	}

	private void DestroyServer()
    {
		NetworkManager.Instance.Disconnect(); // devrait tout déconnecter proprement
        networkObject.Destroy(); // Devrait détruire ce gameobject si cette ligne est appelée ^^
    }
}
