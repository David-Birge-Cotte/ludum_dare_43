using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class PickableItem : PickableItemBehavior 
{

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
            return;
        }
        networkObject.position = transform.position;
	}

	public override void Push(RpcArgs args)
	{
		//if (!networkObject.IsOwner)
			//return;

		Debug.Log("PUSH RPC");
		Vector2 dir = args.GetNext<Vector2>();
		StartCoroutine(Move((Vector2)transform.position + dir));
	}

	public IEnumerator Move(Vector3 newPos)
	{
		float t = 0;
		while (t < 1)
		{
			transform.position = Vector2.Lerp(transform.position, newPos, t);
			t += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}
}
