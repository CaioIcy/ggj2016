using UnityEngine;
using System.Collections;

public abstract class State {
	protected StateManager fsm;

	protected State(StateManager fsm) {
		this.fsm = fsm;
	}

	public abstract void Enter();
	public abstract void Exit();
	public abstract void Update();
}
