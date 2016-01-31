using UnityEngine;
using System.Collections;

public class BadTurnState : GameState {
	private GameObject player;
	private Animator playerAnimator;

	public override void Enter() {
		// Debug.Log("enter bad turn");
		Game.Instance.following.Remove(3);
		// play bad animation
		player = GameObject.FindWithTag("Player");
		playerAnimator = player.GetComponent<Animator>();
		playerAnimator.Play("bad turn");
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
		// finished bad turn animation		
		return true;
	}
}
