using UnityEngine;
using System.Collections;

public class GoodTurnState : GameState {
	private GameObject player;
	private Animator playerAnimator;

	public override void Enter() {
		// Debug.Log("enter good turn");
		Game.Instance.following.Add(3);
		// play good animation
		player = GameObject.FindWithTag("Player");
		playerAnimator = player.GetComponent<Animator>();
		playerAnimator.Play("good turn");
	}

	public override void Exit() {
		// ?
	}

	public override void Update() {
		if(IsDone()) {
			if(Game.Instance.IsGameEnd() != GameEndType.NotYet) {
				StateManager.Instance.ChangeGameState(GameStateId.Summon);
			}
			else {
				StateManager.Instance.ChangeGameState(GameStateId.PlayerTurn);
			}
		}
	}

	private bool IsDone() {
		// implement me
		return true;
	}
}
