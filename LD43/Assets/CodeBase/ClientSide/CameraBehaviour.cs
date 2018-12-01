using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour 
{
	public Transform PlayerTransform = null;
	public float speed = 4;

	void Start () 
	{
		// Get the owned player
		PlayerTransform = GameLogic.Instance.PlayerTransform;
	}
	
	void FixedUpdate () 
	{
		if(PlayerTransform == null)
			return;

		transform.position = Vector3.Lerp(
			transform.position, 
			new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z), 
			Time.deltaTime * speed);
	}
}
