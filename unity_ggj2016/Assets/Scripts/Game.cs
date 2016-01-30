using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	private GameObject player;
	private StateManager fsm;
	public static ArrayList buttons;
	public static ArrayList types;

	private void Awake () {
		player = GameObject.FindWithTag("Player");
		Game.buttons = new ArrayList();
		Game.types = new ArrayList();

		fsm = new StateManager();
		fsm.Build(StateManager.StateId.Player);
	}
	
	// Update is called once per frame
	void Update () {
		fsm.currentState.Update();
	}
}
