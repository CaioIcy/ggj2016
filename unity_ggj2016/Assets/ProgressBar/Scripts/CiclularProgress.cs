using UnityEngine;
using System.Collections;

public class CiclularProgress : MonoBehaviour {
		
	// Use this for initialization
	void Start () {
		//Use this to Start progress
	}

	public void Reset(float time) {
		StopCoroutine("RadialProgress");
		StartCoroutine(RadialProgress(time));		
	}
	
	IEnumerator RadialProgress(float time)
	{
		float rate = 1 / time;
		float i = 0;
		while (i < 1)
		{
			i += Time.deltaTime * rate;
			gameObject.GetComponent<Renderer>().material.SetFloat("_Progress", i);
			yield return 0;
		}
	}
}
