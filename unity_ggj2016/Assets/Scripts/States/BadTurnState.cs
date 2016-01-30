using UnityEngine;
using System.Collections;

public class BadTurnState : GameState {
	public override void Enter() {
		Debug.Log("enter bad turn");
	}

	public override void Exit() {
		Debug.Log("exit bad turn");
	}

	public override void Update() {
		if(IsDone()) {
			if(Game.Instance.IsGameEnd()) {
				StateManager.Instance.ChangeGameState(GameStateId.Summon);
			}
			else {
				StateManager.Instance.ChangeGameState(GameStateId.PlayerTurn);
			}
		}
	}

	private bool IsDone() {
		// implement me
		return false;
	}
}
