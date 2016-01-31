using UnityEngine;
using System.Collections;

public class SummaryState : GameState {
	public override void Enter() {
		Game.Instance.SummonCreature();

		// bad
		if(Game.Instance.gameEndType == GameEndType.NoFollowers) {
			Game.Instance.playerAnimator.Play("loser");
			GameObject.FindWithTag("sfx_bad_summon").GetComponent<AudioSource>().Play();
		}
		// neutral
		else if(!Game.Instance.goodSummon) {
			Game.Instance.playerAnimator.Play("neutral");
			GameObject.FindWithTag("sfx_good_summon").GetComponent<AudioSource>().Play();
		}
		// good
		else {
			Game.Instance.playerAnimator.Play("victory");
			GameObject.FindWithTag("sfx_good_summon").GetComponent<AudioSource>().Play();
			Game.Instance.following.Add(Random.Range(8, 15));
		}
	}

	public override void Exit() {
	}

	public override void Update() {
		if(IsDone()) {
			Game.Instance.ResetAll();
			// StateManager.Instance.ChangeGameState(GameStateId.TitleScreen);
		}
	}

	private bool IsDone() {
		return Input.anyKeyDown;
	}
}
