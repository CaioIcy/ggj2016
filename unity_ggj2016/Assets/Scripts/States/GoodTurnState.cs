using UnityEngine;
using System.Collections;

public class GoodTurnState : GameState {
	public override void Enter() {
		// Debug.Log("enter good turn");
		Game.Instance.following.Add(3);
		// play good animation
	}

	public override void Exit() {
		// ?
	}

	public override void Update() {
		if(IsDone()) {
			if(Game.Instance.IsGameEnd() != GameEndType.NotYet) {
				StateManager.Instance.ChangeGameState(GameStateId.Summon);
			}
			else {
				StateManager.Instance.ChangeGameState(GameStateId.PlayerTurn);
			}
		}
	}

	private bool IsDone() {
		// finished good turn animation
		return true;
	}
}
