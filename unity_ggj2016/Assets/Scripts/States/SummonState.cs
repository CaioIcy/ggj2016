using UnityEngine;
using System.Collections;

public class SummonState : GameState {
	public override void Enter() {
		if(Game.Instance.gameEndType == GameEndType.NotYet) {
			Debug.Log("game should not have ended");
			Debug.Break();
		}

		Game.Instance.objUi.Clear();
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
