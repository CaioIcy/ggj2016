using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerTurnState : GameState {

	public override void Enter() {
		Debug.Log("enter PlayerTurnState");

		// Generate random actions
		for(int i = 0; i < Game.Instance.turnInfo.numActionsExpected; ++i) {
			Action.ButtonId randomButton = (Action.ButtonId) UnityEngine.Random.Range(
				0, Enum.GetValues(typeof(Action.ButtonId)).Length
			);
			Game.Instance.turnInfo.buttons.Add(randomButton);
		}

		Game.Instance.objUi.helpText.text = "Enter Player State";

		// Draw actions
		// ?
	}

	public override void Exit() {
		Debug.Log("exit PlayerTurnState");
		Game.Instance.objUi.helpText.text = "Exit Player State";
		// Reset
		Game.Instance.ResetTurn();
	}

	public override void Update() {
		Game.Instance.turnInfo.timePerformed += Time.deltaTime;

		if(IsTurnEnd()) {
			EndTheTurn();
		}

		CheckInput();
	}

	private void CheckInput() {
		if(Input.GetKeyDown(KeyCode.H)) {
			Game.Instance.ReceiveAction(Action.ButtonId.A);
		}
		else if(Input.GetKeyDown(KeyCode.J)) {
			Game.Instance.ReceiveAction(Action.ButtonId.B);
		}
		else if(Input.GetKeyDown(KeyCode.K)) {
			Game.Instance.ReceiveAction(Action.ButtonId.X);
		}
		else if(Input.GetKeyDown(KeyCode.L)) {
			Game.Instance.ReceiveAction(Action.ButtonId.Y);
		}
	}

	private bool IsTurnEnd() {
		// implement me
		return false;
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
