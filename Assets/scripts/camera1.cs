using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera1 : MonoBehaviour
{
    // Start is called before the first frame update
   float moveX; 
	float moveY;
	public GameObject player;       //Public variable to store a reference to the player game object
	private Vector3 moveCamera;

	private Vector3 offset;         //Private variable to store the offset distance between the player and camera		// Use this for initialization
	void Start () 
	{			//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		

	}

		// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		moveCamera = transform.position;
		offset = transform.position - player.transform.position;
		moveX = offset.x;
		moveY = offset.y;				
		if ((offset.x < -4.5f) && (player.transform.position.x < 18f))
        {
			moveCamera.x = player.transform.position.x - 4.5f;
//			transform.position = moveCamera; 
		}
		if ((offset.x > 2f) && (player.transform.position.x > -18f))
        {
			moveCamera.x = player.transform.position.x + 2f;
		}
		if ((offset.y < - 2) && (player.transform.position.y < 8f))
        {
			moveCamera.y = player.transform.position.y - 2;
//			transform.position = moveCamera;
		}
		if ((offset.y > 2) && (player.transform.position.y > -8f)) {
			moveCamera.y = player.transform.position.y + 2;
//			transform.position = moveCamera;
		}
		transform.position = Vector3.Lerp (transform.position, moveCamera, 0.2f);
		transform.position = moveCamera;

	}
}
