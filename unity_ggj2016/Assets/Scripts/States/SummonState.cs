﻿using UnityEngine;
using System.Collections;

public class SummonState : GameState {

	private float summonTime = 1.5f;
	private float timeSummoning = 0.0f;

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
		this.timeSummoning = 0.0f;
	}

	public override void Update() {
		this.timeSummoning += Time.deltaTime;

		if(IsDone()) {
			StateManager.Instance.ChangeGameState(GameStateId.Summary);
		}
	}

	private bool IsDone() {
		return (this.timeSummoning > this.summonTime);
	}
}
