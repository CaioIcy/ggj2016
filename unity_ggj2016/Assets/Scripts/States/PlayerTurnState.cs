using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerTurnState : GameState {

	public override void Enter() {
		Debug.Log("enter PlayerTurnState");

		// Reset
		Game.Instance.ResetTurn();

		// Generate random actions

		for(int i = 0; i < Game.Instance.turnInfo.numActionsExpected; ++i) {
			Action.ButtonId randomButton = (Action.ButtonId) UnityEngine.Random.Range(
				0, Enum.GetValues(typeof(Action.ButtonId)).Length
			);
			Game.Instance.turnInfo.buttons.Add(randomButton);
		}


		// Draw actions
		Game.Instance.DrawButtons();


		Game.Instance.isPlayerTurn = true;
	}

	public override void Exit() {
		Debug.Log("exit PlayerTurnState");
		Game.Instance.objUi.helpText.text = "waiting...";
		Game.Instance.isPlayerTurn = false;
	}

	public override void Update() {
		Game.Instance.turnInfo.timePerformed += Time.deltaTime;

		if(IsTurnEnd()) {
			EndTheTurn();
		}
	}

	private bool IsTurnEnd() {
		int idx = Game.Instance.turnInfo.currentIdx;
		int expected = Game.Instance.turnInfo.numActionsExpected;
		return (idx == expected);
	}

	private void EndTheTurn() {
		TurnEnd turnEnd = Game.Instance.CalculateTurnSuccess();
		if(turnEnd.successful) {
			StateManager.Instance.ChangeGameState(GameStateId.GoodTurn);
		}
		else {
			StateManager.Instance.ChangeGameState(GameStateId.BadTurn);
		}
	}

}
