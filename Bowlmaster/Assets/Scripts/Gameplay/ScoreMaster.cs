using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreMaster {

	public static List<int> ScoreFrames(List<int> rolls)
	{
		List<int> framelist = new List<int>();
		for (int i = 1; i < rolls.Count; i += 2)
		{
			//Prevent more frames than intended(11th frame in this case)
			if (framelist.Count >=10)
			{
				break;
			}
			//Normal bowling frame
			if (rolls[i-1] + rolls[i] < 10)
			{
				framelist.Add(rolls[i - 1] + rolls[i]);
			}
			//Ensure there is enough look ahead
			if(rolls.Count - i <= 1)
			{
				break;
			}
			//Strike
			if (rolls[i - 1] == 10)
			{
				i--;
				framelist.Add(10 + rolls[i + 1] + rolls[i + 2]);
			}
			else
			//Spare
			if (rolls[i - 1] + rolls[i] == 10)
			{
				framelist.Add(rolls[i + 1] + 10);
			}
		}
		return framelist;
	}
	public static List<int> ScoreCumulative(List<int> rolls)
	{
		List<int> scoreList = new List<int>();
		int runningTotal = 0;

		foreach (int roll in ScoreFrames(rolls))
		{
			runningTotal += roll;
			scoreList.Add(runningTotal);
		}
		return scoreList;
	}
}
