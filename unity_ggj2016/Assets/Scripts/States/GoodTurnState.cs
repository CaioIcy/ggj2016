using UnityEngine;
using System.Collections;

public class GoodTurnState : GameState {
	public override void Enter() {
		Debug.Log("enter good turn");
	}

	public override void Exit() {
		Debug.Log("exit good turn");
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
