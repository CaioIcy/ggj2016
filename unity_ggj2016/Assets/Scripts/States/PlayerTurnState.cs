using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum TurnEndCheck {
	NotYet, CompletedActions, OutOfTime
}

public class PlayerTurnState : GameState {

	public override void Enter() {
		Game.Instance.objUi.helpText.text = "player turn";

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
		Game.Instance.DrawButtons(DrawId.NEUTRAL);
		Game.Instance.isPlayerTurn = true;
		Debug.Log("enter player turn with " + Game.Instance.following.Size() + " followers");
	}

	public override void Exit() {
		// Debug.Log("exit PlayerTurnState");
		// Game.Instance.objUi.helpText.text = "waiting...";
		Game.Instance.isPlayerTurn = false;
		Game.Instance.turnOver = false;
	}

	public override void Update() {
		Game.Instance.turnInfo.timePerformed += Time.deltaTime;

		TurnEndCheck tec = GetTurnEndCheck();
		if(tec != TurnEndCheck.NotYet) {
			EndTheTurn(tec);
		}
	}

	private TurnEndCheck GetTurnEndCheck() {
		int idx = Game.Instance.turnInfo.currentIdx;
		int expected = Game.Instance.turnInfo.numActionsExpected;

		if(idx == expected) {
			return TurnEndCheck.CompletedActions;
		}

		if(Game.Instance.turnInfo.timePerformed >= Game.Instance.turnInfo.timeExpected) {
			return TurnEndCheck.OutOfTime;
		}

		return TurnEndCheck.NotYet;
	}

	private void EndTheTurn(TurnEndCheck tec) {
		Game.Instance.TriggerWaitForTurnOver();
		if(!Game.Instance.turnOver) {
			Game.Instance.StopWaitForStunOver();
			Game.Instance.CancelStun();
			Game.Instance.objUi.helpText.text = "breathe...";
			return;
		}

		Game.Instance.StopWaitForTurnOver();
		TurnEnd turnEnd = Game.Instance.CalculateTurnSuccess(tec);
		if(turnEnd.successful) {
			StateManager.Instance.ChangeGameState(GameStateId.GoodTurn);
		}
		else {
			StateManager.Instance.ChangeGameState(GameStateId.BadTurn);
		}
	}

}
