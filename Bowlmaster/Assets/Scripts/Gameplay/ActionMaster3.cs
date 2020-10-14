using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster3 {

	public enum Action {Tidy, Reset, EndTurn, EndGame};
	public static ActionMaster3 Instance;
	private int[] bowls = new int[21];
	private int currentBowlNum = 0;
	private int oldPinCount = 0;

	private Action Bowl (int pins)
	{
		//Make sure the number of pins are within the range
		if (pins < 0 || pins > 10)
		{
			throw new UnityException("Invalid pin count");
		}
		//Do not continue the game if the current bowl is past the allowed limit
		if (currentBowlNum + 1 >= bowls.Length)
		{
			return Action.EndGame;
		}
		//Store the number of pins hit.
		bowls[currentBowlNum] = pins;
		//Either allow the last bowl or do not
		if (currentBowlNum >= 18 && LastBowlGranted())
		{
			//On the 20th frame, do not reset unless a strike was achieved
			if (currentBowlNum == 19 && pins != 10)
			{
				currentBowlNum++;
				return Action.Tidy;
			}
			else
			{
				currentBowlNum++;
				return Action.Reset;
			}
		}
		else
		if (currentBowlNum == 19 && !LastBowlGranted())
		{
			return Action.EndGame;
		}


		//If there is not a remainder, you are at the first bowl of the current frame
		//Therefore tidy
		if (currentBowlNum % 2 == 0)
		{
			if (pins == 10)
			{
				currentBowlNum += 2;
				return Action.EndTurn;
			}
			else
			{
				currentBowlNum++;
				oldPinCount = pins;
				return Action.Tidy;
			}
		}
		else
		{
			if (pins == 10)
			{
				currentBowlNum++;
				return Action.EndTurn;
			}
			currentBowlNum++;
			oldPinCount = 0;
			return Action.EndTurn;
		}
		throw new UnityException("Not sure what action needs to be thrown");
	}
	public static Action NextAction(List<int>pinFalls)
	{
		ActionMaster3 actionMaster = new ActionMaster3();
		Action currentAction = new Action();

		foreach (int pin in pinFalls)
		{
			currentAction = actionMaster.Bowl(pin);
		}
		return currentAction;
	}
	private bool LastBowlGranted()
	{
		if (bowls[18] >= 10 || bowls[19] >=10)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
