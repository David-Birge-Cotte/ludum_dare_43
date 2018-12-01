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
		if(!networkObject.IsOwner)
		{
			Destroy(GetComponent<Rigidbody2D>());
			return;
		}
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		if (networkObject == null)
			return;

        if (!networkObject.IsOwner)
        {
			rb2d.velocity = networkObject.velocity;
            transform.position = networkObject.position;
            return;
        }

		networkObject.velocity = rb2d.velocity;
        networkObject.position = transform.position;
	}

	public override void Push(RpcArgs args)
	{
		if (!networkObject.IsOwner)
			return;

		Debug.Log("PUSH RPC");
		Vector2 dir = args.GetNext<Vector2>();
		StartCoroutine(Move((Vector2)transform.position + dir));
	}

	public IEnumerator Move(Vector3 newPos)
	{
		float t = 0;
		while (t < 1)
		{
			rb2d.MovePosition(Vector2.Lerp(transform.position, newPos, t));
			t += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}
}
