using UnityEngine;
using System.Collections;

public class PlayerState : State {

	public float timeToChange = 2.0f;
	public float totalTime = 0.0f;

	public PlayerState(StateManager fsm): base(fsm) {}

	public override void Enter() {
		Debug.Log("enter PlayerState");
	}

	public override void Exit() {
		Debug.Log("exit PlayerState");
		totalTime = 0.0f;
	}

	public override void Update() {
		Debug.Log("update player " + totalTime + " -- " + timeToChange);
		totalTime += Time.deltaTime;

		if(totalTime >= timeToChange) {
			this.fsm.ChangeState(StateManager.StateId.Reaction);
		}
	}

}
