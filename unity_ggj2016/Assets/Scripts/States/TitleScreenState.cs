using UnityEngine;
using System.Collections;

public class TitleScreenState : GameState {

	private float timeToSplashGame = 3.0f;
	private float timeToSplashOniric = 3.5f;
	private float timeSpent = 0.0f;
	private bool onGameSplash = true;
	private bool doneSplashing = false;

	private float zoomIn = 3.0f;
	private float speed = 4.0f;
	private bool keyPressed = false;

	public override void Enter() {
		Debug.Log("enter title screen");
		Game.Instance.following.Clear();
		Game.Instance.objUi.Clear();
		Game.Instance.splashGame.SetActive(true);
	}

	public override void Exit() {
		Camera.main.orthographicSize = Game.Instance.defaultCameraSize;
		Game.Instance.player.SetActive(true);

		timeToSplashGame = 3.0f;
		timeToSplashOniric = 3.5f;
		timeSpent = 0.0f;
		onGameSplash = true;
		doneSplashing = false;
		zoomIn = 3.0f;
		speed = 4.0f;
		keyPressed = false;

	}

	public override void Update() {
		timeSpent += Time.deltaTime;
		if(!doneSplashing) {
			if(onGameSplash) {
				if(timeSpent >= timeToSplashGame || Input.anyKeyDown) {
					timeSpent = 0.0f;
					onGameSplash = false;
					Game.Instance.splashGame.SetActive(false);
					Game.Instance.splashLogo.SetActive(true);
				}
			}
			else {
				if(timeSpent >= timeToSplashOniric || Input.anyKeyDown) {
					doneSplashing = true;
					Camera.main.orthographicSize -= zoomIn;
					Game.Instance.splashLogo.SetActive(false);
				}
			}

		}
		else {
			if(!keyPressed && Input.anyKeyDown) {
				keyPressed = true;
			}

			if(keyPressed) {
				speed -= 0.5f;
			}

			Camera.main.orthographicSize += 0.03f;
			if(Camera.main.orthographicSize >= Game.Instance.defaultCameraSize) {
				StateManager.Instance.ChangeGameState(GameStateId.PlayerTurn);
			}
		}
	}
}
