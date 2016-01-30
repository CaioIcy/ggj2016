using UnityEngine;
using System.Collections;

public class TitleScreenState : GameState {
	public override void Enter() {
		// Debug.Log("enter title");
		Game.Instance.following.Clear();
	}

	public override void Exit() {
		// Debug.Log("exit title");
	}

	public override void Update() {
		if(Input.anyKeyDown) {
			StateManager.Instance.ChangeGameState(GameStateId.PlayerTurn);
			Game.Instance.FirstTurn();
		}
	}
}
