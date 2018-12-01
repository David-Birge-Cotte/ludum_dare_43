using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PickableItem : PickableItemBehavior 
{
	private Rigidbody2D rb2d;

	protected override void NetworkStart()
	{
		base.NetworkStart();

		rb2d = GetComponent<Rigidbody2D>();
		if (!networkObject.IsOwner)
		{
			Destroy(GetComponent<Rigidbody>());
			return;
		}
	}

	void Update () 
	{
		if (networkObject == null)
			return;

        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            return;
        }

        networkObject.position = transform.position;
	}
}
