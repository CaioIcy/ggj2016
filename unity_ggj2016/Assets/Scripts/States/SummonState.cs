using UnityEngine;
using System.Collections;

public class SummonState : GameState {
	public override void Enter() {
		Debug.Log("enter summon");
	}

	public override void Exit() {
		Debug.Log("exit summon");
	}

	public override void Update() {
		if(IsDone()) {
			StateManager.Instance.ChangeGameState(GameStateId.Summary);
		}
	}

	private bool IsDone() {
		// implement me
		return false;
	}
}
