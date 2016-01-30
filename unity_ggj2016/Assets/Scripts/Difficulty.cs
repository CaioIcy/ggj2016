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

	public static float ratioToSuccess = 0.5f;
	public static float stunDuration = 1.0f;
	public static int totalTurns = 2;

	public static int NumActionsTurn(int turnIdx) {
		// implement me
		return 1;
	}

	public static float SecondsInTurn(int turnIdx) {
		// implement me
		return 5.0f;
	}
}
