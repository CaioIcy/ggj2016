using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int currentActionsIndex = 0;
	private Game gameController;

	void Awake() {
		gameController = GameObject.FindWithTag("GameController").GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H)) { // A
			gameController.PlayerAction(Action.ButtonId.A);
		}
		else if(Input.GetKeyDown(KeyCode.J)) { // B
			gameController.PlayerAction(Action.ButtonId.B);
		}
		else if(Input.GetKeyDown(KeyCode.K)) { // X
			gameController.PlayerAction(Action.ButtonId.X);
		}
		else if(Input.GetKeyDown(KeyCode.L)) { // Y
			gameController.PlayerAction(Action.ButtonId.Y);
		}
	}
}
