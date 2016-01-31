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
		Game.Instance.splashGame.SetActive(true);
	}

	public override void Exit() {
		Camera.main.orthographicSize = Game.Instance.defaultCameraSize;
		Game.Instance.player.SetActive(true);
	}

	public override void Update() {
		timeSpent += Time.deltaTime;
		if(!doneSplashing) {
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

			Debug.Log("1 " +Camera.main.orthographicSize);
			// Camera.main.orthographicSize = Mathf.Lerp(
			// 	Game.Instance.defaultCameraSize - zoomIn,
			// 	Game.Instance.defaultCameraSize,
			// 	Time.time / speed
			// );
			Camera.main.orthographicSize += 0.03f;
			Debug.Log("2 "+Camera.main.orthographicSize);
			if(Camera.main.orthographicSize >= Game.Instance.defaultCameraSize) {
				StateManager.Instance.ChangeGameState(GameStateId.PlayerTurn);
			}
		}
	}
}
