using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	public float standingThreshold = 10f;
	//Offset due to rotation in unity 
	private float rotationOffset = 270f;
	private float distanceToRaise = 30f;
	private Rigidbody ridgedbodyOfPin;

	private void Start () {
		ridgedbodyOfPin = GetComponent<Rigidbody>();
		isStanding();
	}
	public bool isStanding()
	{
		Vector3 rotationInEulerAngles = transform.rotation.eulerAngles; 
		
		float tiltInX = Mathf.Abs(rotationOffset - transform.rotation.eulerAngles.x);
		float tiltInZ = Mathf.Abs(transform.rotation.eulerAngles.z);

		//Check if the tiltInX or tiltInZ is less than the threshold OR
		//If it is between 360 and 360- standingThreshold) *Due to use of abs value*
		if ((tiltInX < standingThreshold || (tiltInX <= 360.0f && tiltInX >= 360f - standingThreshold))
			&& (tiltInZ < standingThreshold) || (tiltInZ <= 360.0f && tiltInZ >= 360f - standingThreshold))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public void Raise()
	{
		//If the pin is standing, raise the pins
		if (isStanding())
		{
			transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
			ridgedbodyOfPin.useGravity = false;
		}
	}
	public void Lower()
	{
		//Lower the pin back to the ground
		transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
		transform.rotation = Quaternion.Euler(-90f, 0, 0);
		ridgedbodyOfPin.useGravity = true;
	}
}
