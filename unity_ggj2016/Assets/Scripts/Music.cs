using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour {
	private static bool loaded = false;
	private static bool started = false;
	private AudioSource audioSource;
	public AudioClip initialClip;
	public AudioClip loopClip;

	public void Awake() {
		this.audioSource = GetComponent<AudioSource>();
		if(!loaded) {
	        DontDestroyOnLoad(this.transform.gameObject);
	        loaded = true;
		}
	}

	IEnumerator Start() {
		if(!started) {
			started = true;	
			this.audioSource.clip = initialClip;
			this.audioSource.Play();

			yield return new WaitForSeconds(this.audioSource.clip.length);
			this.audioSource.clip = loopClip;
			this.audioSource.Play();
		}
	}

}
