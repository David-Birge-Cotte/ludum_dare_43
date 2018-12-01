using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour 
{
	public Transform PlayerTransform = null;

	void Start () 
	{
		// Get the owned player
		PlayerTransform = GameLogic.Instance.PlayerTransform;
	}
	
	void Update () 
	{
		if(PlayerTransform == null)
			return;

		transform.position = new Vector3(
			PlayerTransform.position.x, 
			PlayerTransform.position.y, 
			transform.position.z);
	}
}
