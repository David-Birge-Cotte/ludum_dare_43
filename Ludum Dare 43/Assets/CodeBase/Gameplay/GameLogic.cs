using System.Collections;
using System.Collections.Generic;

using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
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
}
