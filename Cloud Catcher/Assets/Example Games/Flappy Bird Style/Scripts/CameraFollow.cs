﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform target;		//target for the camera to follow
	public float xOffset;			//how much x-axis space should be between the camera and target


	void Update()
	{
		//follow the target on the x-axis
		transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
	}
}
