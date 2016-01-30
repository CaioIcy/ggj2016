using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public void Update() {
		if(Game.Instance.isPlayerTurn) {
			if(Input.GetKeyDown(KeyCode.H)) {
				Game.Instance.ReceiveAction(Action.ButtonId.A);
			}
			else if(Input.GetKeyDown(KeyCode.J)) {
				Game.Instance.ReceiveAction(Action.ButtonId.B);
			}
			else if(Input.GetKeyDown(KeyCode.K)) {
				Game.Instance.ReceiveAction(Action.ButtonId.X);
			}
			else if(Input.GetKeyDown(KeyCode.L)) {
				Game.Instance.ReceiveAction(Action.ButtonId.Y);
			}
		}
	}
}
