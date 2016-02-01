using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SummaryState : GameState {

	private bool isDone = false;
	private AsyncOperation ao = null;
	private float timetoAlbum = 3.5f;
	private float timeSpent = 0.0f;

	public override void Enter() {
		Game.Instance.SummonCreature();
		Game.Instance.gfxSummon.SetActive(false);

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
		isDone = false;
		ao = null;
		timeSpent = 0.0f;
	}

	public override void Update() {
		if(IsDone()) {
			timeSpent += Time.deltaTime;
			if(timeSpent >= timetoAlbum) {
				if(ao == null) {
					ao = SceneManager.LoadSceneAsync("Main");
				}
				else {
					if(ao.isDone) {
						Debug.Log("scene restarted");
						StateManager.Instance.ChangeGameState(GameStateId.TitleScreen);
					}
				}

			}
			else {
				Game.Instance.fire1.SetActive(false);
				Game.Instance.fire2.SetActive(false);
				Game.Instance.album.SetActive(true);
			}
		}
	}

	private bool IsDone() {
		if(!isDone) {
			isDone = Input.anyKeyDown;
		}
		return isDone;
		// return false;
	}
}
