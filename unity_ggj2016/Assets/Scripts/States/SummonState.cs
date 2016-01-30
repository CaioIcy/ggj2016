using UnityEngine;
using System.Collections;

public class SummonState : GameState {
	public override void Enter() {
		Debug.Log("enter summon");
		Game.Instance.PlaySummonAnimation();
		Game.Instance.SetText("summoning");
	}

	public override void Exit() {
	}

	public override void Update() {
		if(IsDone()) {
			StateManager.Instance.ChangeGameState(GameStateId.Summary);
		}
	}

	private bool IsDone() {
		// implement me
		// done playing summon animation?
		return false;
	}
}
