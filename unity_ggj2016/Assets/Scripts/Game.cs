using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	private GameObject player;
	private StateManager fsm;

	private void Awake () {
		player = GameObject.FindWithTag("Player");
		fsm = new StateManager();
		fsm.Build(StateManager.StateId.Player);
	}
	
	// Update is called once per frame
	void Update () {
		fsm.currentState.Update();
	}
}
