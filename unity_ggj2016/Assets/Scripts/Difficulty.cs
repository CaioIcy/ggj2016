using UnityEngine;
using System.Collections;

public class Difficulty : MonoBehaviour {
	// ratioModifier == 1.0f __ no change
	// ratioModifier <  1.0f __ easier
	// ratioModifier >  1.0f __ harder
	public static float ratioModifier = 1.0f;

	// imperfectModifier == 0.35f __ no change
	// imperfectModifier <  0.35f __ easier
	// imperfectModifier >  0.35f __ harder
	public static float imperfectModifier = 0.35f;

	// ratiotoTurnSuccess == 0.5f __ no change
	// ratiotoTurnSuccess <  0.5f __ easier
	// ratiotoTurnSuccess >  0.5f __ harder
	public static float ratioToTurnSuccess = 0.6f;

	public static float ratioToGoodSummon = 0.8f; // fever anim
	public static float ratioToLoserAnim = 0.45f; // loser anim

	public static float stunDuration = 1.0f; // time per action \/
	public static int startingFollowers = Random.Range(5,7);
	public static int totalTurns = 1;//Random.Range(12, 20);

	public static int NumActionsTurn(int turnIdx) {
		// turnIdx => 1..totalTurns
		int numActions = 0;
		float turnRange = (float)turnIdx/(float)Difficulty.totalTurns;
		
		int max = 8;
		int min = 3;

		if(turnRange > 2.0f/3.0f) {
			numActions = Random.Range(min, max);
		} else if(turnRange > 1.0f/3.0f) {
			numActions = Random.Range(min+1, max);
		}
		else {
			numActions = Random.Range(min+1, max-2);
		}

		return numActions;
	}

	public static float SecondsInTurn(int numActions, int turnIdx) {
		// turnIdx => 1..totalTurns
		float turnModifier = (1.0f/(turnIdx*1.8f)) * 1.02f;
		float actionsModifier = 1.0f/(float)numActions;
		float timePerAction = 0.07f + turnModifier + actionsModifier;
		Difficulty.stunDuration = timePerAction;
		float turnSeconds = timePerAction * numActions;

		Debug.Log("turn mod = " + turnModifier + "  |  act mod = " + actionsModifier);
		Debug.Log("tl-" + turnSeconds + " = tpa." + timePerAction + " * na." + numActions);

		return turnSeconds;
	}

	public static int FollowerLoss() {
		return Random.Range(2,4);
	}

	public static int FollowerGain() {
		return Random.Range(1,3);
	}
}
