using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinCounter : MonoBehaviour {
	private float lastChangedTime;
	private GameManager gameManager;
	private int lastSettledCount = 10;
	public int lastStandingCount = -1;//NOT GOOD PRACTICE; to keep track of when a reset occurs
	private bool ballExitedBox = false;
	private Ball ball;
	private void Start()
	{
		ball = GameObject.FindObjectOfType<Ball>();
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	private void Update()
	{
		if (ballExitedBox)
		{
			UpdateStandingCount();
		}
	}
	public int CountStanding()
	{
		Pin[] activePins;
		int standingPins = 0;
		activePins = FindObjectsOfType<Pin>();
		//For each active pin, check if it is standing and update the 
		//number of standing pins accordingly
		foreach (Pin pin in activePins)
		{
			if (pin.isStanding())
			{
				standingPins++;
			}
		}
		return standingPins;
	}
	private void UpdateStandingCount()
	{
		int currentStanding = CountStanding();
		//Has there been a change to the last count of standing pins?
		if (currentStanding != lastStandingCount)
		{
			lastChangedTime = Time.time;
			lastStandingCount = currentStanding;
		}
		//How long to wait for pins to settle
		float settleTime = 3.0f;
		//If three seconds have passed, update the current standing and stick with what you have
		if ((Time.time - lastChangedTime) > settleTime)
		{
			PinsHaveSettled();
		}
	}
	private void PinsHaveSettled()
	{
		int standing = CountStanding();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;
		gameManager.Bowl(pinFall);
		ballExitedBox = false;
		//Indicates a new frame
		lastStandingCount = -1;
	}
	public void ResetPinCount()
	{
		lastSettledCount = 10;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == ball.name)
		{
			ballExitedBox = true;
		}
	}
}
