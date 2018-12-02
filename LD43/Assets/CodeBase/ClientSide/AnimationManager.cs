using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class AnimationManager : MonoBehaviour 
{
	public Sprite Right, Left, Top, Down;

	private Vector2 dir, prevPos;
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () 
	{
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 pos = new Vector2(transform.position.x, transform.position.y) ;
		dir = pos - prevPos;
		DisplayCorrectSprite();
		prevPos = pos;
	}

	void DisplayCorrectSprite()
	{
		if (dir != Vector2.zero)
		{
			dir = dir.normalized;
			if (dir.x > 0.5f)
			{
				sr.sprite = Right;
			}
			else if (dir.x < -0.5f)
			{
				sr.sprite = Left;
			}
			else if(dir.y > -0.5f)
			{
				sr.sprite = Top;
			}
			else if (dir.y < -0.5f)
			{
				sr.sprite = Down;
			}
		}
	}
}
