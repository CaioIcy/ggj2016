using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// public int currentActionsIndex = 0;
	// private Game gameController;

	void Awake() {
		// gameController = GameObject.FindWithTag("GameController").GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () {
		// if(Game.playerShouldInput) {
		// 	bool success = false;
		// 	if(Input.GetKeyDown(KeyCode.H)) { // A
		// 		success = gameController.PlayerAction(Action.ButtonId.A, currentActionsIndex);
		// 	}
		// 	else if(Input.GetKeyDown(KeyCode.J)) { // B
		// 		success = gameController.PlayerAction(Action.ButtonId.B, currentActionsIndex);
		// 	}
		// 	else if(Input.GetKeyDown(KeyCode.K)) { // X
		// 		success = gameController.PlayerAction(Action.ButtonId.X, currentActionsIndex);
		// 	}
		// 	else if(Input.GetKeyDown(KeyCode.L)) { // Y
		// 		success = gameController.PlayerAction(Action.ButtonId.Y, currentActionsIndex);
		// 	}

		// 	if(success) {
		// 		++currentActionsIndex;
		// 	}
		// }
		// else {
		// 	currentActionsIndex = 0;
		// }
	}
}
