using UnityEngine;
using System.Collections;

public class ReactionState : State {

	public float timeToChange = 2.0f;
	public float totalTime = 0.0f;

	public ReactionState(StateManager fsm): base(fsm) {}

	public override void Enter() {
		Debug.Log("enter ReactionState");
	}

	public override void Exit() {
		Debug.Log("exit ReactionState");
		totalTime = 0.0f;
	}

	public override void Update() {
		// Debug.Log("update reaction " + totalTime + " -- " + timeToChange);
		totalTime += Time.deltaTime;

		if(totalTime >= timeToChange) {
			this.fsm.ChangeState(StateManager.StateId.Player);
		}
	}

}
