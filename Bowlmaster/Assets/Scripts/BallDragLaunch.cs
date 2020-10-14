using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Ball))]
public class BallDragLaunch : MonoBehaviour {

	private Ball ball;
	private Vector3 dragStart;
	private Vector3 dragEnd;
	private float startTime;
	private float endTime;
	private float floorStartWidth = -52;
	private float floorEndWidth = 52;

	private void Start() {
		ball = GetComponent<Ball>();
	}
	public void DragStart()
	{
		//Capture time and position of drag start
		//if the ball is not launched
		if (!ball.GetIsBallLaunched())
		{
			dragStart = Input.mousePosition;
			startTime = Time.time;
		}
	}
	public void DragEnd()
	{
		//if the ball is not launched
		if (!ball.GetIsBallLaunched())
		{
			//Launch the ball in the given direction 
			//between drag start and drag end
			dragEnd = Input.mousePosition;
			endTime = Time.time;

			float dragDuration = Mathf.Abs(endTime - startTime);
			float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
			//The drag's y pos = Z axis in gamespace
			float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;
			Vector3 launchVelocity = new Vector3(launchSpeedX, 0f, launchSpeedZ);
			ball.Launch(launchVelocity);
		}
	}
	public void MoveStart(float xNudge)
	{
		//As long as the ball is within the bounds of the playspace and
		//the ball has not been launched, nudge the ball either left or right
		if ((ball.transform.position.x >= floorStartWidth  && 
			ball.transform.position.x <= floorEndWidth) &&
			!ball.GetIsBallLaunched())
		{
			ball.transform.Translate(new Vector3(xNudge, 0, 0));
			Debug.Log(xNudge.ToString() + " has been added to x");
		}
	}
}
