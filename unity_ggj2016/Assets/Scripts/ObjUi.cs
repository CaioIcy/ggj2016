using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum DrawId {
	NEUTRAL, MISS, HIT
};

public class ObjUi : MonoBehaviour {
	public Text helpText;

	public GameObject btn_bg_black;
	public GameObject btn_bg_green;
	public GameObject btn_bg_red;
	public GameObject btn_a;
	public GameObject btn_b;
	public GameObject btn_x;
	public GameObject btn_y;
	public float btn_size = 0.0f;

	private List<GameObject> list = new List<GameObject>();

	public void Add(GameObject obj_bg, Action.ButtonId btn) {
		GameObject obj_btn = null;
		switch(btn) {
			case Action.ButtonId.A:
				obj_btn = btn_a;
				break;
			case Action.ButtonId.B:
				obj_btn = btn_b;
				break;
			case Action.ButtonId.X:
				obj_btn = btn_x;
				break;
			case Action.ButtonId.Y:
				obj_btn = btn_y;
				break;
			default:
				Debug.Log("btn should be from Action.ButtonId");
				Debug.Break();
				break;
		}
		Vector3 pos = new Vector3(
			this.transform.position.x + (this.list.Count),
			this.transform.position.y,
			this.transform.position.z
		);
		GameObject gObj_bg = Instantiate(obj_bg, pos, Quaternion.identity) as GameObject;
		GameObject gObj_btn = Instantiate(obj_btn, pos, Quaternion.identity) as GameObject;

		this.list.Add(gObj_bg);
		this.list.Add(gObj_btn);
	}

	public void Clear() {
		for(int i = 0; i < this.list.Count; ++i) {
			Destroy(this.list[i]);
		}
		this.list.Clear();
	}
}
