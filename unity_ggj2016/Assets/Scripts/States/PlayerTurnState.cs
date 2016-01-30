using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerTurnState : GameState {

	public float timeToChange = 10.0f;
	public float totalTime = 0.0f;
	public int numActions = 4;

	public override void Enter() {
		Debug.Log("enter PlayerTurnState");

		// Generate random actions
		for(int i = 0; i < numActions; ++i) {
			Action.ButtonId randomButton = (Action.ButtonId) UnityEngine.Random.Range(
				0, Enum.GetValues(typeof(Action.ButtonId)).Length
			);
			Game.Instance.turnInfo.buttons.Add(randomButton);
		}

		// Draw actions
		// ?
	}

	public override void Exit() {
		Debug.Log("exit PlayerTurnState");
		totalTime = 0.0f;
		Game.Instance.turnInfo.buttons.Clear();
	}

	public override void Update() {
		// Debug.Log("update player " + totalTime + " -- " + timeToChange);
		totalTime += Time.deltaTime;

		if(totalTime >= timeToChange) {
			EndOfTurn();
		}

	}

	private void EndOfTurn() {
		TurnEnd turnEnd = Game.Instance.CalculateTurnSuccess();
		if(turnEnd.successful) {
			StateManager.Instance.ChangeGameState(GameStateId.GoodTurn);
		}
		else {
			StateManager.Instance.ChangeGameState(GameStateId.BadTurn);
		}
	}

}
