using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Ball bowlingBall;
	private Vector3 offset;
	private float stopPos = 1839f;//1839 = In front of head pin
	private void Start ()
	{
		offset = transform.position - bowlingBall.transform.position;
	}
	private void Update ()
	{
		//Follow the ball until you reach the first pin's position
		if (bowlingBall.transform.position.z <= stopPos)
		{
			transform.position = bowlingBall.transform.position + offset;
		}
	}
}
