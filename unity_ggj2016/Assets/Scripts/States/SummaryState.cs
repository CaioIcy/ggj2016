using UnityEngine;
using System.Collections;

public class SummaryState : GameState {
	public override void Enter() {
		Debug.Log("enter summary");
	}

	public override void Exit() {
		Debug.Log("exit summary");
	}

	public override void Update() {
		if(IsDone()) {
			StateManager.Instance.ChangeGameState(GameStateId.TitleScreen);
		}
	}

	private bool IsDone() {
		// implement me
		return false;
	}
}
