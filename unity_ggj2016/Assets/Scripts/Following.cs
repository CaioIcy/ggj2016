using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Following : MonoBehaviour {

	public GameObject pfbFollower_onLeft;
	public GameObject pfbFollower_onRight;
	private List<GameObject> list = new List<GameObject>();
	
	public void Add(int amount) {
		float y_min = -2.24f;
		float y_max = -5.65f;

		float x_min = 9.0f;
		float x_max = 3.5f;

		for(int i = 0; i < amount; ++i) {
			int x_sign = RandomSign();
			Vector3 pos = new Vector3(
				Random.Range(x_sign * x_min, x_sign * x_max),
				Random.Range(y_min, y_max),
				0.0f
			);

			GameObject gObj = null;
			if(x_sign == -1) {
				gObj = Instantiate(this.pfbFollower_onLeft, pos, Quaternion.identity) as GameObject;
			}
			else {
				gObj = Instantiate(this.pfbFollower_onRight, pos, Quaternion.identity) as GameObject;				
			}
			// gObj.GetComponent<SpriteRenderer>().sortingOrder = (int) pos.y * (-1);
			this.list.Add(gObj);
		}
	}

	public void Remove(int amount) {
		if(amount >= this.list.Count) {
			Debug.Log("no more followers | gameover/go to summon");
			// Debug.Break();
			// return;
		}

		for(int i = 0; i < amount; ++i) {
			if(this.list.Count > 0) {
				int idx = (int) Random.Range(0, this.list.Count-1);
				Destroy(this.list[idx]);
				this.list.RemoveAt(idx);
			}
		}
	}

	public void Clear() {
		Debug.Log("clear following");
		for(int i = 0; i < this.list.Count; ++i) {
			Destroy(this.list[i]);
		}
		this.list.Clear();
	}

	public int Size() {
		return this.list.Count;
	}

	private int RandomSign() {
	    return Random.value < .5? 1 : -1;
	}

}
