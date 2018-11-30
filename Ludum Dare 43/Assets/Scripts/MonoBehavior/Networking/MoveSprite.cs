using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class MoveSprite : MovingSpriteBehavior 
{
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
}
