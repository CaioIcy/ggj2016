using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public static bool playerShouldInput = false;
	public static ArrayList buttons;
	public static bool playerTurnOver = false;

	private GameObject player;
	private StateManager fsm;
	private int totalInputs = 0;
	private int currentIdx = 0;
	// public static ArrayList types;

	private void Awake () {
		player = GameObject.FindWithTag("Player");
		Game.buttons = new ArrayList();
		// Game.types = new ArrayList();

		fsm = new StateManager();
		fsm.Build(StateManager.StateId.Player);
	}
	
	// Update is called once per frame
	private void Update () {
		fsm.currentState.Update();
	}

	public bool PlayerAction(Action.ButtonId btn, int actionIdx) {
		if(!(actionIdx <= buttons.Count - 1)) {
			Debug.Log("invalid idx for btns");
			Debug.Break();
		}

		++totalInputs;
		currentIdx = actionIdx;

		bool correct = false;
		bool lastBtn = (actionIdx == buttons.Count - 1);

		// correct button
		if((Action.ButtonId)Game.buttons[actionIdx] == btn) {
			if(lastBtn) { playerTurnOver = true; }
			correct = true;
		}
		// wrong button
		else {
			// stall input ?
			correct = false;
		}

		return correct;
	}
}
