using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PinSetter : MonoBehaviour {
	private float lastChangedTime;
	public GameObject pinSet;
	private Animator animator;
	private PinCounter pinCounter;
	private void Start()
	{
		animator = GetComponent<Animator>();
		pinCounter = GameObject.FindObjectOfType<PinCounter>();
	}
	public void RaisePins()
	{
		//Raise each pin that is active and standing. 
		foreach (Pin pin in FindObjectsOfType<Pin>())
		{
			pin.Raise();
		}
	}
	public void LowerPins()
	{
		//Lower the pin back to the ground
		foreach (Pin pin in FindObjectsOfType<Pin>())
		{
			pin.Lower();
		}
	}
	public void CreateNewPins()
	{
		//Create new pins that are 20 cms off the ground
		//*20 cms is to allow them to collide and not fall through the floor*
		GameObject newPins = Instantiate(pinSet);
		newPins.transform.position += new Vector3(0, 20, 0);
	}
	public void LaneManagement(ActionMaster.Action action)
	{
		switch(action)
		{
			case (ActionMaster.Action.Tidy):
				animator.SetTrigger("tidyTrigger");
				break;
			case (ActionMaster.Action.Reset):
				animator.SetTrigger("resetTrigger");
				pinCounter.ResetPinCount();
				break;
			case (ActionMaster.Action.EndTurn):
				animator.SetTrigger("resetTrigger");
				pinCounter.ResetPinCount();
				break;
			default:
				break;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		//Destroy the pins as they fall out of the play space
		if (other.gameObject.GetComponent<Pin>())
		{
			Destroy(other.gameObject);
		}
	}	
}
