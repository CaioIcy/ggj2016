using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum DrawId {
	NEUTRAL, MISS, HIT
};

public class ObjUi : MonoBehaviour {

	public Text helpText;
	public GameObject btn_2_green;
	public GameObject btn_2_gray;
	public GameObject btn_2_red;
	public GameObject img_cloud;

	private List<GameObject> list = new List<GameObject>();
	private GameObject stunObj = null;
	private GameObject cloudObj = null;

	public void Begin() {
		cloudObj = Instantiate(img_cloud) as GameObject;
	}

	public void Add(Action.ButtonId btn, bool stun=false, bool done=false) {
		GameObject obj_btn = null;
		if(done) {
			obj_btn = btn_2_green;
		}
		else if(stun) {
			obj_btn = btn_2_red;
		}
		else {
			obj_btn = btn_2_gray;
		}

		float rot = 0.0f;

		switch(btn) {
			case Action.ButtonId.A:
				rot = 90.0f;
				break;
			case Action.ButtonId.B:
				rot = 180.0f;
				break;
			case Action.ButtonId.X:
				rot = 270.0f;
				break;
			case Action.ButtonId.Y:
				rot = 0.0f;
				break;
			default:
				Debug.Log("btn should be from Action.ButtonId");
				Debug.Break();
				break;
		}

		float posOffset = (this.list.Count)*0.93f + 5;
		if(done) {
			// posOffset = (this.list.Count/2)*0.8f + 5;
		}

		float y = this.transform.position.y;
		if(this.list.Count == 0 || this.list.Count > 4) {
			y -= 0.4f;
		}
		else {
			y += 0.1f;
		}

		Vector3 pos = new Vector3(
			this.transform.position.x + posOffset,
			y,
			this.transform.position.z
		);

		Quaternion quaternion = Quaternion.identity;
		quaternion = Quaternion.AngleAxis(rot, Vector3.forward);

		GameObject gObj_btn = Instantiate(obj_btn, pos, quaternion) as GameObject;

		this.list.Add(gObj_btn);
	}

	public void Clear() {
		if(cloudObj != null) {
			Destroy(cloudObj);
			cloudObj = null;
		}
		Destroy(stunObj);
		
		for(int i = 0; i < this.list.Count; ++i) {
			Destroy(this.list[i]);
		}
		this.list.Clear();
	}

	public void ClearCloud() {
		Destroy(cloudObj);
	}
}
