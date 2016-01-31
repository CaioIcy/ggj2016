using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip sfxMiss;

	private void Awake() {
		this.audioSource = GetComponent<AudioSource>();
		this.audioSource.mute = true;
		this.audioSource.clip = sfxMiss;
	}

	public void PlayClip() {
		this.audioSource.mute = false;
		this.audioSource.Play();
	}

	public void Update() {
		if(Game.Instance.isPlayerTurn && !Game.Instance.stunned) {
			Game.Instance.StopWaitForStunOver();

			if(Input.GetButtonDown("A_UP")) {
				Game.Instance.ReceiveAction(Action.ButtonId.A);
			} else
			  if(Input.GetButtonDown("B_LEFT")) {
				Game.Instance.ReceiveAction(Action.ButtonId.B);
			} else
			  if(Input.GetButtonDown("X_DOWN")) {
				Game.Instance.ReceiveAction(Action.ButtonId.X);
			} else
			  if(Input.GetButtonDown("Y_RIGHT")) {
				Game.Instance.ReceiveAction(Action.ButtonId.Y);
			}
		}
		else if(Game.Instance.stunned) {
			Game.Instance.TriggerWaitForStun();
		}
	}

	private bool AnyActionKey() {
		return Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.J)
			|| Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L);
	}
}
