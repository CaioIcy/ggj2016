using UnityEngine;
using System.Collections;

public class BadTurnState : GameState {
	public override void Enter() {
		Debug.Log("enter bad turn");
		// play bad animation
		Game.Instance.following.Remove(3);
	}

	public override void Exit() {
		// ?
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
		return true;
	}
}
