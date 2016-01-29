using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	public void OnButtonNewGame() {
		SceneManager.LoadScene("Main");
	}

}
