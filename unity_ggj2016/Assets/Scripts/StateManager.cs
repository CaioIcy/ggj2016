using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager {

	public enum StateId {
		Player, Reaction
	};

	public State currentState;
	private Dictionary<StateId, State> stateMap;

	public void Build(StateId initial) {
		this.stateMap = new Dictionary<StateId, State>();

		this.stateMap.Add(StateId.Player, new PlayerState(this));
		this.stateMap.Add(StateId.Reaction, new ReactionState(this));

		this.currentState = this.stateMap[initial];
		currentState.Enter();
	}

	public void ChangeState(StateId to) {
		currentState.Exit();
		currentState = this.stateMap[to];
		currentState.Enter();
	}

}
