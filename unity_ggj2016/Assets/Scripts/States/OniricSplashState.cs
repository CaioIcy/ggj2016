using UnityEngine;
using System.Collections;

public class OniricSplashState : GameState {

	private float timeToSplashGame = 3.0f;
	private float timeToSplashOniric = 3.5f;
	private float timeSpent = 0.0f;
	private bool onGameSplash = true;

	public override void Enter() {
		Game.Instance.following.Clear();
		// Camera.main.orthographicSize -= 3.0f; // zoom in title state
		Game.Instance.splashGame.SetActive(true);
	}

	public override void Exit() {
		Game.Instance.splashLogo.SetActive(false);
		Camera.main.orthographicSize -= 3.0f;
	}

	public override void Update() {
		timeSpent += Time.deltaTime;
		if(onGameSplash) {
			if(timeSpent >= timeToSplashGame) {
				timeSpent = 0.0f;
				onGameSplash = false;
				Game.Instance.splashGame.SetActive(false);
				Game.Instance.splashLogo.SetActive(true);
			}
		}
		else {
			if(timeSpent >= timeToSplashOniric) {
				StateManager.Instance.ChangeGameState(GameStateId.TitleScreen);
			}
		}
	}
}
