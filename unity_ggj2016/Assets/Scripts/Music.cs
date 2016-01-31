using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour {
	private AudioSource audioSource;
	public AudioClip initialClip;
	public AudioClip loopClip;

	public void Awake() {
		this.audioSource = GetComponent<AudioSource>();
	}

	IEnumerator Start() {
		this.audioSource.clip = initialClip;
		this.audioSource.Play();

		yield return new WaitForSeconds(this.audioSource.clip.length);
		this.audioSource.clip = loopClip;
		this.audioSource.Play();
	}

}
