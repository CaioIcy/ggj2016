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
	public static float ratioToTurnSuccess = 0.5f;

	public static float ratioToGoodSummon = 0.8f; // fever anim
	public static float ratioToLoserAnim = 0.45f; // loser anim

	public static float stunDuration = 1.0f; // time per action \/
	public static int totalTurns = 1;//Random.Range(6, 12);

	public static int NumActionsTurn(int turnIdx) {
		// turnIdx => 1..totalTurns
		int numActions = 0;
		float turnRange = (float)turnIdx/(float)Difficulty.totalTurns;
		
		int max = 8;

 		if(turnRange > 1.0f - 1*0.125f) {
			numActions = max;
		} else if(turnRange > 1.0f - 2*0.125f) {
			numActions = Random.Range(max-1, max);
		} else if(turnRange > 1.0f - 3*0.125f) {
			numActions = Random.Range(max-2, max-1);
		} else if(turnRange > 1.0f - 4*0.125f) {
			numActions = Random.Range(max-3, max-1);
		} else if(turnRange > 1.0f - 5*0.125f) {
			numActions = Random.Range(max-3, max-2);
		} else {
			numActions = Random.Range(max-4, max-3);
		}

		return numActions;
	}

	public static float SecondsInTurn(int numActions, int turnIdx) {
		// turnIdx => 1..totalTurns
		float turnModifier = (1.0f/(turnIdx*1.8f)) * 1.08f;
		float timePerAction = 0.34f + turnModifier;
		Difficulty.stunDuration = timePerAction;
		float turnSeconds = timePerAction * numActions;


		Debug.Log("tl-" + turnSeconds + " = " + timePerAction + " * " + numActions);

		return turnSeconds;
	}
}
