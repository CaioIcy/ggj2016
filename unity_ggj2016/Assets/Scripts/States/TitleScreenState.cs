using UnityEngine;
using System.Collections;

public class TitleScreenState : GameState {

	private float zoomIn = 3.0f;
	private float speed = 4.0f;
	private bool keyPressed = false;

	public override void Enter() {
		// Debug.Log("enter title");
		Game.Instance.following.Clear();
		Camera.main.orthographicSize -= zoomIn;
	}

	public override void Exit() {
		// Debug.Log("exit title");
		Camera.main.orthographicSize = Game.Instance.defaultCameraSize;
	}

	public override void Update() {
		if(!keyPressed && Input.anyKeyDown) {
			keyPressed = true;
		}

		if(keyPressed) {
			speed -= 0.5f;
		}

		Camera.main.orthographicSize = Mathf.Lerp(
			Game.Instance.defaultCameraSize - zoomIn,
			Game.Instance.defaultCameraSize,
			Time.time / speed
		);
		if(Camera.main.orthographicSize >= Game.Instance.defaultCameraSize) {
			StateManager.Instance.ChangeGameState(GameStateId.PlayerTurn);
		}
	}
}
