using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour {
	public Vector3 velocity;

	private Rigidbody rididbody;
	private AudioSource audioSource;
	private bool isBallLaunched = false;
	private Vector3 ballStartPos;

	private void Start () {
		rididbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		ballStartPos = gameObject.transform.position;
		rididbody.useGravity = false;
	}
	public void Launch(Vector3 velocity)
	{
		//Launch the ball with the given velocity
		rididbody.velocity = velocity;
		rididbody.useGravity = true;
		audioSource.Play();
		isBallLaunched = true;
	}
	public void ResetBall()
	{
		//Reset the ball back to the captured start origin with no gravity or velocity
		rididbody.velocity = Vector3.zero;
		rididbody.angularVelocity = Vector3.zero;
		rididbody.useGravity = false;
		gameObject.transform.position = ballStartPos;
		gameObject.transform.rotation = Quaternion.identity;
		isBallLaunched = false;

	}
	public bool GetIsBallLaunched()
	{
		return isBallLaunched;
	}
}
