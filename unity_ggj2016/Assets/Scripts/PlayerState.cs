using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : State {

	public float timeToChange = 2.0f;
	public float totalTime = 0.0f;
	public int numActions = 4;

	public PlayerState(StateManager fsm): base(fsm) {}

	public override void Enter() {
		Debug.Log("enter PlayerState");

		for(int i = 0; i < numActions; ++i) {
			Action.ButtonId randomButton = (Action.ButtonId) UnityEngine.Random.Range(
				0, Enum.GetValues(typeof(Action.ButtonId)).Length
			);
			Game.buttons.Add(randomButton);

			Action.TypeId randomType = (Action.TypeId) UnityEngine.Random.Range(
				0, Enum.GetValues(typeof(Action.TypeId)).Length
			);
			Game.types.Add(randomType);
		}

		if(Game.buttons.Count != Game.types.Count) {
			Debug.Log("btns length should be equal to typs length");
			Debug.Break();
		}

		for(int i = 0; i < Game.buttons.Count; ++i) {
			Debug.Log(Game.buttons[i] + " -- " + Game.types[i]);
		}

	}

	public override void Exit() {
		Debug.Log("exit PlayerState");
		totalTime = 0.0f;
		Game.buttons.Clear();
		Game.types.Clear();
	}

	public override void Update() {
		// Debug.Log("update player " + totalTime + " -- " + timeToChange);
		totalTime += Time.deltaTime;

		if(totalTime >= timeToChange) {
			this.fsm.ChangeState(StateManager.StateId.Reaction);
		}
	}

}
